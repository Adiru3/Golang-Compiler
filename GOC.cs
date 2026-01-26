using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections;

namespace GoSingleCompiler
{
    class Program
    {
        static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        static string goDir = Path.Combine(baseDir, "go_internal");
        static string goExe = Path.Combine(goDir, "bin", "go.exe");
        static string mingwBin = Path.Combine(baseDir, "mingw64", "bin");

        static void Main(string[] args)
        {
            Console.Title = "Amaze Go Compiler Wrapper";

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: GoCompiler.exe <filename.go>");
                return;
            }

            string sourceFile = Path.GetFullPath(args[0]);

            try 
            {
                CheckEnvironment();
                CompileFile(sourceFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nCRITICAL ERROR: " + ex.Message);
            }

            Console.WriteLine("\nDone. Press any key to exit...");
            Console.ReadKey();
        }

        static void CheckEnvironment()
        {
            if (!File.Exists(goExe))
            {
                throw new Exception("Go compiler not found at: " + goExe + "\nPlease put Go distribution into 'go_internal' folder.");
            }

            if (!Directory.Exists(mingwBin))
            {
                Console.WriteLine("WARNING: 'mingw64' folder not found. CGO (GUI) apps might fail to compile.");
            }
        }

        static void CompileFile(string sourceFile)
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("Error: File {0} does not exist.", sourceFile);
                return;
            }

            string workDir = Path.GetDirectoryName(sourceFile);
            string goModFile = Path.Combine(workDir, "go.mod");

            // 1. Инициализация модуля (если нужно)
            if (!File.Exists(goModFile))
            {
                Console.WriteLine("--> Initializing Go module...");
                RunGoCommand("mod init project", workDir);
            }

            // 2. Загрузка зависимостей (Fyne и др.)
            Console.WriteLine("--> Checking dependencies (go mod tidy)...");
            RunGoCommand("mod tidy", workDir);

            // 3. Сборка бинарника
            string outputExe = Path.ChangeExtension(sourceFile, ".exe");
            Console.WriteLine("--> Compiling {0}...", Path.GetFileName(sourceFile));

            // -s -w убирает отладочную инфу (меньше вес)
            // -H windowsgui убирает консоль при запуске готовой программы
            string buildArgs = string.Format("build -ldflags=\"-s -w -H windowsgui\" -o \"{0}\" \"{1}\"", outputExe, sourceFile);
            
            bool success = RunGoCommand(buildArgs, workDir);

            if (success)
            {
                Console.WriteLine("\nSUCCESS! Compiled to: " + outputExe);
            }
        }

        static bool RunGoCommand(string args, string workingDirectory)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = goExe,
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = workingDirectory
            };
            
            // Настройка GOROOT и GOPATH
            psi.EnvironmentVariables["GOROOT"] = goDir;
            psi.EnvironmentVariables["GOPATH"] = Path.Combine(baseDir, "go_path");
            
            // Настройка Сети и CGO
            psi.EnvironmentVariables["GOPROXY"] = "https://proxy.golang.org,direct";
            psi.EnvironmentVariables["GOSUMDB"] = "sum.golang.org";
            psi.EnvironmentVariables["CGO_ENABLED"] = "1";

            // Добавляем MinGW в PATH текущего процесса
            string currentPath = Environment.GetEnvironmentVariable("PATH");
            psi.EnvironmentVariables["PATH"] = mingwBin + ";" + currentPath;

            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                string err = p.StandardError.ReadToEnd();
                
                p.WaitForExit();

                if (p.ExitCode != 0)
                {
                    if (!string.IsNullOrEmpty(output)) Console.WriteLine(output);
                    if (!string.IsNullOrEmpty(err)) Console.WriteLine("Go Error: " + err);
                    return false;
                }
                return true;
            }
        }
    }
}
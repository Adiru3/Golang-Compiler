# Go Compiler (GOC)

A standalone, portable Go compiler wrapper built on **.NET Framework 4.0**. This tool is designed to provide a completely isolated Go development environment that supports **CGO** and **GUI libraries (like Fyne)** without requiring any system-wide installations.

## 🚀 Features

* **Zero Installation:** Everything (Go, MinGW, Dependencies) stays inside the application folder.
* **Automatic Dependency Management:** Automatically initializes `go.mod` and runs `go mod tidy`.
* **GUI Ready:** Pre-configured for **Fyne** and other CGO-based libraries.
* **Optimized Output:** Automatically applies `-s -w` flags for smaller binaries and `-H windowsgui` to hide the console window.
* **Low-Level Focus:** Tailored for creating system optimization tools like AmazeMem, ALPA, and AmazeDisk.

## 📂 Directory Structure

For the compiler to work correctly, organize your folders as follows:

```
GOC/
├── GoCompiler.exe          # Compiled GOC.cs
├── go_internal/            # Standard Go distribution (bin, pkg, src)
├── mingw64/                # MinGW-w64 (posix-seh-ucrt)
│   └── bin/gcc.exe         # C-Compiler for CGO
└── go_path/                # Automatically created (stores downloaded libs)
```
Usage
Simply drag and drop your .go file onto GoCompiler.exe or use the command line:
``` GoCompiler.exe C:\path\to\your\main.go ```

The tool will:

Check for go_internal and mingw64.

Create go.mod if it's missing.

Download all required libraries into go_path.

Compile your script into a high-performance Windows executable.

## ⚙️ Environment Variables (Auto-configured)
The wrapper dynamically sets these for every build process to ensure isolation:

GOROOT -> ./go_internal
GOPATH -> ./go_path
GOPROXY -> https://proxy.golang.org,direct
CGO_ENABLED -> 1
PATH -> Includes ./mingw64/bin

## 📦 Requirements
OS: Windows 7/8/10/11
Runtime: .NET Framework 4.0
Architecture: x64 (recommended for SEH/UCRT)

## 🔗 Connect with me
[![YouTube](https://img.shields.io/badge/YouTube-@adiruaim-FF0000?style=for-the-badge&logo=youtube)](https://www.youtube.com/@adiruaim)
[![TikTok](https://img.shields.io/badge/TikTok-@adiruhs-000000?style=for-the-badge&logo=tiktok)](https://www.tiktok.com/@adiruhs)

### 💰 Legacy Crypto
* **BTC:** `bc1qflvetccw7vu59mq074hnvf03j02sjjf9t5dphl`
* **ETH:** `0xf35Afdf42C8bf1C3bF08862f573c2358461e697f`
* **Solana:** `5r2H3R2wXmA1JimpCypmoWLh8eGmdZA6VWjuit3AuBkq`
* **USDT (TRC20):** `TNgFjGzbGxztHDcSHx9DEPmQLxj2dWzozC`
* **USDT (TON):** `UQC5fsX4zON_FgW4I6iVrxVDtsVwrcOmqbjsYA4TrQh3aOvj`

### 🌍 Support Links
[![Donatello](https://img.shields.io/badge/Support-Donatello-orange?style=for-the-badge)](https://donatello.to/Adiru3)
[![Ko-fi](https://img.shields.io/badge/Ko--fi-Support-blue?style=for-the-badge&logo=kofi)](https://ko-fi.com/adiru)
[![Steam](https://img.shields.io/badge/Steam-Trade-blue?style=for-the-badge&logo=steam)](https://steamcommunity.com/tradeoffer/new/?partner=1124211419&token=2utLCl48)

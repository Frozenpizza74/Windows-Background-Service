# ⚙️ Windows Service in C# (.NET Framework)
This project implements a Windows background service using C# and the .NET Framework. Designed for an Operating Systems course, it demonstrates how system-level services are created, installed, and managed on Windows environments.

## Features
- Built with .NET Framework 4.7.2
- Uses `ProjectInstaller` for easy installation
- Modular code structure

## Getting Started
1. Build the solution in Visual Studio.
2. Open Command Prompt as Administrator.
3. Navigate to the output directory and run:
    ```bash
    InstallUtil.exe OS_Project_1.exe
    ```
4. Start the service from the **Services** console or using:
    ```bash
    net start "OS Project 1"
    ```

## Files
- `Service1.cs`: Main service logic
- `ProjectInstaller.cs`: Installer configuration
- `Program.cs`: Entry point

## Notes
Use `sc delete` or `InstallUtil /u` to uninstall the service.

# XLauncher
**A basic launcher for basic games**
**Legacy (.NET Framework) version**

## Overview

This is a game launcher supporting custom image and music, configurable with a JSON file. It is built with .NET and WPF.

This is the "legacy" branch that uses .NET Framework instead of .NET (Core). It supports most of the same features, except for looping music. For more information on the differences, see the README on the main branch

## Usage

Build the project.

Copy everything from the build folder to your destination folder. Copy `startup.example.json` to your destination folder, edit to your liking, and rename to `config.json`.

The program icon is built into the application, you can change it from the XLauncher project properties (Win32 Resources->Icon in VS2022).

## License

This project is licensed under the MIT License, a copy of which is available in COPYING.txt

This project uses Newtonsoft.Json as a dependencies, which has its own license terms.
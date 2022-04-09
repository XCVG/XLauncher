# XLauncher
**A basic launcher for basic games**

## Overview

This is a game launcher supporting custom image and music, configurable with a JSON file. It is built with WPF and .NET 6.

## Usage

Publish (not build) the project. There are publish profiles included for both self-contained and framework-dependent executables.

Copy everything from the publish folder to your destination folder. Copy `startup.example.json` to your destination folder, edit to your liking, and rename to `config.json`.

The program icon is built into the application, you can change it from the XLauncher project properties (Win32 Resources->Icon in VS2022).

## License

This project is licensed under the MIT License, a copy of which is available in COPYING.txt

This project uses NAudio and Newtonsoft.Json as dependencies, which have their own license terms.
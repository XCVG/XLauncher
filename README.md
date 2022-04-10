# XLauncher
**A basic launcher for basic games**

## Overview

This is a game launcher supporting custom image and music, configurable with a JSON file. It is built with .NET and WPF.

This is the main branch that uses .NET 6. It supports more features, but has some deployment caveats. See the Versions section for more information.

## Versions

There are two versions of this launcher built on slightly different technologies. For more information on the differences between .NET Framework and .NET (Core) in general, see https://docs.microsoft.com/en-us/dotnet/fundamentals/implementations

### .NET (Core) version (master branch)

This version uses .NET 6. It is a newer development of this project but is now the main development focus which will receive new features first. It uses NAudio for music playback.

There are two publishing options. _Framework-dependent_ produces a very small executable, but one that relies on the .NET 6 runtime being installed already. As .NET 6 is very new, few users will have it installed. _Self-contained_ produces an executable with no dependencies, but one that is _much_ larger.

You can read about the difference between self-contained and framework-dependent (in general) here: https://docs.microsoft.com/en-us/dotnet/core/deploying/

Note that the `netcore` branch is an artifact of the initial porting process and can be ignored.

### .NET Framework version (netfx branch)

This version uses .NET Framework. It is the first version of this project to exist, but has been resurrected with features backported from the .NET version. It uses WMPLib for music playback. While it is currently at feature parity, as time goes on it is likely this branch will lag behind the other in features.

The executable produced is dependent on .NET Framework and is very small. There is no self-contained option, but .NET Framework has been provided with Windows for years and almost all users will have it installed already.

## Usage

Publish (not build) the project. There are publish profiles included for both self-contained and framework-dependent executables.

Copy everything from the publish folder to your destination folder. Copy `startup.example.json` to your destination folder, edit to your liking, and rename to `config.json`.

The program icon is built into the application, you can change it from the XLauncher project properties (Win32 Resources->Icon in VS2022).

Several formats work for the background image (BMP, PNG, JPEG, etc) but the background music must be in Windows Media Audio (WMA) format.

## License

This project is licensed under the MIT License, a copy of which is available in COPYING.txt

This project uses NAudio and Newtonsoft.Json as dependencies, which have their own license terms.
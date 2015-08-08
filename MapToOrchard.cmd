@echo off

if "%1" == "" goto usage

mklink /J %1\src\Tools\Proligence.Orchard.Testing %~dp0\Proligence.Orchard.Testing
goto done

:usage
echo Usage: MapToOrchard.cmd OrchardRootDir
goto exit

:done
echo Done.

:exit
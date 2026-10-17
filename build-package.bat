@echo off
setlocal
pushd "%~dp0"
if errorlevel 1 exit /b 1
set "PACKAGE_POWERSHELL=powershell.exe"
where /q pwsh.exe
if not errorlevel 1 set "PACKAGE_POWERSHELL=pwsh.exe"
"%PACKAGE_POWERSHELL%" -NoLogo -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "%~dp0scripts\Build-Package.ps1" %*
set "PACKAGE_EXIT_CODE=%errorlevel%"
popd
exit /b %PACKAGE_EXIT_CODE%

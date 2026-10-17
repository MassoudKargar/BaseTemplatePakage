@echo off
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0scripts\Verify-Template.ps1" %*
exit /b %errorlevel%

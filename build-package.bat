@echo off
REM حذف پوشه nupkgs در مسیر ریشه پروژه
if exist "nupkgs" (
    echo Removing existing nupkgs folder...
    rmdir /s /q nupkgs
)

REM Clean کردن پروژه (با فرض اینکه csproj در فولدر فعلی هست)
echo Cleaning the project...
dotnet clean

REM ساخت پکیج و ذخیره در پوشه nupkgs
echo Packing the project...
dotnet pack -o nupkgs

echo Done.
pause

@echo off

if not exist ".\src\packages\FAKE.Core.3.17.7" (
    echo Restoring FAKE
    .\src\.nuget\nuget.exe restore ".\src\.nuget\packages.config" -SolutionDirectory ".\src"
)

src.\packages.\FAKE.Core.3.17.7\tools\FAKE.exe %*
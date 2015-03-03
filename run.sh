#!/bin/sh

if [ ! -d "./src/packages/FAKE.Core.3.17.7" ]; then
    echo Restoring FAKE
    mono ./src/.nuget/nuget.exe restore "./src/.nuget/packages.config" -SolutionDirectory "./src"
fi

mono ./src/packages/FAKE.Core.3.17.7/tools/FAKE.exe $@
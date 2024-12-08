#!/bin/bash
dotnet publish -r linux-x64 -c Release /p:PublishAot=true -o bin
rm bin/*.dbg
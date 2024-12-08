#!/bin/bash
dotnet build -c Release -o bin
rm bin/*.dbg
cp wordlist.txt bin/wordlist.txt
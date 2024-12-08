echo "Building for windows!"
dotnet build -c Release -o bin
rm bin/*.dbg
copy wordlist.txt bin\wordlist.txt
Set-ExecutionPolicy RemoteSigned
echo "Building for windows! (AOT for native version)"
dotnet publish -r win-x64 -c Release /p:PublishAot=true -o bin
rm bin/*.dbg
copy wordlist.txt bin\wordlist.txt
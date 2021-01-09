# Patching is fun!

Could not find a CircleCi orb to do so and wrote our own code.



## Verpatch.exe

`Verpatch.exe` is a utility to patch an assembly with a custom version number.
It comes from: https://www.codeproject.com/Articles/37133/Simple-Version-Resource-Tool-for-Windows



Run it as `verpatch foo.dll "1.2.3.4 my special build"`

## Patcher.exe

Patcher is a C# script to patch multiple files beneath a specified a directory, source is in its directory.



Run it as `patcher.exe mydirectory assembly.dll 1.2.3.4`
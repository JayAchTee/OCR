# OCR

After much research, trial and error, I was able to create a Windows 
command line program that uses the Universal Windows Platform (UWP) OcrEngine.

While this is not a fully developed solution it's enough to get you started if 
you need a simple, fast OCR program on Windows 10 and Windows Server 2012R2.

The key to success in doing this is to include the proper references at:

  \Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll
  \Program Files (x86)\Windows Kits\10\UnionMetadata\Windows.winmd 
  
You will need to browse to the above locations to add the reference and for the latter you will need to select All files (*.*) in the file type dropdown.

And to manually edit the Visual Studio project file (OCR.csproj in this case) to add:
  
  [PropertyGroup]
    [TargetPlatformVersion]8.0[/TargetPlatformVersion]
  [PropertyGroup]
  
That should do it!

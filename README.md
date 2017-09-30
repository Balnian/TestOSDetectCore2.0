# TestOSDetectCore2.0
Detecting OS/Target when building .Net Core project (Useful when you need to DllImport different lib name by OS). 
Only add the following to your .csproj
```xml
<!-- OS Detection default Value  -->
  <PropertyGroup>
    <IsWindows>False</IsWindows>
    <IsOSX>False</IsOSX>
    <IsAndroid>False</IsAndroid>
    <IsLinux>False</IsLinux>
  </PropertyGroup>

  <!-- Check if we target Windows -->
  <PropertyGroup Condition="($(RuntimeIdentifier.StartsWith('win')) And !$(RuntimeIdentifier.Equals('')) ) Or ( $(OS.Equals('Windows_NT')) And $(RuntimeIdentifier.Equals('')) ) ">
    <!--Replacing '-' and '.' by '_' in Constants because we cannot use those characters in the source files -->
    <DefineConstants>_WINDOWS_, $(RuntimeIdentifier.Replace("-","_").Replace(".","_"))</DefineConstants>
    <IsWindows>True</IsWindows>
  </PropertyGroup>

  <!-- Check if we target OSX -->
  <PropertyGroup Condition="($(RuntimeIdentifier.StartsWith('osx')) And !$(RuntimeIdentifier.Equals('')) ) Or ( $([System.Runtime.InteropServices.RuntimeInformation]::IsOSPlatform($([System.Runtime.InteropServices.OSPlatform]::OSX))) And $(RuntimeIdentifier.Equals('')) ) ">
    <!--Replacing '-' and '.' by '_' in Constants because we cannot use those characters in the source files -->
    <DefineConstants>_OSX_, $(RuntimeIdentifier.Replace("-","_").Replace(".","_"))</DefineConstants>
    <IsOSX>True</IsOSX>
  </PropertyGroup>

  <!-- Check if we target Android (Doesn't Detect properly for now but is Official: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) -->
  <!-- (Less check to do because we consider we cannot Dev on Android and anyway there's no way to detect an Android Platform other then the RID) -->
  <PropertyGroup Condition="$(RuntimeIdentifier.StartsWith('android')) ">
    <!--Replacing '-' and '.' by '_' in Constants because we cannot use those characters in the source files -->
    <DefineConstants>_ANDROID_, $(RuntimeIdentifier.Replace("-","_").Replace(".","_"))</DefineConstants>
    <IsAndroid>True</IsAndroid>
  </PropertyGroup>

  <!-- Check if we target Linux (To many name to check, instead we check if not one of the other OS) -->
  <PropertyGroup Condition=" !$(IsWindows) And !$(IsOSX) And !$(IsAndroid)">
    <!--Replacing '-' and '.' by '_' in Constants because we cannot use those characters in the source files -->
    <DefineConstants>_LINUX_, $(RuntimeIdentifier.Replace("-","_").Replace(".","_"))</DefineConstants>
    <IsLinux>True</IsLinux>
  </PropertyGroup>
```

And then you can detect the OS you are running on with C# preprocessor directives
```C#
static void Main(string[] args)
        {
#if _WINDOWS_
            Console.WriteLine("_WINDOWS_");

#if (_WINDOWS_ == win10_x64)

            Console.WriteLine("win10_x64");
#endif
#elif _OSX_
            Console.WriteLine("_OSX_");

#elif _LINUX_
            Console.WriteLine("_LINUX_");
#if _LINUX_ == ubuntu_14_04_x64
            Console.WriteLine("ubuntu_14_04_x64");
#elif _LINUX_ == ubuntu_16_04_x64
            Console.WriteLine("ubuntu_16_04_x64");
#endif
#elif _ANDROID_ 
            /* Doesn't Detect properly for now but is Official: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog */
            Console.WriteLine("_ANDROID_");
#endif

        }
```

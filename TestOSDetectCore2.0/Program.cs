using System;

namespace TestOSDetectCore2._0
{
    class Program
    {
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
#endif

        }
    }
}

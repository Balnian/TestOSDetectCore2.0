using System;

namespace TestOSDetectCore2._0
{
    class Program
    {
        static void Main(string[] args)
        {
#if _WINDOWS_
            Console.WriteLine("_WINDOWS_");
#elif _OSX_
            Console.WriteLine("_OSX_");

#elif _LINUX_
            Console.WriteLine("_LINUX_");

#endif

        }
    }
}

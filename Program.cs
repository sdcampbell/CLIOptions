using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Options;

namespace CLIOptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool showHelp = false;
            bool verbose = false;
            string shellcodeStr = null;
            string dll = null;
            string export = null;
            int pid = 0;

            OptionSet option_set = new OptionSet()
                .Add("h|help", "Display this help", v => showHelp = v != null)
                .Add("v|verbose", "Enable verbose output", v => verbose = v != null)
                .Add("x=|shellcode=", "Path/Base64 for x64 shellcode payload (default: calc launcher)", v => shellcodeStr = v)
                .Add<int>("p=|pid=", @"Target process ID to inject", v => pid = v)
                .Add("d=|dll=", "The DLL that that contains the export to patch (must be KnownDll)", v => dll = v)
                .Add("e=|export=", "The exported function that will be hijacked", v => export = v);

            try
            {

                option_set.Parse(args);

                if (dll == null || pid == 0 || export == null)
                {
                    Console.WriteLine("[!] pid, dll and export arguments are required");
                    showHelp = true;
                }

                if (showHelp)
                {
                    option_set.WriteOptionDescriptions(Console.Out);
                    return;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"[!] Failed to parse arguments: {e.Message}");
                option_set.WriteOptionDescriptions(Console.Out);
                return;
            }

            //All other code goes after this
            Console.WriteLine($"The value of verbose was: {verbose}");
            Console.WriteLine($"The value of shellcodeStr was: {shellcodeStr}");
            Console.WriteLine($"The value of pid was: {pid}");
            Console.WriteLine($"The value of dll was: {dll}");
            Console.WriteLine($"The value of export was: {export}");
        }
    }
}

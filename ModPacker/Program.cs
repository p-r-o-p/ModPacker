using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ModPacker
{
    class Program
    {
        static int Main(string[] args)
        {
            int returnValue = ValidateArgs(args);
            if (returnValue != 0)
            {
                return returnValue;
            }

            string fileContent;
            try
            {
                fileContent = File.ReadAllText(args[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

            IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            ModList modList = deserializer.Deserialize<ModList>(fileContent);

            Console.WriteLine("Hello World!");
            return 0;
        }

        private static int ValidateArgs(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("No path provided");
                return 1;
            }
            return 0;
        }
    }
}

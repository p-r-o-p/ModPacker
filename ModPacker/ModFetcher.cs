using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModPacker
{
    class ModFetcher
    {
        public List<Mod> Mods { get; set; }

        private static readonly HttpClient client = new HttpClient();

        public ModFetcher(ModList modList)
        {
            Mods = new List<Mod>();
            string version = modList.MinecraftVersion;
            List<string> mods = modList.Mods;

            foreach (string mod in mods)
            {
                try
                {
                    this.Mods.Add(this.getModInfo(version, mod).Result);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        private async Task<Mod> getModInfo(string version, string modName)
        {
            string responseBody;
            try
            {
                string baseUrl = "https://addons-ecs.forgesvc.net/api/v2/addon/search?categoryId=0&gameId=432&gameVersion={0}&index=0&pageSize=25&searchFilter={1}&sectionId=6&sort=0";
                string url = String.Format(baseUrl, version, modName);
                responseBody = await ModFetcher.client.GetStringAsync(url);
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }

            JsonDocument document = JsonDocument.Parse(responseBody);
            JsonElement root = document.RootElement.EnumerateArray().FirstOrDefault();
            string name = root.GetProperty("name").GetString();
            JsonElement latestFile = root.GetProperty("latestFiles").EnumerateArray().FirstOrDefault();
            string fileName = latestFile.GetProperty("fileName").GetString();
            string downloadUrl = latestFile.GetProperty("downloadUrl").GetString();

            return new Mod(name, fileName, downloadUrl);
        }

        public bool ConfirmModSelection()
        {
            Console.WriteLine("The following mods were found:");

            foreach (Mod mod in this.Mods)
            {
                Console.WriteLine(mod.Name);
            }

            Console.WriteLine("Would you like to use these mods for the mod pack? (Y/n)");
            string userInput = Console.ReadLine().ToLower();
            if (userInput == "y" || userInput == "yes" || userInput == "")
            {
                return true;
            }
            return false;
        }
    }
}

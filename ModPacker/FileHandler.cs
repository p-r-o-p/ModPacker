using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace ModPacker
{
    class FileHandler
    {
        private string PackName;
        private List<Mod> Mods;
        private string DownloadPath;

        public FileHandler(string packName, List<Mod> mods)
        {
            this.PackName = packName;
            this.Mods = mods;
            this.DownloadPath = String.Concat(Path.GetTempPath(), "/ModPacker/", this.PackName);
            Directory.CreateDirectory(this.DownloadPath);
        }

        ~FileHandler()
        {
            Directory.Delete(this.DownloadPath, true);
        }

        public void DownloadModFiles()
        {
            WebClient client = new WebClient();
            foreach (Mod mod in this.Mods)
            {
                string filePath = String.Concat(this.DownloadPath, "/", mod.FileName);
                try
                {
                    client.DownloadFile(mod.DownloadUrl, filePath);
                }
                catch (WebException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void ZipModFiles()
        {
            string destination = String.Concat(Directory.GetCurrentDirectory(), "/", this.PackName, ".zip");
            if (File.Exists(destination))
            {
                File.Delete(destination);
            }
            try
            {
                ZipFile.CreateFromDirectory(this.DownloadPath, destination);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

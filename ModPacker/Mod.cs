using System;
using System.Collections.Generic;
using System.Text;

namespace ModPacker
{
    class Mod
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string DownloadUrl { get; set; }

        public Mod(string name, string file_name, string download_url)
        {
            this.Name = name;
            this.FileName = file_name;
            this.DownloadUrl = download_url;
        }
    }
}

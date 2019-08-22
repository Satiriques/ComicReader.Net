using ComicReader.Net.Common.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ComicReader.Net.Common.Classes
{
    public class UserConfig : IUserConfig
    {
        private readonly static string _configPath = Path.Combine(Path.GetDirectoryName(
            Assembly.GetEntryAssembly().Location), "config.json");

        private UserConfig()
        {
        }

        public static UserConfig Load()
        {
            if (File.Exists(_configPath))
            {
                return Read();
            }
            return new UserConfig();
        }

        public List<string> Libraries { get; set; } = new List<string>();

        public void Save()
        {
            File.WriteAllText(_configPath, ToString());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        private static UserConfig Read()
        {
            return JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(_configPath));
        }
    }
}
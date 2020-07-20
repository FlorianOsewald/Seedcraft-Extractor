using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PoeSeedcraftPricer.DataModels
{
    public class SeedCraft
    {
        public string IdentifierString { get; set; }
        public Currency Price { get; set; }
        public string ShortDescription { get; set; }
        public SeedType Seed { get; set; }
        public CraftType Craft { get; set; }

        public static SeedCraft Find(List<SeedCraft> seedCrafts, string exportLine)
        {
            var targetCraft = exportLine.Substring(0, exportLine.Length - 15);
            if (seedCrafts.Any(sc => sc.IdentifierString == targetCraft))
                return seedCrafts.First(sc => sc.IdentifierString == targetCraft);
            else
                return new SeedCraft() { ShortDescription = "Unknown Craft.", Price = new Currency(-1, CurrencySymbol.Fusing) };
        }
        public static List<SeedCraft> Load()
        {
            
            using(var file = new StreamReader("SeedCrafts.json"))
            {
                return JsonConvert.DeserializeObject<List<SeedCraft>>(file.ReadToEnd());
            }
        }

        public static void Save(List<SeedCraft> seedCrafts)
        {
            using (var file = new StreamWriter("SeedCrafts.json"))
            {
                file.Write(JsonConvert.SerializeObject(seedCrafts, Formatting.Indented));
            }
        }
    }
}

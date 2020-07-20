using System;
using System.Collections.Generic;
using System.Text;

namespace PoeSeedcraftPricer.DataModels
{
    public class SavedCraft
    {
        public string CraftDescription { get; set; }
        public bool SkipCraft { get; set; }
        public Currency Price { get; set; }
        public bool UiEnabled { get; set; }

        public SavedCraft()
        {
            this.UiEnabled = false;
        }

        public SavedCraft(SeedCraft seedCraft)
        {
            this.CraftDescription = seedCraft.ShortDescription;
            this.SkipCraft = false;
            this.Price = seedCraft.Price;
            this.UiEnabled = true;
        }

        public SavedCraft(string craftExportLine)
        {
            //parse exportLine to determine type of craft. Look that up in the config for prices

            //remove the last 15 characters in order to remove the level and the crafted attribute
            this.CraftDescription = craftExportLine.Substring(0, craftExportLine.Length - 15);
            this.SkipCraft = false;
            this.Price = new Currency(20, CurrencySymbol.Chaos);
            this.UiEnabled = true;
        }

        public string GetNotePart()
        {
            return ConverterUtil.ConvertPriceToString(Price);
        }
    }
}

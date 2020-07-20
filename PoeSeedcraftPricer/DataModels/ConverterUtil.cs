using System;
using System.Collections.Generic;
using System.Text;

namespace PoeSeedcraftPricer.DataModels
{
    public static class ConverterUtil
    {
        public static string ConvertPriceToString(Currency price)
        {
            string currencySymbolName = "";
            switch(price.Symbol)
            {
                case CurrencySymbol.Alchemy:
                    {
                        currencySymbolName = "Alc";
                        break;
                    }
                case CurrencySymbol.Alteration:
                    {
                        currencySymbolName = "Alt";
                        break;
                    }
                case CurrencySymbol.Chaos:
                    {
                        currencySymbolName = "c";
                        break;
                    }
                case CurrencySymbol.Divine:
                    {
                        currencySymbolName = "divine";
                        break;
                    }
                case CurrencySymbol.Exalted:
                    {
                        currencySymbolName = "ex";
                        break;
                    }
                case CurrencySymbol.Fusing:
                    {
                        currencySymbolName = "fusing";
                        break;
                    }
            }
            return $"{price.Amount}{currencySymbolName}";
        }

        public static string GetNoteForCrafts(SavedCraft[] crafts)
        {
            var note = "#";
            foreach(var craft in crafts)
            {
                if(craft.Price != null)
                {
                    if (craft.SkipCraft)
                        note += "-/";
                    else
                        note += ConverterUtil.ConvertPriceToString(craft.Price) + "/";
                }   
            }
            //cut the last instance of /
            return note.Substring(0, note.Length - 1);
        }
    }
}

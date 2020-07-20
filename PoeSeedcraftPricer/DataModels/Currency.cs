using System;
using System.Collections.Generic;
using System.Text;

namespace PoeSeedcraftPricer.DataModels
{
    public class Currency
    {
        public int Amount { get; set; }
        public CurrencySymbol Symbol { get; set; }

        public Currency(int amnt, CurrencySymbol smbl)
        {
            this.Amount = amnt;
            this.Symbol = smbl;
        }
    }
}

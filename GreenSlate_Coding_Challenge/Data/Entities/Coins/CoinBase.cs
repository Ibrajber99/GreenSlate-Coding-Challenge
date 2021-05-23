using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Entities.Coins
{
    /// <summary>
    /// This base class that all other coins are derived from
    /// </summary>
    public abstract class CoinBase
    {
        private const double CoinDollarDevisor = 100.0d;

        public int CoinID { get; set; }


        public int CoinAmount { get; set; }

        public string CoinName { get; set; }

        public double CoinValueInCents { get; set; }

        public double CoinValueInDollars => CoinValueInCents > 0 
                        ? CoinValueInCents / CoinDollarDevisor : 0;
    }
}

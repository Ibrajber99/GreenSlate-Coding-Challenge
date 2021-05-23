using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Entities.Drinks
{
    /// <summary>
    /// Drink base all other drinks dervied from it
    /// </summary>
    public abstract class DrinkBase
    {
        private const double CoinDollarDevisor = 100.0d;

        public int DrinkID { get; set; }


        public int DrinkAmount { get; set; }

        public string DrinkName { get; set; }

        public double DrinkValueInCents { get; set; }

        public double DrinkValueInDollars => DrinkValueInCents > 0 ?
                        DrinkValueInCents / CoinDollarDevisor : 0;
    }
}

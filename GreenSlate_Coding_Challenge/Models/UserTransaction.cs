using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Models
{
    /// <summary>
    /// Class that records all necessary results after the purchase attempt happens in the vending machine.
    /// </summary>
    public class UserTransaction
    {
        public UserTransaction()
        {
            DrinksBought = new List<DrinkBase>();
            RemainingChange = new Dictionary<CoinsEnum.Coins, int>();
        }


        public double AmountInserted { get; set; }

        public List<DrinkBase> DrinksBought { get; set; }

        public Dictionary<CoinsEnum.Coins, int> RemainingChange { get; set; }

        public bool IsEnoughChange { get; set; }

        public bool IsEnoughDrinks { get; set; }
    }
}

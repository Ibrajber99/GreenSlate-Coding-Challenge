using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using System;
using System.Collections.Generic;

namespace GreenSlate_Coding_Challenge.Models.VendingMachine
{
    public class DrinksVendingMachine : MachineBase<DrinkBase>
    {
        public DrinksVendingMachine()
        {
            Products = new List<DrinkBase>();
            CoinInventory = new List<CoinBase>();
        }
    }
}

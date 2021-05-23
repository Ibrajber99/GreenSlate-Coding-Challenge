using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Models.VendingMachine
{
    /// <summary>
    /// Machine base. that other machines with different products can be derived from.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MachineBase<T>
    {
        public List<T> Products { get; set; }

        public List<CoinBase> CoinInventory { get; set; }
    }
}

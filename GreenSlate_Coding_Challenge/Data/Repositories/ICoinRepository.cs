using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Repositories
{
    /// <summary>
    /// Contract for Coin repo
    /// </summary>
    public interface ICoinRepository
    {
        List<CoinBase> GetCoins();

        bool Update(List<CoinBase>coins);
           
    }
}

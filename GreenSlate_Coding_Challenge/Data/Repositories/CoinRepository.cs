using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Repositories
{
    /// <summary>
    /// Mock class for a repository. all data are in memory (this class is implemented as a singleton for data persistancey)
    /// </summary>
    public class CoinRepository : ICoinRepository
    {
        private List<CoinBase> Coins;

        public CoinRepository()
        {
            Coins = new List<CoinBase>
            {
                new CentCoin{CoinID =1,CoinAmount=100,CoinName=CoinsEnum.Coins.CENTS.ToString(),CoinValueInCents=1},
                new PennyCoin{ CoinID=2,CoinAmount=10,CoinName=CoinsEnum.Coins.PENNY.ToString(),CoinValueInCents=5},
                new NickelCoin{ CoinID=3,CoinAmount=5,CoinName=CoinsEnum.Coins.NICKLE.ToString(),CoinValueInCents=10},
                new QuarterCoin{ CoinID=3,CoinAmount=25,CoinName=CoinsEnum.Coins.QUARTERS.ToString(),CoinValueInCents=25}
            };
        }

        /// <summary>
        /// Ordering the coin values and reversing it for DESC order.
        /// </summary>
        /// <returns></returns>
        public List<CoinBase> GetCoins()
        {
            Coins.Sort((a, b) => 
                a.CoinValueInCents.CompareTo(b.CoinValueInCents));

            Coins.Reverse();

            return Coins;
        }

        /// <summary>
        /// Updaing the value of the list (mocking Update in actual Database)
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        public bool Update(List<CoinBase> coins)
        {
            Coins = new List<CoinBase>(coins);
            return true;
        }
    }
}

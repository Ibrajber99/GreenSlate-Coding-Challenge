using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Repositories
{
    /// <summary>
    /// Mock class for a repository. all data are in memory (this class is implemented as a singleton for data persistancey)
    /// </summary>
    public class DrinkRepository : IDrinkRepository
    {
        private List<DrinkBase> Drinks;

        public DrinkRepository()
        {
            Drinks = new List<DrinkBase>
            {
                new CokeDrink{DrinkID = 1,DrinkAmount=5,DrinkName=DrinksEnum.Drinks.COKE.ToString(),DrinkValueInCents=25},
                new PepsiDrink{ DrinkID=2,DrinkAmount=15,DrinkName=DrinksEnum.Drinks.PEPSI.ToString(),DrinkValueInCents=36},
                new SodaDrink{ DrinkID=3,DrinkAmount=45,DrinkName=DrinksEnum.Drinks.SODA.ToString(),DrinkValueInCents=3}
            };
        }

        public List<DrinkBase> GetDrinks()
        {
            return Drinks;
        }

        /// <summary>
        /// Updaing the value of the list (mocking Update in actual Database)
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        public bool Update(List<DrinkBase> drinks)
        {
            Drinks = new List<DrinkBase>(drinks);
            return true;
        }
    }
}

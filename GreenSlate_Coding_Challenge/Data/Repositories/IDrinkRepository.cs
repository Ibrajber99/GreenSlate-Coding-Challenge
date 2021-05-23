using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSlate_Coding_Challenge.Data.Repositories
{
    /// <summary>
    /// Contract for coin repo.
    /// </summary>
    public interface IDrinkRepository
    {
        List<DrinkBase> GetDrinks();

        bool Update(List<DrinkBase> drinks);
    }
}

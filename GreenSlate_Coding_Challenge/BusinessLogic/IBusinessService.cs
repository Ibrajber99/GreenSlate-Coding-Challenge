using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using GreenSlate_Coding_Challenge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace GreenSlate_Coding_Challenge.BusinessLogic
{
    /// <summary>
    /// Business logic layer contract
    /// </summary>
    public interface IBusinessService
    {
        List<SelectListItem> ConvertCoinsToSelectList();

        List<SelectListItem> ConvertDrinksToSelectList();

        List<DrinkBase> ConvertSelectListToDrinks(List<SelectListItem> selectedDrinks);

        DrinkBase GetDrinkInstance(string drinkName);

        UserTransaction ConvertValuesToTransaction(List<SelectListItem> insertedCoins,
                        List<SelectListItem> selectedDrinks,double price);

        Dictionary<CoinsEnum.Coins, int> GetChangeBreakDown(double price);

        List<DrinkBase> GetUpdatedDrinkInventory(List<DrinkBase> boughtDrinks);

        List<CoinBase> GetUpdatedCoinInventory(Dictionary<CoinsEnum.Coins, int> changeBreakDown);

        double GetAmountInserted(List<SelectListItem> amountInserted);

        bool IsAnyDrinksSelected(List<SelectListItem> selectedDrinks);

        bool IsAnyCoinsInserted(List<SelectListItem> insertedCoins);

        bool IsEnoughChange(double price);

        bool IsEnoughDrinks(List<DrinkBase> boughtDrinks);

        bool CompareInsetredCoinsToPrice(List<SelectListItem> insertedCoins, double price);

        List<string> GetTransactionMessage(UserTransaction transaction);

        List<string> GetSuccessfullTransactionBreakDown(UserTransaction transaction);
    }
}

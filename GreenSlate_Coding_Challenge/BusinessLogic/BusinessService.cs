using GreenSlate_Coding_Challenge.Data.Entities.Coins;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using GreenSlate_Coding_Challenge.Data.Repositories;
using GreenSlate_Coding_Challenge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenSlate_Coding_Challenge.BusinessLogic
{
    /// <summary>
    /// This class handels all logical operations and conversions between different objects.
    /// </summary>
    public class BusinessService : IBusinessService
    {
        private readonly IDrinkRepository _DrinkRepo;
        private readonly ICoinRepository _CoinRepo;

        public BusinessService(IDrinkRepository drinkRepo,
            ICoinRepository coinRepo)
        {
            _DrinkRepo = drinkRepo;
            _CoinRepo = coinRepo;
        }

        public List<SelectListItem> ConvertCoinsToSelectList()
        {
            var coinsSelectList = new List<SelectListItem>();
            var coinsList = _CoinRepo.GetCoins();

            foreach (var coin in coinsList)
            {
                coinsSelectList.Add
                    (new SelectListItem { Text = coin.CoinName,Selected=true });
            }

            return coinsSelectList;
        }

        public List<SelectListItem> ConvertDrinksToSelectList()
        {
            var drinksSelectList = new List<SelectListItem>();
            var drinksList = _DrinkRepo.GetDrinks();

            foreach (var drink in drinksList)
            {
                drinksSelectList.Add
                    (new SelectListItem { Text = drink.DrinkName,Selected = true });
            }

            return drinksSelectList;
        }

        public List<DrinkBase> ConvertSelectListToDrinks(List<SelectListItem> selectedDrinks)
        {
            var drinksList = new List<DrinkBase>();
            foreach (var drink in selectedDrinks)
            {
                var drinkInstance = GetDrinkInstance(drink.Text);

                if (drinkInstance != null)
                {
                    if (drink.Value != null)
                    {
                        drinkInstance.DrinkName = drink.Text;
                        drinkInstance.DrinkAmount = Convert.ToInt32(drink.Value);
                        drinksList.Add(drinkInstance);
                    }
                }
            }

            return drinksList;
        }


        public DrinkBase GetDrinkInstance(string drinkName)
        {
            DrinkBase instance;
            DrinksEnum.Drinks enumVal;
            Enum.TryParse(drinkName, out enumVal);

            switch (enumVal)
            {
                case DrinksEnum.Drinks.COKE:
                    instance = new CokeDrink();
                    break;
                case DrinksEnum.Drinks.PEPSI:
                    instance = new PepsiDrink();
                    break;
                case DrinksEnum.Drinks.SODA:
                    instance = new SodaDrink();
                    break;
                default:
                    instance = null;
                    break;
            }
            return instance;
        }


        public UserTransaction ConvertValuesToTransaction(List<SelectListItem> insertedCoins,
            List<SelectListItem> selectedDrinks, double price)
        {
            var transaction = new UserTransaction();

            transaction.DrinksBought = ConvertSelectListToDrinks(selectedDrinks);
            transaction.AmountInserted = GetAmountInserted(insertedCoins);
            transaction.IsEnoughDrinks = IsEnoughDrinks(transaction.DrinksBought);


            var changeValue = Math.Abs(transaction.AmountInserted - price);

            transaction.RemainingChange = GetChangeBreakDown(changeValue);
            transaction.IsEnoughChange = IsEnoughChange(changeValue);


            return transaction;
        }


        public Dictionary<CoinsEnum.Coins, int> GetChangeBreakDown(double price)
        {
            price /= 100.0d;
            var coinsList = _CoinRepo.GetCoins();
            var changeBreakDown = new Dictionary<CoinsEnum.Coins, int>();


            foreach (var coin in coinsList)
            {
                CoinsEnum.Coins enumVal;
                if(Enum.TryParse(coin.CoinName, out enumVal))
                {
                    var changeAmount = (int)(price / coin.CoinValueInDollars);
                    price %= coin.CoinValueInDollars;

                    if (changeAmount > 0)
                    {
                        changeBreakDown.Add(enumVal, changeAmount);
                    }
                }
            }

            return changeBreakDown;
        }


        public double GetAmountInserted(List<SelectListItem> amountInserted)
        {
            var coinsList = _CoinRepo.GetCoins();

            double totalInserted = 0;
            if (amountInserted != null)
            {
                foreach (var coin in amountInserted)
                {
                    if (coin.Value != null)
                    {
                        var coinFound = coinsList.FirstOrDefault
                            (c => c.CoinName == coin.Text);

                        if (coinFound != null)
                        {
                            double coinCardinality = Convert.ToDouble(coin.Value);
                            totalInserted += coinFound.CoinValueInCents * coinCardinality;
                        }
                    }
                }
            }
            return totalInserted;
        }

        public bool IsEnoughChange(double price)
        {
            var coinsList = _CoinRepo.GetCoins();
            var changeBreakDown = GetChangeBreakDown(price);

            foreach (var coin in coinsList)
            {
                CoinsEnum.Coins enumVal;
                if (Enum.TryParse(coin.CoinName, out enumVal))
                {
                    if (changeBreakDown.ContainsKey(enumVal))
                    {
                        int coinReturnVal;
                        if(changeBreakDown.TryGetValue(enumVal,out coinReturnVal))
                        {
                            if(coin.CoinAmount - coinReturnVal < 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;

        }

        public bool IsEnoughDrinks(List<DrinkBase> boughtDrinks)
        {
            var drinksList = _DrinkRepo.GetDrinks();
            foreach (var drink in drinksList)
            {
                var boughtDrinkFound = boughtDrinks.FirstOrDefault
                    (d => d.DrinkName == drink.DrinkName);

                if(boughtDrinkFound != null)
                {
                    if(drink.DrinkAmount - boughtDrinkFound.DrinkAmount < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsAnyDrinksSelected(List<SelectListItem> selectedDrinks)
        {
            if(selectedDrinks != null)
            {
                return selectedDrinks.Any(d => d.Value != null);
            }
            return false;
        }

        public bool IsAnyCoinsInserted(List<SelectListItem> insertedCoins)
        {
            if(insertedCoins != null)
            {
                return insertedCoins.Any(c => c.Value != null);
            }
            return false;
        }

        public bool CompareInsetredCoinsToPrice(List<SelectListItem> insertedCoins, double price)
        {
            var amountInserted = GetAmountInserted(insertedCoins);
            return amountInserted >= price;
        }

        public List<DrinkBase> GetUpdatedDrinkInventory(List<DrinkBase> boughtDrinks)
        {
            var drinksList = new List<DrinkBase>(_DrinkRepo.GetDrinks());

            foreach (var drink in drinksList)
            {
                var boughtDrinkFound = boughtDrinks.FirstOrDefault
                   (d => d.DrinkName == drink.DrinkName);

                if (boughtDrinkFound != null)
                {
                    drink.DrinkAmount = Math.Abs
                      (drink.DrinkAmount - boughtDrinkFound.DrinkAmount);
                }
            }
            return drinksList;
        }

        public List<CoinBase> GetUpdatedCoinInventory(Dictionary<CoinsEnum.Coins, int> changeBreakDown)
        {
            var coinsList = new List<CoinBase>(_CoinRepo.GetCoins());
            foreach (var coin in coinsList)
            {
                CoinsEnum.Coins enumVal;
                if (Enum.TryParse(coin.CoinName, out enumVal))
                {
                    if (changeBreakDown.ContainsKey(enumVal))
                    {
                        int coinReturnVal;
                        if (changeBreakDown.TryGetValue(enumVal, out coinReturnVal))
                        {
                            coin.CoinAmount =Math.Abs
                                (coin.CoinAmount - coinReturnVal);
                        }
                    }
                }
            }
            return coinsList;
        }

        public List<string> GetTransactionMessage(UserTransaction transaction)
        {
            var messages = new List<string>();
            if(transaction != null)
            {
                if (!transaction.IsEnoughChange)
                {
                    messages.Add("Not sufficient change in the inventory");
                }else if (!transaction.IsEnoughDrinks)
                {
                    messages.Add("Drink is sold out, your purchase cannot be processed");
                }
                else
                {
                    messages.AddRange(GetSuccessfullTransactionBreakDown(transaction));
                }
            }
            return messages;
        }

        public List<string> GetSuccessfullTransactionBreakDown(UserTransaction transaction)
        {
            var successMessage = new List<string>();
            successMessage.Add("Drinks Bought: \n");
            foreach (var drink in transaction.DrinksBought)
            {
                successMessage.Add($"{drink.DrinkName} amount: {drink.DrinkAmount}\n");
            }
            successMessage.Add("\n\n");
            successMessage.Add("Change returned: \n");

            foreach (var coin in transaction.RemainingChange)
            {
                successMessage.Add($"{coin.Key} return: {coin.Value}\n");
            }
            successMessage.Add("\n\n");

            return successMessage;
        }
    }
}

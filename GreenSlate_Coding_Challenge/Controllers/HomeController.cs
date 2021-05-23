using GreenSlate_Coding_Challenge.BusinessLogic;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using GreenSlate_Coding_Challenge.Data.Repositories;
using GreenSlate_Coding_Challenge.Models;
using GreenSlate_Coding_Challenge.Models.InputModels;
using GreenSlate_Coding_Challenge.Models.VendingMachine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;

namespace GreenSlate_Coding_Challenge.Controllers
{
    /// <summary>
    /// Controller that handels all the requests
    /// </summary>
    public class HomeController : Controller
    {
        //Required objects
        private readonly InputViewModel _InputModel;
        private readonly MachineBase<DrinkBase> _DrinkMachine;
        private readonly IDrinkRepository _DrinkRepo;
        private readonly ICoinRepository _CoinRepo;
        private readonly IBusinessService _BusinessService;
        private UserTransaction _UserTransaction;

        public HomeController
            (InputViewModel inputModel,
            MachineBase<DrinkBase>drinkMachine,
            IDrinkRepository drinkRepo,
            ICoinRepository coinRepo,
            IBusinessService businessService)
        {
            _InputModel = inputModel;
            _DrinkMachine = drinkMachine;
            _DrinkRepo = drinkRepo;
            _CoinRepo = coinRepo;
            _BusinessService = businessService;
        }

        /// <summary>
        /// Sets all necessary values before forwarding the model to the view
        /// </summary>
        private void SetViewModelEntryValues()
        {
            _DrinkMachine.Products = _DrinkRepo.GetDrinks();
            _DrinkMachine.CoinInventory = _CoinRepo.GetCoins();

            _InputModel.CoinsInserted = _BusinessService.ConvertCoinsToSelectList();
            _InputModel.DrinksBought = _BusinessService.ConvertDrinksToSelectList();
            _InputModel.Machine = (DrinksVendingMachine)_DrinkMachine;
        }

        /// <summary>
        /// Forwarding the view model to the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DrinksMachine()
        {
            try
            {
                SetViewModelEntryValues();

                return View(_InputModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is the post back method from the view. All validation and logical forwarding is contained here
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DrinksMachine(InputViewModel model)
        {
            try
            {
                //Checking initial possible errors
                if (!_BusinessService.IsAnyDrinksSelected(model.DrinksBought))
                {
                    ModelState.AddModelError(string.Empty,
                        "Error. Please select at least 1 drink");
                }

                if (!_BusinessService.IsAnyCoinsInserted(model.CoinsInserted))
                {
                    ModelState.AddModelError(string.Empty,
                        "Error. Coins must be inserted and it cannot be 0 or less.");
                }
                if (!_BusinessService.CompareInsetredCoinsToPrice(model.CoinsInserted, model.Total))
                {
                    ModelState.AddModelError(string.Empty,
                        "Error. amount inserted is less than the total price.");
                }

                //Checkign if the modele state is valid before proceeding
                if (ModelState.IsValid)
                {
                    //Getting the transaction
                    _UserTransaction = _BusinessService.ConvertValuesToTransaction
                                            (model.CoinsInserted, model.DrinksBought, model.Total);

                    //Update the repos id the transaction is successfull.
                    if (_UserTransaction.IsEnoughChange && _UserTransaction.IsEnoughDrinks)
                    {
                        var updatedDrinkInventory = _BusinessService
                                    .GetUpdatedDrinkInventory(_UserTransaction.DrinksBought);

                        var updatedCoinInventory = _BusinessService
                                .GetUpdatedCoinInventory(_UserTransaction.RemainingChange);

                        _DrinkRepo.Update(updatedDrinkInventory);
                        _CoinRepo.Update(updatedCoinInventory);
                    }


                    SetViewModelEntryValues();

                    //Inserting messages if there is any
                    _InputModel.Messages = _BusinessService
                        .GetTransactionMessage(_UserTransaction);


                    ModelState.Clear();//Clearing the inserted values for new entry.

                    return View(_InputModel);
                }

                SetViewModelEntryValues();

                return View(_InputModel);
            }
            catch (Exception)
            {
                SetViewModelEntryValues();
                return View("DrinksMachine", _InputModel);
            }
          
        }
    }
}

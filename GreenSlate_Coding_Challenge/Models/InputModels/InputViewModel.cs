using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using GreenSlate_Coding_Challenge.Models.VendingMachine;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GreenSlate_Coding_Challenge.Models.InputModels
{

   /// <summary>
   /// Viewmodel that has all required objects for the interactivity
   /// </summary>
    public class InputViewModel
    {
        public InputViewModel(DrinksVendingMachine machine)
        {
            Machine = machine;
            DrinksBought = new List<SelectListItem>();
            CoinsInserted = new List<SelectListItem>();
            Messages = new List<string>();
            Total = 0.0d;
        }
        public InputViewModel()
        {

        }



        public UserTransaction Transaction { get; set; }

        public DrinksVendingMachine Machine { get; set; }

        public List<SelectListItem> DrinksBought { get; set; }

        public List<SelectListItem> CoinsInserted { get; set; }

        public List<string> Messages { get; set; }

        public double Total { get; set; }
    }
}

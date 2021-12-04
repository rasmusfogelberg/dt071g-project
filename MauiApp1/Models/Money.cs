using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Money : BindableObject
    {
        // General variables
        private int _amount = 0;
        private string amountCountDisplay = "0 Monies";

        // Keeping track of the amount of money
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // Property to display money
        public string AmountCountDisplay
        {
            get => amountCountDisplay;
            set
            {
                amountCountDisplay = value;
                OnPropertyChanged();
            }
        }
    }
}

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
        private int _amount = 0;
        private string amountCountDisplay = "0 Monies";

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

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

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    
    public class Bar : BindableObject
    {
        // General variables for Bar entity
        private int barCount = 0;
        private string barCountDisplay = "Bar: 0";
        private int barSlider = 0;
        private bool barSwitch = false;
        private string _barTotalPerSecDisplay = "0 generated/s";

        // Mini auto-smelter variables
        private int _barMiniCount = 0;
        private string _barMiniDisplay = "Mini: 0";
        private double _barMiniCost = 600;
        private string _barMiniCostDisplay = "Cost: 600";
        private string _barMiniPerSecDisplay = "Bar/s: 0";
        private int _barMiniPerSec;

        // Mega auto-smelter variables
        private int _barMegaCount = 0;
        private string _barMegaDisplay = "Mega: 0";
        private double _barMegaCost = 1500;
        private string _barMegaCostDisplay = "Cost: 1500";
        private string _barMegaPerSecDisplay = "Bar/s: 0";
        private int _barMegaPerSec;

        // Super auto-smelter variables
        private int _barSuperCount = 0;
        private string _barSuperDisplay = "Super: 0";
        private double _barSuperCost = 2000;
        private string _barSuperCostDisplay = "Cost: 2000";
        private string _barSuperPerSecDisplay = "Bar/s: 0";
        private int _barSuperPerSec;

        // Provide reference to Money class/model in Bar
        private Money _money;
        public Bar(Money money)
        {
            _money = money;
        }

        // Keeping track of the amount of Bars
        public int BarCount
        {
            get { return barCount; }
            set { barCount = value; }
        }

        // The display property used to show how many bars there
        // currently are
        public string BarCountDisplay
        {
            get => barCountDisplay;
            set
            {
                barCountDisplay = value;
                OnPropertyChanged();
            }
        }

        // Method to increase bar count by 1 when called as long
        // as oreCount is larger than 1 (1 bar = 2 ore)
        public void OnBarIncreaseBy1()
        {
           
                barCount++;
                BarCountDisplay = $"Bar: {barCount}";
            
        }

        // Method to sell bar for money
        public void SellBar(int increaseBy)
        {
            if (BarCount >= increaseBy)
            {
                // Money is increased by 3 for each bar
                _money.Amount = _money.Amount + (increaseBy * 3);
                BarCount = BarCount - increaseBy;
                _money.AmountCountDisplay = $"{_money.Amount} Monies";
                BarCountDisplay = $"Bar: {BarCount}";
            }
        }

        // Slider for auto-selling bars
        public int BarSlider
        {
            get => barSlider;
            set
            {
                barSlider = value;
                OnPropertyChanged();
            }
        }

        // On and off toggle to auto-sell bars
        public bool BarSwitch
        {
            get => barSwitch;
            set
            {
                barSwitch = value;

                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                    if (BarSlider <= BarCount)
                    {
                        _money.Amount = _money.Amount + (barSlider * 3);
                        BarCount = BarCount - barSlider;
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        BarCountDisplay = $"Bar: {BarCount}";

                        OnPropertyChanged();

                    }
                    // Since Device.StartTimer will repeat if below value is true, it is
                    // connected to the _barSwitch value
                    return barSwitch;
                });
            }
        }

        // The current cost to buy auto-smelter. Will also increase
        // the cost by 25% when an auto-smelter is bought and provided
        // argument is set to true.
        public int Cost(bool check, double machineType)
        {
            double cost = machineType;
            if (check == true)
            {
                cost = cost * 1.25;
            }
            int roundedCost = (int)Math.Round(cost);
            return roundedCost;
        }

        // Display for how many bars are generated per second
        public string BarTotalPerSecDisplay
        {
            get => _barTotalPerSecDisplay;
            set
            {
                _barTotalPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        // Method to update values when an auto-smelter is bought
        // Uses a switch case that gets its parameter from the
        // CommandParameter in MainPage.xaml.
        public void PurchaseAutoSmelter(string smelterType)
        {
            // Based on the smelterType the following is done.
            switch (smelterType)
            {
                case "Mini":
                    int currentBarMiniCost = Cost(false, _barMiniCost);
                    if (_money.Amount >= currentBarMiniCost)
                    {
                        _barMiniCount++; // Increment count of Mini-smelters
                        _money.Amount = _money.Amount - currentBarMiniCost; // Update money amount
                        BarMiniDisplay = $"Mini: {_barMiniCount}"; 
                        BarMiniCostDisplay = $"Cost: {Cost(true, _barMiniCost)}";
                        _barMiniCost = Cost(true, _barMiniCost); 
                        BarMiniPerSecDisplay = $"Bar/s: {_barMiniCount}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Update how many bars that are produced per second
                        BarMiniPerSec = _barMiniCount; 
                    }

                    break;

                case "Mega":
                    int currentBarMegaCost = Cost(false, _barMegaCost);
                    if (_money.Amount >= currentBarMegaCost)
                    {
                        _barMegaCount++;
                        _money.Amount = _money.Amount - currentBarMegaCost;
                        BarMegaDisplay = $"Mega: {_barMegaCount}";
                        BarMegaCostDisplay = $"Cost: {Cost(true, _barMegaCost)}";
                        _barMegaCost = Cost(true, _barMegaCost);
                        BarMegaPerSecDisplay = $"Bar/s: {_barMegaCount * 2}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Mega-smelters produces 2 bars per smelter
                        BarMegaPerSec = (_barMegaCount * 2);
                    }

                    break;

                case "Super":
                    int currentBarSuperCost = Cost(false, _barSuperCost);
                    if (_money.Amount >= currentBarSuperCost)
                    {
                        _barSuperCount++;
                        _money.Amount = _money.Amount - currentBarSuperCost;
                        BarSuperDisplay = $"Super: {_barSuperCount}";
                        BarSuperCostDisplay = $"Cost: {Cost(true, _barSuperCost)}";
                        _barSuperCost = Cost(true, _barSuperCost);
                        BarSuperPerSecDisplay = $"Bar/s: {_barSuperCount * 4}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Super-smelters produces 4 bars per smelter 
                        BarSuperPerSec = (_barSuperCount *4);
                    }

                    break;
            }
        }

        /*
         * MINI AUTO-SMELTERS
         */

        // Displays number of mini auto-smelters currently owned
        public string BarMiniDisplay
        {
            get => _barMiniDisplay;
            set
            {
                _barMiniDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display the current cost of a mini-smelter
        public string BarMiniCostDisplay
        {
            get => _barMiniCostDisplay;
            set
            {
                _barMiniCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // How many bars per second the mini-smelters produce
        // This is used in the timer that provides bar
        // produced per second
        public int BarMiniPerSec
        {
            get => _barMiniPerSec;
            set
            {
                _barMiniPerSec = value;
            }
        }

        // Property to display how many bars the current number of mini-smelters
        // smelt per second
        public string BarMiniPerSecDisplay
        {
            get => _barMiniPerSecDisplay;
            set
            {
                _barMiniPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        /*
        * MEGA AUTO-SMELTERS
        */

        // Displays number of mega auto-smelters currently owned
        public string BarMegaDisplay
        {
            get => _barMegaDisplay;
            set
            {
                _barMegaDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display the current cost of a mega-smelter
        public string BarMegaCostDisplay
        {
            get => _barMegaCostDisplay;
            set
            {
                _barMegaCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // How many bars per second the mega-smelters produce
        // This is used in the timer that provides bar and bar
        // produced per second
        public int BarMegaPerSec
        {
            get => _barMegaPerSec;
            set
            {
                _barMegaPerSec = value;
            }
        }

        // Property to display how many bars the current number of mega-smelters
        // mine per second
        public string BarMegaPerSecDisplay
        {
            get => _barMegaPerSecDisplay;
            set
            {
                _barMegaPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        /*
        * SUPER AUTO-SMELTERS
        */

        // Displays number of super auto-smelters currently owned
        public string BarSuperDisplay
        {
            get => _barSuperDisplay;
            set
            {
                _barSuperDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display the current cost of a super-smelter
        public string BarSuperCostDisplay
        {
            get => _barSuperCostDisplay;
            set
            {
                _barSuperCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // How many bars per second the super-smelters produce
        // This is used in the timer that provides bar and bar
        // produced per second
        public int BarSuperPerSec
        {
            get => _barSuperPerSec;
            set
            {
                _barSuperPerSec = value;
            }
        }

        // Property to display how many bars the current number of super-smelters
        // mine per second
        public string BarSuperPerSecDisplay
        {
            get => _barSuperPerSecDisplay;
            set
            {
                _barSuperPerSecDisplay = value;
                OnPropertyChanged();
            }
        }
    }
}

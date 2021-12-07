using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Ore : BindableObject
    {
        // General Ore variables
        private int _oreCount = 0;
        private string _oreCountDisplay = "Ore: 0";
        private int _oreSlider = 0;
        private bool _oreSwitch = false;
        private string _oreTotalPerSecDisplay = "0 generated/s";
        
        // Mini auto-miner variables
        private int _oreMiniCount = 0;
        private string _oreMiniDisplay = "Mini: 0";
        private double _oreMiniCost = 50;
        private string _oreMiniCostDisplay = "Cost: 50";
        private string _oreMiniPerSecDisplay = "Ore/s: 0";
        private int _oreMiniPerSec;

        // Mega auto-miner variables
        private int _oreMegaCount = 0;
        private string _oreMegaDisplay = "Mega: 0";
        private double _oreMegaCost = 150;
        private string _oreMegaCostDisplay = "Cost: 150";
        private string _oreMegaPerSecDisplay = "Ore/s: 0";
        private int _oreMegaPerSec;

        // Super auto-miner variables
        private int _oreSuperCount = 0;
        private string _oreSuperDisplay = "Super: 0";
        private double _oreSuperCost = 300;
        private string _oreSuperCostDisplay = "Cost: 300";
        private string _oreSuperPerSecDisplay = "Ore/s: 0";
        private int _oreSuperPerSec;

        // Provide reference to Money class/model in Ore
        private Money _money;
        public Ore(Money money) {
            _money = money;
        }

        // Keeping track of the amount of Ore
        public int OreCount
        {
            get { return _oreCount; }
            set { _oreCount = value; }
        }

        // The display property called for showing how much
        // ore there currently is
        public string OreCountDisplay
        {
            get => _oreCountDisplay;
            set
            {
                if (value == _oreCountDisplay)
                    return;

                _oreCountDisplay = value;
                OnPropertyChanged();
            }
        }

        // Method to increase ore count by 1 when called
        public void OnOreIncreaseBy1()
        {
            _oreCount++;
            OreCountDisplay = $"Ore: {_oreCount}";
        }

        // Method that sells ore for money
        public void SellOre(int increaseBy)
        {
            if (OreCount >= increaseBy)
            {
                _money.Amount = _money.Amount + increaseBy;
                OreCount = OreCount - increaseBy;
                _money.AmountCountDisplay = $"{_money.Amount} Monies";
                OreCountDisplay = $"Ore: {OreCount}";
                
                OnPropertyChanged();
            }
        }

        // Slider for auto-selling ore
        public int OreSlider
        {
            get => _oreSlider;
            set
            {
                _oreSlider = value;
                OnPropertyChanged();
            }
        }

        // On and off toggle to auto-sell ore
        public bool OreSwitch
        {
            get => _oreSwitch;
            set
            {
                _oreSwitch = value;

                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                    if (_oreSlider <= OreCount)
                    {
                        _money.Amount = _money.Amount + _oreSlider;
                        OreCount = OreCount - _oreSlider;
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        OreCountDisplay = $"Ore: {OreCount}";

                        OnPropertyChanged();

                    }
                    // Since Device.StartTimer will repeat if below value is true, it is
                    // connected to the _oreSwitch value
                    return _oreSwitch;
                });
            }
        }

        // The current cost to buy auto-miner. Will also increase
        // the cost by 15% when an auto-miner is bought and provided
        // argument is set to true.
        public int Cost(bool check, double machineType)
        {
            double cost = machineType;
            if (check == true)
            {
                cost = cost * 1.15;
            }
            int roundedCost = (int)Math.Round(cost);
            return roundedCost;
        }

        // Display for how much ore is generated per second
        public string OreTotalPerSecDisplay
        {
            get => _oreTotalPerSecDisplay;
            set
            {
                _oreTotalPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        // Method to update values when an auto-miner is bought
        // Uses a switch case that gets its parameter from the
        // CommandParameter in MainPage.xaml.
        public void PurchaseAutoMiner(string minerType)
        {
            // Based on the minerType the following is done.
            switch (minerType)
            {
                case "Mini":
                    int currentOreMiniCost = Cost(false, _oreMiniCost);
                    if (_money.Amount >= currentOreMiniCost)
                    {
                        _oreMiniCount++; // Increment count of Mini-miners
                        _money.Amount = _money.Amount - currentOreMiniCost; // Update money amount
                        OreMiniDisplay = $"Mini: {_oreMiniCount}";
                        OreMiniCostDisplay = $"Cost: {Cost(true, _oreMiniCost)}";
                        _oreMiniCost = Cost(true, _oreMiniCost);
                        OreMiniPerSecDisplay = $"Ore/s: {_oreMiniCount}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Update how much ore that is produced per second
                        OreMiniPerSec = _oreMiniCount;
                    }
                    break;

                case "Mega":
                    int currentOreMegaCost = Cost(false, _oreMegaCost);
                    if (_money.Amount >= currentOreMegaCost)
                    {
                        _oreMegaCount++;
                        _money.Amount = _money.Amount - currentOreMegaCost;
                        OreMegaDisplay = $"Mega: {_oreMegaCount}";
                        OreMegaCostDisplay = $"Cost: {Cost(true, _oreMegaCost)}";
                        _oreMegaCost = Cost(true, _oreMegaCost);
                        OreMegaPerSecDisplay = $"Ore/s: {_oreMegaCount * 2}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Mega-miners produces 2 ore per miner
                        OreMegaPerSec = (_oreMegaCount * 2);
                    }
                    break;

                case "Super":
                    int currentOreSuperCost = Cost(false, _oreSuperCost);
                    if (_money.Amount >= currentOreSuperCost)
                    {
                        _oreSuperCount++;
                        _money.Amount = _money.Amount - currentOreSuperCost;
                        OreSuperDisplay = $"Super: {_oreSuperCount}";
                        OreSuperCostDisplay = $"Cost: {Cost(true, _oreSuperCost)}";
                        _oreSuperCost = Cost(true, _oreSuperCost);
                        OreSuperPerSecDisplay = $"Ore/s: {_oreSuperCount * 4}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        // Super-miners produces 4 ore per miner 
                        OreSuperPerSec = (_oreSuperCount * 4);
                    }
                    break;
            }
        }

        /*
         * MINI AUTO-MINERS
         */

        // Displays number of mini auto-miners currently owned
        public string OreMiniDisplay
        {
            get => _oreMiniDisplay;
            set
            {
                _oreMiniDisplay = value;
                OnPropertyChanged();
            }
        }
        
        // Property to display the current cost of a mini-miner
        public string OreMiniCostDisplay
        {
            get => _oreMiniCostDisplay;
            set
            {
                _oreMiniCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // How much ore per second the mini-miners produce
        // This is used in the timer that provides ore and bar
        // produced per second
        public int OreMiniPerSec
        {
            get => _oreMiniPerSec;
            set
            {
                _oreMiniPerSec = value;
            }
        }

        // Property to display how much ore the current number of mini-miners
        // mine per second
        public string OreMiniPerSecDisplay
        {
            get => _oreMiniPerSecDisplay;
            set
            {
                _oreMiniPerSecDisplay = value;
                OnPropertyChanged();
            }
        }
      
        
        /*
         * MEGA AUTO-MINERS
         */

        // Displays number of mega auto-miners currently owned
        public string OreMegaDisplay
        {
            get => _oreMegaDisplay;
            set
            {
                _oreMegaDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display the current cost of a mega-miner
        public string OreMegaCostDisplay
        {
            get => _oreMegaCostDisplay;
            set
            {
                _oreMegaCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display how much ore the current number of mega-miners
        // mine per second
        public string OreMegaPerSecDisplay
        {
            get => _oreMegaPerSecDisplay;
            set
            {
                _oreMegaPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        // How much ore per second the mega-miners produce
        // This is used in the timer that provides ore and bar
        // produced per second
        public int OreMegaPerSec
        {
            get => _oreMegaPerSec;
            set
            {
                _oreMegaPerSec = value;
            }
        }


        /*
         * Super AUTO-MINERS
         */

        // Displays number of super auto-miners currently owned
        public string OreSuperDisplay
        {
            get => _oreSuperDisplay;
            set
            {
                _oreSuperDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display the current cost of a super-miner
        public string OreSuperCostDisplay
        {
            get => _oreSuperCostDisplay;
            set
            {
                _oreSuperCostDisplay = value;
                OnPropertyChanged();
            }
        }

        // Property to display how much ore the current number of super-miners
        // mine per second
        public string OreSuperPerSecDisplay
        {
            get => _oreSuperPerSecDisplay;
            set
            {
                _oreSuperPerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        // How much ore per second the super-miners produce
        // This is used in the timer that provides ore and bar
        // produced per second
        public int OreSuperPerSec
        {
            get => _oreSuperPerSec;
            set
            {
                _oreSuperPerSec = value;
            }
        }
    }
}

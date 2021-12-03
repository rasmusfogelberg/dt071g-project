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
        private int oreCount = 0;
        private string oreCountDisplay = "Ore: 0";
        private int oreSlider = 0;
        private bool oreSwitch = false;
        private Money _money;

        public Ore(Money money) {
            _money = money;
        }

        public int OreCount
        {
            get { return oreCount; }
            set { oreCount = value; }
        }

        public string OreCountDisplay
        {
            get => oreCountDisplay;
            set
            {
                if (value == oreCountDisplay)
                    return;

                oreCountDisplay = value;
                OnPropertyChanged();
            }
        }
        public void OnOreIncreaseBy1()
        {
            oreCount++;
            OreCountDisplay = $"Ore: {oreCount}";
        }

        // Ore sellers
        public void OreToMoneyBy(int increaseBy)
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

        public int OreSlider
        {
            get => oreSlider;
            set
            {
                oreSlider = value;
                OnPropertyChanged();
            }
        }

        public bool OreSwitch
        {
            get => oreSwitch;
            set
            {
                oreSwitch = value;

                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                    if (oreSlider <= OreCount)
                    {
                        _money.Amount = _money.Amount + oreSlider;
                        OreCount = OreCount - oreSlider;
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        OreCountDisplay = $"Ore: {OreCount}";

                        OnPropertyChanged();

                    }
                    return oreSwitch;
                });
            }
        }

        private int oreMiniCount = 0;
        public void PurchaseAutoMiner(string minerType)
        {
            int currentCost = Cost(false);
            switch (minerType)
            {
                case "Mini":
                    
                    if (_money.Amount > currentCost)
                    {
                        oreMiniCount++;
                        _money.Amount = _money.Amount - currentCost;
                        MiniOreMachineDisplay = $"Mini: {oreMiniCount}";
                        MiniOreMachineCostDisplay = $"Cost: {Cost(true)}";
                        MiniOrePerSecDisplay = $"Ore/s: {oreMiniCount}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        OrePerSecMini = oreMiniCount;

                    }
                    break;

                case "Mega":
                    if (_money.Amount > currentCost)
                    {
                        oreMegaCount++;
                        _money.Amount = _money.Amount - currentCost;
                        MegaOreMachineDisplay = $"Mega: {oreMegaCount}";
                        MegaOreMachineCostDisplay = $"Cost: {Cost(true)}";
                        MegaOrePerSecDisplay = $"Ore/s: {oreMegaCount}";
                        _money.AmountCountDisplay = $"{_money.Amount} Monies";
                        OrePerSecMega = oreMegaCount * 2;

                    }
                    break;
                case "Super":

                    break;
            }


            
           
        }

        private string miniOreMachineDisplay = "Mini: 0";
        public string MiniOreMachineDisplay
        {
            get => miniOreMachineDisplay;
            set
            {
                miniOreMachineDisplay = value;
                OnPropertyChanged();
            }
        }

        private double _cost = 100;
        public int Cost(bool check)
        {
            if (check == true)
            {
                _cost = _cost * 1.15;
            }
            int roundedCost = (int)Math.Round(_cost);
            return roundedCost;
        }



        private string _miniOreMachineCostDisplay = "Cost: 100";
        public string MiniOreMachineCostDisplay
        {
            get => _miniOreMachineCostDisplay;
            set
            {
                _miniOreMachineCostDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _miniOrePerSecDisplay = "Ore/s: 0";
        public string MiniOrePerSecDisplay
        {
            get => _miniOrePerSecDisplay;
            set
            {
                _miniOrePerSecDisplay = value;
                OnPropertyChanged();
            }
        }

        private int _miniMinerCount = 0;
        public int MiniMinerCount
        {
            get => _miniMinerCount;
            set
            {
                _miniMinerCount = value;
            }
        }

        private int _orePerSecMini;
        public int OrePerSecMini
        {
            get => _orePerSecMini;
            set
            {
                _orePerSecMini = value;
            }
        }

        private int _megaMinerCount = 0;
        public int MegaMinerCount
        {
            get => _megaMinerCount;
            set
            {
                _megaMinerCount = value;
            }
        }

        private int _orePerSecMega;
        public int OrePerSecMega
        {
            get => _orePerSecMega;
            set
            {
                _orePerSecMega = value;
            }
        }

        private int _superMinerCount = 0;
        public int SuperMinerCount
        {
            get => _superMinerCount;
            set
            {
                _superMinerCount = value;
            }
        }

        private int _orePerSecSuper;
        public int OrePerSecSuper
        {
            get => _orePerSecSuper;
            set
            {
                _orePerSecSuper = value;
            }
        }
    }
}

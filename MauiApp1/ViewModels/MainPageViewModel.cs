using MauiApp1.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        // Instatiate reference to Money, Ore and Bar classes
        public Money _money = new Money();
        private Ore _ore;
        private Bar _bar;

        public Ore Ore
        {
            get => _ore;
        }

        public Bar Bar
        {
            get => _bar;
        }

        public Money Money
        {
            get => _money;
        }

        // Constructor responsible for passing reference of money to bar and ore
        // and starting timer for the game as well as wiring commands for the buttons
        // in the view MainPage.xaml
        public MainPageViewModel()
        {
            _ore = new Ore(_money);
            _bar = new Bar(_money);

            Tick();

            IncreaseOreClick = new Command(_ore.OnOreIncreaseBy1);
            IncreaseBarClick = new Command(() => 
            {
                
                if (_ore.OreCount > 1)
                {
                    _ore.OreCount = _ore.OreCount - 2;
                    _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                    _bar.OnBarIncreaseBy1();
                }
            });

            IncreaseMoneyBy1 = new Command(() => _ore.SellOre(1));
            IncreaseMoneyBy10 = new Command(() => _ore.SellOre(10));
            IncreaseMoneyBy100 = new Command(() => _ore.SellOre(100));
            IncreaseMoneyBy500 = new Command(() => _ore.SellOre(500));

            IncreaseMoneyBy3 = new Command(() => _bar.SellBar(1));
            IncreaseMoneyBy30 = new Command(() => _bar.SellBar(10));
            IncreaseMoneyBy300 = new Command(() => _bar.SellBar(100));
            IncreaseMoneyBy1500 = new Command(() => _bar.SellBar(500));

            BuyAutoOreMiner = new Command<string>((string minerType) =>
            {
                _ore.PurchaseAutoMiner(minerType);
            });

            BuyAutoBarSmelter = new Command<string>((string smelterType) =>
            {
                _bar.PurchaseAutoSmelter(smelterType);
            });

        }

        // Clicker buttons to produce ore and bars
        public ICommand IncreaseOreClick { get; }
        public ICommand IncreaseBarClick { get; }

        // Sell ore buttons
        public ICommand IncreaseMoneyBy1 { get; }
        public ICommand IncreaseMoneyBy10 { get; }
        public ICommand IncreaseMoneyBy100 { get; }
        public ICommand IncreaseMoneyBy500 { get; }

        // Sell bar buttons
        public ICommand IncreaseMoneyBy3 { get; }
        public ICommand IncreaseMoneyBy30 { get; }
        public ICommand IncreaseMoneyBy300 { get; }
        public ICommand IncreaseMoneyBy1500 { get; }

        // Buy buttons for auto-miners/smelters
        public ICommand BuyAutoOreMiner { get; }
        public ICommand BuyAutoBarSmelter { get; }

        // Method that starts when the game is run and keeps
        // track of the auto-generated ore and bars
        public void Tick()
        {
            // On every 1 second this code is executed
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Checking so enough ore is available to convert to bars
                // for the mini-smelters
                int oreNeededToConvertToMini = (_bar.BarMiniPerSec * 2);
                if (oreNeededToConvertToMini <= _ore.OreCount)
                {
                    _ore.OreCount = _ore.OreCount - (_bar.BarMiniPerSec * 2);
                    _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                    _bar.BarCount = _bar.BarCount + _bar.BarMiniPerSec;
                    _bar.BarCountDisplay = $"Bar: {_bar.BarCount}";
                }

                // Checking so enough ore is available to convert to bars
                // for the mega-smelters
                int oreNeededToConvertToMega = (_bar.BarMegaPerSec * 2);
                if (oreNeededToConvertToMega <= _ore.OreCount)
                {
                    _ore.OreCount = _ore.OreCount - (_bar.BarMegaPerSec * 2);
                    _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                    _bar.BarCount = _bar.BarCount + _bar.BarMegaPerSec;
                    _bar.BarCountDisplay = $"Bar: {_bar.BarCount}";
                }

                // Checking so enough ore is available to convert to bars
                // for the super-smelters
                int oreNeededToConvertToSuper = (_bar.BarSuperPerSec * 2);
                if (oreNeededToConvertToSuper <= _ore.OreCount)
                {
                    _ore.OreCount = _ore.OreCount - (_bar.BarSuperPerSec * 2);
                    _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                    _bar.BarCount = _bar.BarCount + _bar.BarSuperPerSec;
                    _bar.BarCountDisplay = $"Bar: {_bar.BarCount}";
                }

                // Adding togheter all the generated ore per second from all miners
                int totalOrePerSec = (_ore.OreMiniPerSec + _ore.OreMegaPerSec + _ore.OreSuperPerSec);
                // Updating ore count with value from ore miners
                _ore.OreCount = _ore.OreCount + totalOrePerSec;
                // Updating ore count display with value from ore miners
                _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                // Updating the display for total ore generated per second
                _ore.OreTotalPerSecDisplay = $"{totalOrePerSec} generated/s";

                // Adding togheter all the generated bars per second from all smelters
                int totalBarPerSec = (_bar.BarMiniPerSec + _bar.BarMegaPerSec + _bar.BarSuperPerSec);
                // Updating the display for total bars generated per second
                _bar.BarTotalPerSecDisplay = $"{totalBarPerSec} generated/s";

                return true;
            });
        }
    }
}

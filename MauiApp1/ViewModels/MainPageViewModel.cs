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

        public MainPageViewModel()
        {
            _ore = new Ore(_money);
            _bar = new Bar(_money);

            StartTimer();

            IncreaseOreClick = new Command(_ore.OnOreIncreaseBy1);
            IncreaseBarClick = new Command(() => 
            {
                _bar.OnBarIncreaseBy1(_ore.OreCount);
                if (_ore.OreCount > 1)
                {
                    _ore.OreCount = _ore.OreCount - 2;
                    _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";
                }
            });

            IncreaseMoneyBy1 = new Command(() => _ore.OreToMoneyBy(1));
            IncreaseMoneyBy10 = new Command(() => _ore.OreToMoneyBy(10));
            IncreaseMoneyBy100 = new Command(() => _ore.OreToMoneyBy(100));
            IncreaseMoneyBy500 = new Command(() => _ore.OreToMoneyBy(500));


            IncreaseMoneyBy3 = new Command(() => _bar.BarToMoneyBy(1));
            IncreaseMoneyBy30 = new Command(() => _bar.BarToMoneyBy(10));
            IncreaseMoneyBy300 = new Command(() => _bar.BarToMoneyBy(100));
            IncreaseMoneyBy1500 = new Command(() => _bar.BarToMoneyBy(500));

            BuyAutoMiner = new Command<string>((string minerType) =>
            {
                _ore.PurchaseAutoMiner(minerType);
            });


        }

        public ICommand IncreaseOreClick { get; }
        public ICommand IncreaseBarClick { get; }

        public ICommand IncreaseMoneyBy1 { get; }
        public ICommand IncreaseMoneyBy10 { get; }
        public ICommand IncreaseMoneyBy100 { get; }
        public ICommand IncreaseMoneyBy500 { get; }

        public ICommand IncreaseMoneyBy3 { get; }
        public ICommand IncreaseMoneyBy30 { get; }
        public ICommand IncreaseMoneyBy300 { get; }
        public ICommand IncreaseMoneyBy1500 { get; }
        public ICommand BuyAutoMiner { get; }

        public void StartTimer()
        {
         
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {    
                _ore.OreCount = _ore.OreCount + _ore.OrePerSecMini;
                _ore.OreCountDisplay = $"Ore: {_ore.OreCount}";

                return true;
            });
        }


    }
}

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
        private int barCount = 0;
        private string barCountDisplay = "Bar: 0";
        private int barSlider = 0;
        private bool barSwitch = false;
        private Money _money;

        public Bar(Money money)
        {
            _money = money;
        }
        public int BarCount
        {
            get { return barCount; }
            set { barCount = value; }
        }

        public string BarCountDisplay
        {
            get => barCountDisplay;
            set
            {
                if (value == barCountDisplay)
                    return;

                barCountDisplay = value;
                OnPropertyChanged();
            }
        }

        public void OnBarIncreaseBy1(int oreCount)
        {
            if (oreCount > 1)
            {
                barCount++;
               
                BarCountDisplay = $"Bar: {barCount}";
            
            }
        }

        public void BarToMoneyBy(int increaseBy)
        {
            if (BarCount >= increaseBy)
            {
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
                    return barSwitch;
                });
            }
        }
    }
}

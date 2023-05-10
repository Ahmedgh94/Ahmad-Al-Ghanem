using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace De_Fluitende_Fietser_Project_1
{
    /// <summary>
    /// Interaction logic for KassaSysteem.xaml
    /// </summary>
    public partial class KassaSysteem : Window
    {
        public KassaSysteem(string totalCost, List<string> bestelling)
        {
            InitializeComponent();
            TotalCostBox_KassaSysteem.Text = totalCost;
            BestelLijst_KassaSysteem.ItemsSource = bestelling;
        }

        private void CouponCode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CouponCode.Text == "code here")
            {
                CouponCode.Text = "";

            }
        }

        private void CouponCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CouponCode.Text == "")
            {
                CouponCode.Text = "code here";
            }
        }

        private void KassaPay_Click(object sender, RoutedEventArgs e)
        {
            if (MoneyBack.Text != "+€ 00,00")
            {
                MessageBox.Show($"U heeft betaald! uw terugaven is {MoneyBack.Text}");
            }


            this.Close();
        }

        private void CouponCodeApply_Click(object sender, RoutedEventArgs e)
        {
            decimal totalCost;
            if (!decimal.TryParse(TotalCostBox_KassaSysteem.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("nl-NL"), out totalCost))
            {
                MessageBox.Show("Invalid total cost");
                return;
            }

            decimal discount = 0.0m;
            string couponCode = CouponCode.Text;

            // Check if coupon code is valid
            if (couponCode == "PATR1CK")
            {
                discount = totalCost * 0.5m; // Apply 50% discount
            }

            // Apply discount and update TotalCostBox
            decimal discountedTotalCost = totalCost - discount;
            TotalCostBox_KassaSysteem.Text = $"€ {discountedTotalCost.ToString("0.00", CultureInfo.GetCultureInfo("nl-NL"))}";

                MessageBox.Show($"The total cost is {TotalCostBox_KassaSysteem.Text}");
        }



        private void VijfCent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 0,05", "Vijf cent");
        }

        private void TienCent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 0,10", "Tien cent");
        }

        private void TwintigCent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 0,20", "Twintig cent");
        }

        private void VijftigCent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 0,50", "Vijftig cent");
        }

        private void EenEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 1,00", "Een euro");
        }

        private void TweeEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 2,00", "Twee euro");
        }

        private void VijfEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 5,00", "Vijf euro");
        }

        private void TienEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 10,00", "Tien euro");
        }

        private void TwintigEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 20,00", "Twintig euro");
        }

        private void VijftigEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 50,00", "Vijftig euro");
        }

        private void HonderdEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 100,00", "Honderd euro");
        }

        private void TweehonderdEuro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddToListAndDecreaseTotalCost("€ 200,00", "Tweehonderd euro");
        }

        private void AddToListAndDecreaseTotalCost(string amount, string name)
        {
            if (MoneyBack.Text == "+€ 00,00")
            {
                decimal totalCost = ParseMoney(TotalCostBox_KassaSysteem.Text);
                decimal worth = ParseMoney(amount);

                if (worth > totalCost)
                {
                    decimal moneyBack = worth - totalCost;
                    MoneyBack.Text = FormatMoney(moneyBack);
                    TotalCostBox_KassaSysteem.Text = FormatMoney(0.0m);
                }
                else
                {
                    decimal remainingCost = totalCost - worth;
                    TotalCostBox_KassaSysteem.Text = FormatMoney(remainingCost);
                }

                MuntenLijst.Items.Add(name);
            }
        }
        private decimal ParseMoney(string moneyString)
        {
            decimal money;
            if (decimal.TryParse(moneyString, NumberStyles.Currency, CultureInfo.GetCultureInfo("nl-NL"), out money))
            {
                return money;
            }
            else
            {
                // handle invalid input
                return 0.0m;
            }
        }

        private string FormatMoney(decimal money)
        {
            string formatString = "€ #,##0.00";
            return money.ToString(formatString, CultureInfo.GetCultureInfo("nl-NL"));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace De_Fluitende_Fietser_Project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int aantalDagen;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            Verzekeringen.SelectionChanged += ResetTimer;
            Fietsen.SelectionChanged += ResetTimer;
            Services.SelectionChanged += ResetTimer;
            BestelButton.Click += ResetTimer;
            NieuweKlantButton.Click += ResetTimer;
            AfrekenButton.Click += ResetTimer;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ProgressBar.Value += 1;

            if (ProgressBar.Value == 60)
            {
                timer.Stop();
                this.Close();
            }
        }

        private void ResetTimer(object sender, EventArgs e) 
        {
            ProgressBar.Value = 0;
            timer.Stop();
            timer.Start();
        }

        private void Services_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Services.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)Services.SelectedItem;
                string content = selectedItem.Content.ToString();
                string[] parts = content.Split('€');
                string serviceName = parts[0].Trim();
                string servicePrice = "€ " + parts[1].Trim();

                Verzekeringen.SelectedIndex = -1;
                Fietsen.SelectedIndex = -1;

                EuroDPServicesName.Text = serviceName;
                EuroDPServices.Text = servicePrice + " Per dag";

                EuroDPVerzekeringenName.Text = String.Empty;
                EuroDPVerzekeringen.Text = "€ 00,00 Per dag";
                EuroDPFietsenName.Text = String.Empty;
                EuroDPFietsen.Text = "€ 00,00 Per dag";


            }
        }

        private void Verzekeringen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Verzekeringen.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)Verzekeringen.SelectedItem;
                string content = selectedItem.Content.ToString();
                string[] parts = content.Split('€');
                string verzekeringName = parts[0].Trim();
                string verzekeringPrice = "€ " + parts[1].Trim();

                Services.SelectedIndex = -1;
                Fietsen.SelectedIndex = -1;

                EuroDPVerzekeringenName.Text = verzekeringName;
                EuroDPVerzekeringen.Text = verzekeringPrice + " Per dag";

                EuroDPServicesName.Text = String.Empty;
                EuroDPServices.Text = "€ 00,00 Per dag";
                EuroDPFietsenName.Text = String.Empty;
                EuroDPFietsen.Text = "€ 00,00 Per dag";

            }
        }

        private void Fietsen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Fietsen.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)Fietsen.SelectedItem;
                string content = selectedItem.Content.ToString();
                string[] parts = content.Split('€');
                string fietsenName = parts[0].Trim();
                string fietsenPrice = "€ " + parts[1].Trim();

                Services.SelectedIndex = -1;
                Verzekeringen.SelectedIndex = -1;

                EuroDPFietsenName.Text = fietsenName;
                EuroDPFietsen.Text = fietsenPrice + " Per dag";


                EuroDPVerzekeringenName.Text = String.Empty;
                EuroDPVerzekeringen.Text = "€ 00,00 Per dag";
                EuroDPServicesName.Text = String.Empty;
                EuroDPServices.Text = "€ 00,00 Per dag";
            }

        }

        private void AantalDagen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AantalDagen.Text, out int aantalDagen))
            {
                if (EuroDPAantalDagen != null)
                {
                    if (aantalDagen == 1)
                    {
                        EuroDPAantalDagen.Text = "1 Dag";
                    }
                    else
                    {
                        EuroDPAantalDagen.Text = aantalDagen + " Dagen";
                    }
                    this.aantalDagen = aantalDagen;
                }
                else if (aantalDagen == 0)
                {
                    MessageBox.Show("Aantal dagen kan niet 0 zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    AantalDagen.Text = "";
                }
            }
        }


        private void BestelButton_Click(object sender, RoutedEventArgs e)
        {


            Random rand = new Random();
            int orderNumber = rand.Next(1000, 9999);

            if (Verzekeringen.SelectedIndex < 0 && Fietsen.SelectedIndex < 0 && Services.SelectedIndex < 0)
            {
                MessageBox.Show("Error: You must select an item from at least one of the following boxes: Verzekeringen, Fietsen, Services");
                return;
            }

            string servicePrice = GetPriceFromComboBox(Services);
            string verzekeringPrice = GetPriceFromComboBox(Verzekeringen);
            string fietsenPrice = GetPriceFromComboBox(Fietsen);

            if (int.TryParse(AantalDagen.Text, out int aantalDagen))
            {
                decimal totaal = decimal.Parse(servicePrice) + decimal.Parse(verzekeringPrice) + decimal.Parse(fietsenPrice);
                totaal *= aantalDagen;
                totaal /= 100;

                string fietsenName = EuroDPFietsenName.Text.ToString();
                string verzekeringName = EuroDPVerzekeringenName.Text.ToString();
                string serviceName = EuroDPServicesName.Text.ToString();

                if (Verzekeringen.SelectedIndex >= 0)
                {
                    string labelTextVerzekering = $"Bestelnummer:{orderNumber} {verzekeringName} voor {aantalDagen} {(aantalDagen == 1 ? "dag" : "dagen")} = € {totaal.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("nl-NL"))}";
                    BestelLijst.Items.Add(labelTextVerzekering);
                }

                else if (Services.SelectedIndex >= 0)
                {
                    string labelTextServices = $"Bestelnummer:{orderNumber} {serviceName} voor {aantalDagen} {(aantalDagen == 1 ? "dag" : "dagen")} = € {totaal.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("nl-NL"))}";
                    BestelLijst.Items.Add(labelTextServices);
                }

                else
                {
                    string labelTextFietsen = $"Bestelnummer:{orderNumber} {fietsenName} voor {aantalDagen} {(aantalDagen == 1 ? "dag" : "dagen")} = € {totaal.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("nl-NL"))}";
                    BestelLijst.Items.Add(labelTextFietsen);
                }




                decimal totalCost = 0;
                foreach (var item in BestelLijst.Items)
                {
                    string[] parts = item.ToString().Split('€');
                    totalCost += decimal.Parse(parts[1].Trim());
                }

                totalCost /= 100;

                TotalCostBox.Text = $"€ {totalCost.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("nl-NL"))}";
            }
        }

        private string GetPriceFromComboBox(ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                string content = selectedItem.Content.ToString();
                string[] parts = content.Split('€');
                return parts[1].Trim();
            }
            else
            {
                return "0";
            }
        }


        private void BestelLijst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                var result = MessageBox.Show("Bestelling annuleren?", "Annuleren", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    (sender as ListBox).Items.Remove(item);

                    RecalculateTotalCost();
                }
            }
        }

        private void RecalculateTotalCost()
        {
            decimal totalCost = 0;
            foreach (var item in BestelLijst.Items)
            {
                string[] parts = item.ToString().Split('€');
                totalCost += decimal.Parse(parts[1].Trim());
            }

            totalCost /= 100;

            TotalCostBox.Text = $"€ {totalCost.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("nl-NL"))}";
        }


        private void AfrekenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetAll()//Dit is de functie for het ressten van alles. Dit zorgt er voor dat alle boxes terug gezet worden naar het Origineel!
        {
            Verzekeringen.SelectedIndex = -1;
            Fietsen.SelectedIndex = -1;
            Services.SelectedIndex = -1;

            AantalDagen.Text = "";
            EuroDPAantalDagen.Text = "";
            EuroDPVerzekeringenName.Text = "";
            EuroDPVerzekeringen.Text = "€ 00,00 Per dag";
            EuroDPFietsenName.Text = "";
            EuroDPFietsen.Text = "€ 00,00 Per dag";
            EuroDPServicesName.Text = "";
            EuroDPServices.Text = "€ 00,00 Per dag";

            TotalCostBox.Text = "€ 00,00";
            AantalDagen.Text = "1";

            BestelLijst.Items.Clear();

            ProgressBar.Value = 0;
            timer.Stop();
            timer.Start();
        }

        private void NieuwKlantButton_Click(object sender, RoutedEventArgs e) //de eventhandler van NieuwKlantButton, de functie hier onder wordt uitgevoerd als je er op klikt
        {
            string ComboxEmptyNot = Fietsen.Text + Verzekeringen.Text + Services.Text;//  ComboxEmptyNot is een string met alle text naar string van alle Comboxes, zoals je ziet heb ik + tekengebruikt, dan hoef ik maar 1 string aan te maken 

            if (string.IsNullOrEmpty(ComboxEmptyNot))// if statement voor, IsNullOrEmpty geeft aan dat hij er 'geen' tekst in zit, zit er geen tekst in? dan geeft hij een melding
            {
                MessageBox.Show("U heeft geen nog geen product gekozen");// de MessageBox met de tekst er in
            }

            else // er zijn geen andere conditions, dus we gebruiken else. Nu als de ComBoxen wel 'gevuld' zijn dan is een MessageBox met een Ja of nee optie
            {
                MessageBoxResult result = MessageBox.Show("Heeft u betaald?", "Betaling verificatie", MessageBoxButton.YesNo, MessageBoxImage.Question);// hier maak ik het resultaat aan van de MessageBox samen met de Ja of Nee functie

                if (result == MessageBoxResult.Yes) //Is het resultaat ja? dan gebeurt het volgende
                {
                    ResetAll();// dit is een private void die alle boxes terug zet naar het Origineel! Dus alles komt er weer schoon uit te zien
                }
            }
        }
    }
}

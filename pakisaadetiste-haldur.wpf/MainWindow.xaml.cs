using pakisaadetiste_haldur.core;
using pakisaadetiste_haldur.core.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace pakisaadetiste_haldur.wpf
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Saadetis> _saadetised = new ObservableCollection<Saadetis>();

        private int _jargmineId = 1;
        private Saadetis? _valitudSaadetis;

        public MainWindow()
        {

            InitializeComponent();

            CmbLiik.ItemsSource = Enum.GetValues(typeof(SaadetiseLiik));
            CmbLinn.ItemsSource = Enum.GetValues(typeof(Linn));
            CmbTarneviis.ItemsSource = Enum.GetValues(typeof(Tarneviis));

            LstSaadetised.ItemsSource = _saadetised;

            _staatusTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };

            _staatusTimer.Tick += StaatusTimer_Tick;
            _staatusTimer.Start();

        }

        private readonly DispatcherTimer _staatusTimer;

        /// <summary>
        /// Lisab uue saadetise, kui vormi ei muudeta, või salvestab
        /// muudatused, kui nimekirjast on rida valitud.
        /// </summary>
        private void BtnPrimary_Click(object sender, RoutedEventArgs e)
        {
            if (!TryLoeVorm(out Saadetis saadetis, out string veateade))
            {
                NaitaViga(veateade);
                return;
            }

            if (_valitudSaadetis == null)
            {
                saadetis.Id = _jargmineId;
                _jargmineId++;

                saadetis.Staatus = SaadetiseStaatus.Registreeritud;

                _saadetised.Add(saadetis);
            }
            else
            {
                _valitudSaadetis.Liik = saadetis.Liik;
                _valitudSaadetis.Kaal = saadetis.Kaal;
                _valitudSaadetis.Linn = saadetis.Linn;
                _valitudSaadetis.Tarneviis = saadetis.Tarneviis;
                _valitudSaadetis.Aadress = saadetis.Aadress;
                _valitudSaadetis.Hind = saadetis.Hind;

                LstSaadetised.Items.Refresh();
            }

            LahtestaVorm();
        }

        /// <summary>
        /// Kustutab nimekirjast valitud saadetise, küsides eelnevalt kinnitust.
        /// </summary>
        private void BtnKustuta_Click(object sender, RoutedEventArgs e)
        {
            Saadetis? valitud = LstSaadetised.SelectedItem as Saadetis;

            if (valitud == null)
            {
                NaitaViga(pakisaadetiste_haldur.wpf.Properties.Resources.Err_NoSelection);
                return;
            }

            MessageBoxResult kinnitus = MessageBox.Show(
                pakisaadetiste_haldur.wpf.Properties.Resources.Confirm_Delete,
                pakisaadetiste_haldur.wpf.Properties.Resources.Confirm_Title,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (kinnitus != MessageBoxResult.Yes)
            {
                return;
            }

            _saadetised.Remove(valitud);
            LahtestaVorm();
        }

        /// <summary>
        /// Nimekirjast rea valimisel täidetakse vorm valitud saadetise
        /// andmetega ning lülitutakse muutmisrežiimi.
        /// </summary>
        private void LstSaadetised_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Saadetis? valitud = LstSaadetised.SelectedItem as Saadetis;

            if (valitud == null)
            {
                return;
            }

            _valitudSaadetis = valitud;

            CmbLiik.SelectedItem = valitud.Liik;
            TxtKaal.Text = valitud.Kaal.ToString(CultureInfo.CurrentCulture);
            CmbLinn.SelectedItem = valitud.Linn;
            CmbTarneviis.SelectedItem = valitud.Tarneviis;
            TxtAadress.Text = valitud.Aadress;
            TxtHind.Text = valitud.Hind.ToString(pakisaadetiste_haldur.wpf.Properties.Resources.Str_PriceDefault, CultureInfo.CurrentCulture) + pakisaadetiste_haldur.wpf.Properties.Resources.Euro;

            BtnPrimary.Content = pakisaadetiste_haldur.wpf.Properties.Resources.Str_ButtonUpdate;
            BtnKustuta.IsEnabled = true;
        }

        /// <summary>
        /// Loeb ja valideerib vormi väljad. Tagastab valmis Saadetis-objekti
        /// koos arvutatud hinnaga, kui kõik sisendid on korrektsed.
        /// </summary>
        private bool TryLoeVorm(out Saadetis saadetis, out string veateade)
        {
            saadetis = new Saadetis();
            veateade = string.Empty;

            if (!(CmbLiik.SelectedItem is SaadetiseLiik liik))
            {
                veateade = pakisaadetiste_haldur.wpf.Properties.Resources.Err_PackageTypeRequired;
                return false;
            }

            if (!(CmbLinn.SelectedItem is Linn linn))
            {
                veateade = pakisaadetiste_haldur.wpf.Properties.Resources.Err_CityRequired;
                return false;
            }

            if (!(CmbTarneviis.SelectedItem is Tarneviis tarneviis))
            {
                veateade = pakisaadetiste_haldur.wpf.Properties.Resources.Err_DeliveryRequired;
                return false;
            }

            if (!SisendiValideerija.ProoviValideeriKaal(
                    TxtKaal.Text,
                    out double kaal,
                    out veateade))
            {
                return false;
            }

            if (!SisendiValideerija.ProoviValideeriAadress(
                    TxtAadress.Text,
                    out veateade))
            {
                return false;
            }

            saadetis.Liik = liik;
            saadetis.Kaal = kaal;
            saadetis.Linn = linn;
            saadetis.Tarneviis = tarneviis;
            saadetis.Aadress = TxtAadress.Text.Trim();
            saadetis.Hind = HinnaKalkulaator.ArvutaHind(saadetis);

            return true;
        }

        /// <summary>
        /// Viib vormi tagasi "uue saadetise lisamise" algolekusse.
        /// </summary>
        private void LahtestaVorm()
        {
            _valitudSaadetis = null;

            CmbLiik.SelectedItem = null;
            TxtKaal.Text = string.Empty;
            CmbLinn.SelectedItem = null;
            CmbTarneviis.SelectedItem = null;
            TxtAadress.Text = string.Empty;
            TxtHind.Text = pakisaadetiste_haldur.wpf.Properties.Resources.Str_PriceDefault;

            BtnPrimary.Content = pakisaadetiste_haldur.wpf.Properties.Resources.Str_ButtonSend;
            BtnKustuta.IsEnabled = false;

            LstSaadetised.SelectedItem = null;
        }

        private static void NaitaViga(string sonum)
        {
            MessageBox.Show(sonum, pakisaadetiste_haldur.wpf.Properties.Resources.Err_Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void VormValja_Muudetud(object sender, RoutedEventArgs e)
        {
            if (TryLoeVorm(out Saadetis saadetis, out _))
            {
                TxtHind.Text = saadetis.Hind.ToString(
                    pakisaadetiste_haldur.wpf.Properties.Resources.Str_PriceDefault,
                    CultureInfo.CurrentCulture)
                    + pakisaadetiste_haldur.wpf.Properties.Resources.Euro;
            }
        }

        private void StaatusTimer_Tick(object? sender, EventArgs e)
        {
            foreach (Saadetis saadetis in _saadetised)
            {
                switch (saadetis.Staatus)
                {
                    case SaadetiseStaatus.Registreeritud:
                        saadetis.Staatus = SaadetiseStaatus.Töötlemisel;
                        break;

                    case SaadetiseStaatus.Töötlemisel:
                        saadetis.Staatus = SaadetiseStaatus.Te­el;
                        break;

                    case SaadetiseStaatus.Te­el:
                        saadetis.Staatus = SaadetiseStaatus.KohaleToimetatud;
                        break;

                    case SaadetiseStaatus.KohaleToimetatud:
                        break;
                }
            }

            LstSaadetised.Items.Refresh();
        }
    }
}

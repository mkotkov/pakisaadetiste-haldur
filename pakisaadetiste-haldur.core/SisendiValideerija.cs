using System.Globalization;

namespace pakisaadetiste_haldur.core.Services
{
    /// <summary>
    /// Vastutab ainult kasutaja sisendi kontrollimise eest.
    /// Veateated on siin praegu lihtsate konstandina; teisaldage need ühisesse ressursside projekti kui vaja.
    /// </summary>
    public static class SisendiValideerija
    {
        private const double MaksimaalneKaal = 100;
        private const int MinAadressiPikkus = 3;
        private const int MaksimaalneAadressiPikkus = 100;

        public static bool ProoviValideeriKaal(
            string sisend,
            out double kaal,
            out string veateade)
        {
            kaal = 0;
            veateade = string.Empty;

            if (!double.TryParse(
                    sisend,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out kaal))
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_WeightInvalid;
                return false;
            }

            if (kaal <= 0)
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_WeightRequired;
                return false;
            }

            if (kaal > MaksimaalneKaal)
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_MaksimaalneKaal;
                return false;
            }

            return true;
        }

        public static bool ProoviValideeriAadress(
            string aadress,
            out string veateade)
        {
            veateade = string.Empty;

            string puhastatud = aadress.Trim();

            if (string.IsNullOrWhiteSpace(puhastatud))
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_AddressRequired;
                return false;
            }

            if (puhastatud.Length < MinAadressiPikkus)
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_MinAddressLength;
                return false;
            }

            if (puhastatud.Length > MaksimaalneAadressiPikkus)
            {
                veateade = pakisaadetiste_haldur.core.Properties.Resources.Err_MaxAddressLength;
                return false;
            }

            return true;
        }
    }
}

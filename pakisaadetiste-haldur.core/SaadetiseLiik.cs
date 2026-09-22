using System;

namespace pakisaadetiste_haldur.core
{
    /// <summary>
    /// Saadetise liik. Mõjutab hinna arvutamisel kasutatavat kordajat.
    /// </summary>
    public enum SaadetiseLiik
    {
        Dokument,
        Pakk,
        Suurpakk
    }

    public static class SaadetiseLiikExtensions
    {
        /// <summary>
        /// Iga saadetise liigi hinnakordaja, mida rakendatakse baashinna
        /// ja kaaluhinna summale.
        /// </summary>
        public static double GetHinnakordaja(this SaadetiseLiik liik) => liik switch
        {
            SaadetiseLiik.Dokument => 0.8,
            SaadetiseLiik.Pakk => 1.0,
            SaadetiseLiik.Suurpakk => 1.5,
            _ => throw new ArgumentOutOfRangeException(nameof(liik))
        };
    }
}

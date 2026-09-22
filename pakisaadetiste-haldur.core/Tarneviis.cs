using System;

namespace pakisaadetiste_haldur.core
{
    /// <summary>
    /// Saadetise tarneviis. Mõjutab hinna arvutamisel lisanduvat tasu.
    /// </summary>
    public enum Tarneviis
    {
        Pakiautomaat,
        Kulleriga,
        Postkontorisse
    }

    public static class TarneviisExtensions
    {
        /// <summary>
        /// Iga tarneviisi lisatasu, mis liidetakse koguhinnale.
        /// </summary>
        public static double GetLisatasu(this Tarneviis tarneviis) => tarneviis switch
        {
            Tarneviis.Pakiautomaat => 0.0,
            Tarneviis.Kulleriga => 3.0,
            Tarneviis.Postkontorisse => 1.0,
            _ => throw new ArgumentOutOfRangeException(nameof(tarneviis))
        };
    }
}

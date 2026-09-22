using System;
using System.Collections.Generic;
using System.Text;

namespace pakisaadetiste_haldur.core
{
    public enum Linn
    {
        Tallinn,
        Tartu,
        Pärnu,
        Narva,
        KohtlaJärve,
        Viljandi,
        Rakvere,
        Maardu,
        Sillamäe,
        Kuressaare
    }

    public static class LinnExtensions
    {
        public static double GetPrice(this Linn linn) => linn switch
        {
            Linn.Tallinn => 5.0,
            Linn.Tartu => 4.0,
            Linn.Pärnu => 3.0,
            Linn.Narva => 4.0,
            Linn.KohtlaJärve => 3.0,
            Linn.Viljandi => 2.0,
            Linn.Rakvere => 3.0,
            Linn.Maardu => 4.0,
            Linn.Sillamäe => 3.0,
            Linn.Kuressaare => 2.0,
            _ => throw new ArgumentOutOfRangeException(nameof(linn))
        };
    }

    public enum SaadetiseStaatus
    {
        Registreeritud,
        Töötlemisel,
        Teel,
        KohaleToimetatud
    }
}

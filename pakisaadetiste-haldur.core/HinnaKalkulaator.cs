using System;

namespace pakisaadetiste_haldur.core
{
    /// <summary>
    /// Vastutab ainult saadetise koguhinna arvutamise eest.
    /// Ei tea midagi kasutajaliidesest ega valideerimisest.
    /// </summary>
    public static class HinnaKalkulaator
    {
        private const double HindKgKohta = 0.5;

        public static double ArvutaHind(Saadetis saadetis)
        {
            if (saadetis == null)
            {
                throw new ArgumentNullException(nameof(saadetis));
            }

            double baasHind = saadetis.Linn.GetPrice();
            double kaaluHind = saadetis.Kaal * HindKgKohta;
            double liigiKordaja = saadetis.Liik.GetHinnakordaja();
            double tarneLisatasu = saadetis.Tarneviis.GetLisatasu();

            double kogusumma = (baasHind + kaaluHind) * liigiKordaja + tarneLisatasu;

            return Math.Round(kogusumma, 2);
        }
    }
}

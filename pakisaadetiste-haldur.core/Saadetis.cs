namespace pakisaadetiste_haldur.core
{
    /// <summary>
    /// Üks pakisaadetise kirje. Sisaldab ainult andmeid -
    /// hinna arvutamise loogika asub eraldi klassis HinnaKalkulaator.
    /// </summary>
    public class Saadetis
    {
        public int Id { get; set; }

        public SaadetiseLiik Liik { get; set; }

        public double Kaal { get; set; }

        public Linn Linn { get; set; }

        public Tarneviis Tarneviis { get; set; }

        public string Aadress { get; set; } = string.Empty;

        public double Hind { get; set; }

        public SaadetiseStaatus Staatus { get; set; }
    }
}

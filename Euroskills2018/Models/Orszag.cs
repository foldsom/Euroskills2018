namespace Euroskills2018.Models
{
    public class Orszag
    {
        public string Id { get; set; }
        public string OrszagNev { get; set; }
        public List<Versenyzo> Versenyzok { get; set; }
    }
}

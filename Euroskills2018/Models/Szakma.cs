namespace Euroskills2018.Models
{
    public class Szakma
    {
        public string Id { get; set; }
        public string SzakmaNev { get; set; }
        public List<Versenyzo> Versenyzok { get; set; }
    }
}

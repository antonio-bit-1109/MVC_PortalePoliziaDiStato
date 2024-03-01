namespace MVC_PortalePoliziaDiStato.Models
{
    public class TotalePuntiDecurtati_PerTrasgressore
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotalePuntiDecurtati { get; set; }

        public TotalePuntiDecurtati_PerTrasgressore()
        {
        }

        public TotalePuntiDecurtati_PerTrasgressore(string Cognome, string Nome, int TotalePuntiDecurtati)
        {
            this.Cognome = Cognome;
            this.Nome = Nome;
            this.TotalePuntiDecurtati = TotalePuntiDecurtati;
        }
    }
}
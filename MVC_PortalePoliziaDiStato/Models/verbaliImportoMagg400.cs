namespace MVC_PortalePoliziaDiStato.Models
{
    public class verbaliImportoMagg400
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public decimal Importo { get; set; }
        public string Descrizione { get; set; }

        public verbaliImportoMagg400()
        {
        }

        public verbaliImportoMagg400(string Nome, string Cognome, decimal Importo, string Descrizione)
        {
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Importo = Importo;
            this.Descrizione = Descrizione;
        }
    }
}
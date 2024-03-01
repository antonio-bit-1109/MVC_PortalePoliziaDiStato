namespace MVC_PortalePoliziaDiStato.Models
{
    public class ListaViolazioni
    {
        public int IDViolazione { get; set; }
        public string descrizione { get; set; }

        public ListaViolazioni()
        {
        }

        public ListaViolazioni(int IDViolazione, string descrizione)
        {
            this.IDViolazione = IDViolazione;
            this.descrizione = descrizione;
        }
    }

}
namespace MVC_PortalePoliziaDiStato.Models
{
    public class ModelloNomeCompleto
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public string NomeCompleto
        {
            get { return Nome + " " + Cognome; }
        }
    }
}
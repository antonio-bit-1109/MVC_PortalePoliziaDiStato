using System;

namespace MVC_PortalePoliziaDiStato.Models
{
    public class ViolazioniMagg10Pt
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public decimal Importo { get; set; }

        public DateTime DataViolazione { get; set; }

        public int DecurtamentoPunti { get; set; }


        public ViolazioniMagg10Pt(string nome, string cognome, decimal importo, DateTime dataViolazione, int decurtamentoPunti)
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Importo = importo;
            this.DataViolazione = dataViolazione;
            this.DecurtamentoPunti = decurtamentoPunti;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_PortalePoliziaDiStato.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [Required(ErrorMessage = "Inserire Data Della Violazione")]
        public DateTime DataViolazione { get; set; }

        [StringLength(200)]
        public string IndirizzoViolazione { get; set; }

        [Required(ErrorMessage = "Inserire Nominativo Agente")]
        [StringLength(100)]
        public string NominativoAgente { get; set; }

        [Required(ErrorMessage = "Inserisci data di Trascrizione del Verbale")]
        public DateTime DataTrascrizione_verbale { get; set; }

        [Required(ErrorMessage = "Inserire Importo")]
        public decimal Importo { get; set; }

        [Required(ErrorMessage = "Inserire Decurtamento Punti")]
        public int DecurtamentoPunti { get; set; }

        [Required(ErrorMessage = "Inserire ID Violazione")]
        public int IDViolazione { get; set; }

        [Required(ErrorMessage = "Inserire ID Anagrafica")]
        public int IDAnagrafica { get; set; }
    }
}
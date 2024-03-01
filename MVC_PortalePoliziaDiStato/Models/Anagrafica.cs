using System.ComponentModel.DataAnnotations;

namespace MVC_PortalePoliziaDiStato.Models
{
    public class Anagrafica
    {

        public int IdAnagrafica { get; set; }

        [Required(ErrorMessage = "Inserire il cognome")]
        // deve essere lungo almeno 3 caratteri
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Inserire il nome")]
        [StringLength(50)]
        public string nome { get; set; }

        [Required(ErrorMessage = "Inserire Indirizzo")]
        [StringLength(200)]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Inserire Città")]
        [StringLength(50)]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Inserire CAP")]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required(ErrorMessage = "Inserire Provincia")]
        [StringLength(16)]
        public string CF { get; set; }
    }
}
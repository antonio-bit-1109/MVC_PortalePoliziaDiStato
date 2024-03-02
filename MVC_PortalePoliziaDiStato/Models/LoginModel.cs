using System.ComponentModel.DataAnnotations;

namespace MVC_PortalePoliziaDiStato.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Inserisci il nomeAgente ")]
        public string NomeAgente { get; set; }

        [Required(ErrorMessage = "Inserisci la password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
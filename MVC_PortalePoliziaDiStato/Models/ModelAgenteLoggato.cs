namespace MVC_PortalePoliziaDiStato.Models
{
    public class ModelAgenteLoggato
    {

        public int IdAgente { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }

        public ModelAgenteLoggato(int idAgente, string nome, string password)
        {
            this.IdAgente = idAgente;
            this.Nome = nome;
            this.Password = password;
        }
    }
}
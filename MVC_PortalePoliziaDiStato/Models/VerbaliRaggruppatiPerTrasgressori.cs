namespace MVC_PortalePoliziaDiStato.Models
{
    public class VerbaliRaggruppatiPerTrasgressori
    {
        public string CognomeTrasgressore { get; set; }
        public string NomeTrasgressore { get; set; }
        public int NumeroVerbali { get; set; }


        public VerbaliRaggruppatiPerTrasgressori()
        {

        }

        public VerbaliRaggruppatiPerTrasgressori(string cognomeTrasgressore, string nomeTrasgressore, int numeroVerbali)
        {
            CognomeTrasgressore = cognomeTrasgressore;
            NomeTrasgressore = nomeTrasgressore;
            NumeroVerbali = numeroVerbali;

        }
    }
}
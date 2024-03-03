using MVC_PortalePoliziaDiStato.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_PortalePoliziaDiStato.Controllers
{
    public class HomeController : Controller
    {

        // restituisce la view dell'index come pagina principale del sito
        public ActionResult Index()
        {
            return View();
        }

        // restituisce la view del login come pagina parziale
        // la login partial è inserita all'interno di home/index 
        public ActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        // questo metodo viene richiamato quando si clicca sul bottone login nella pagina parziale ed effettua un controllo su nome e password inseriti dall'utente, 
        // se corrispondenti ai valori salvati nella tabella AgentiPolizia del database allora l'utente viene reindirizzato a verbali/index e ceata una sessione con i dati dell'agente loggato
        // sfruttando il ModelAgenteLoggato.
        [HttpPost]
        public ActionResult CheckLogin(LoginModel modello)
        {
            if (ModelState.IsValid)
            {


                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    string query = "SELECT * FROM AgentiPolizia WHERE Nome = @NomeAgente AND Password = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NomeAgente", modello.NomeAgente);
                    cmd.Parameters.AddWithValue("@password", modello.Password);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int idAgente = Convert.ToInt32(reader["IdAgente"]);
                        string nome = reader["Nome"].ToString();
                        string password = reader["Password"].ToString();

                        if (nome == modello.NomeAgente && password == modello.Password)
                        {
                            ModelAgenteLoggato agenteLoggato = new ModelAgenteLoggato(idAgente, nome, password);

                            Session["DatiAgenteLoggato"] = agenteLoggato;
                            Session["benvenuto"] = $" Benvenuto Agente {nome} ";
                            return RedirectToAction("Index", "Verbali");
                        }
                        else
                        {
                            TempData["Errore"] = "NomeAgente o Password non validi";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {

                        TempData["Errore"] = "NomeAgente o Password non trovati";
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);

                }
                finally
                {
                    conn.Close();
                }
            }
            TempData["Errore"] = "NomeAgente o Password non validi";
            return RedirectToAction("Index", "Home");
        }

        // questa action viene richiamata quando si clicca sul bottone logout nella pagina parziale
        // essa è responsabile della cancellazione dei dati dalla session e del reindirizzamento ad Home/index
        public ActionResult LogoutAction()
        {
            Session["DatiAgenteLoggato"] = null;
            Session["benvenuto"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
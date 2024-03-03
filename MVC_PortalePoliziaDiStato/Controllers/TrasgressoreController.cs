using MVC_PortalePoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_PortalePoliziaDiStato.Controllers
{
    public class TrasgressoreController : Controller
    {
        // GET: Trasgressore
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Trasgressore/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Trasgressore/Create

        // questo metodo , se esiste una sessione dell'agente loggat, mostra la view per inserire un nuovo trasgressore.
        public ActionResult Create()
        {
            if (Session["DatiAgenteLoggato"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // quando nella view viene cliccato il bottone per inviare il form , se il modello inserito nel form risulta coerente con il modello anagrafica, quest' ultimo viene inserito nel databaase
        [HttpPost]
        public ActionResult Create(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                string query;

                try
                {
                    conn.Open();
                    query = "Insert Into Anagrafica (Cognome , nome , Indirizzo , Città , CAP , CF)" +
                        " VALUES (@cognome , @nome , @indirizzo , @citta , @cap , @cf) ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cognome", anagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@nome", anagrafica.nome);
                    cmd.Parameters.AddWithValue("@indirizzo", anagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@citta", anagrafica.Citta);
                    cmd.Parameters.AddWithValue("@cap", anagrafica.CAP);
                    cmd.Parameters.AddWithValue("@cf", anagrafica.CF);

                    cmd.ExecuteNonQuery();

                    TempData["Messaggio"] = "Trasgressore inserito correttamente";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    TempData["Errore"] = ex.Message;
                    return RedirectToAction("Create");
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                TempData["Errore"] = "Errore durante la compilazione. Riprova";
                return RedirectToAction("Create");
            }


        }

        // get che ricava l'elenco dei trasgressori dal DB , li inserisce in una lista e li mostra nella view.
        public ActionResult ElencoTrasgressioni()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);


            List<ListaViolazioni> ElencoViolazioni = new List<ListaViolazioni>();


            string query;
            try
            {

                conn.Open();
                query = "SELECT * FROM Violazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idViolazione = Convert.ToInt32(reader["IDViolazione"]);
                    string descrizione = reader["descrizione"].ToString();
                    //decimal TotaleImporti = Convert.ToDecimal(reader["TotaleImporti"]);
                    //int TotalePunti = Convert.ToInt32(reader["TotalePunti"]);

                    ElencoViolazioni.Add(new ListaViolazioni(idViolazione, descrizione));


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                //TempData["Errore"] = ex.Message;
                //return RedirectToAction("Index", "Verbali");
            }
            finally
            {
                conn.Close();
            }

            return View(ElencoViolazioni);
        }

    }
}

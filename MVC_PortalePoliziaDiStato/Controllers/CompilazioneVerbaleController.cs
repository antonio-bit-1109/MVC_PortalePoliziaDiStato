using MVC_PortalePoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_PortalePoliziaDiStato.Controllers
{
    public class CompilazioneVerbaleController : Controller
    {
        // GET: CompilazioneVerbale
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: CompilazioneVerbale/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: CompilazioneVerbale/Create
        public ActionResult Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            //List<int> ListaElencoViolazioni = new List<int>();
            Dictionary<int, string> DizionarioElencoViolazioni = new Dictionary<int, string>();
            Dictionary<int, ModelloNomeCompleto> DizionarioElencoAnagrafiche = new Dictionary<int, ModelloNomeCompleto>();
            //List<int> ListaIdAnagrafica = new List<int>();
            string query;

            try
            {
                conn.Open();
                //query = "SELECT IDViolazione FROM Violazione";
                query = "SELECT * FROM Violazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idViolazione = Convert.ToInt32(reader["IDViolazione"]);
                    string descrizione = reader["descrizione"].ToString();
                    //ListaElencoViolazioni.Add(idViolazione);
                    //Session["ListaElencoViolazioni"] = ListaElencoViolazioni;
                    DizionarioElencoViolazioni.Add(idViolazione, descrizione);
                }

                reader.Close();

                query = "SELECT idanagrafica , nome , cognome FROM anagrafica order by idanagrafica ASC ";
                cmd = new SqlCommand(query, conn); // Crea un nuovo comando SQL
                reader = cmd.ExecuteReader(); // Esegui il nuovo comando SQL


                while (reader.Read())
                {
                    int idanagrafica = Convert.ToInt32(reader["IDAnagrafica"]);
                    string nome = reader["Nome"].ToString();
                    string cognome = reader["Cognome"].ToString();
                    //ListaIdAnagrafica.Add(idanagrafica);
                    //Session["ListaIdAnagrafica"] = ListaIdAnagrafica;
                    ModelloNomeCompleto Nome_CognomeCompleto = new ModelloNomeCompleto { Nome = nome, Cognome = cognome };
                    DizionarioElencoAnagrafiche.Add(idanagrafica, Nome_CognomeCompleto);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                //TempData["Errore"] = ex.Message;
                //return RedirectToAction("Index");
                return new EmptyResult();
            }
            finally
            {
                conn.Close();
            }

            ViewBag.dizionarioViolazioni = DizionarioElencoViolazioni;
            ViewBag.dizionarioAnagrafiche = DizionarioElencoAnagrafiche;
            return View();

        }


        // POST: CompilazioneVerbale/Create
        [HttpPost]
        public ActionResult Create(Verbale verbale)
        {

            if (ModelState.IsValid)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                string query;

                try
                {
                    conn.Open();
                    query = "INSERT INTO Verbale (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizione_verbale, Importo, DecurtamentoPunti, IDViolazione, IDAnagrafica) " +
                        "VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizione_verbale, @Importo, @DecurtamentoPunti, @IDViolazione, @IDAnagrafica)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DataViolazione", Convert.ToDateTime(verbale.DataViolazione));
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    cmd.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                    cmd.Parameters.AddWithValue("@DataTrascrizione_verbale", Convert.ToDateTime(verbale.DataTrascrizione_verbale));
                    cmd.Parameters.AddWithValue("@Importo", Convert.ToDecimal(verbale.Importo));
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", Convert.ToInt32(verbale.DecurtamentoPunti));
                    cmd.Parameters.AddWithValue("@IDViolazione", Convert.ToInt32(verbale.IDViolazione));
                    cmd.Parameters.AddWithValue("@IDAnagrafica", Convert.ToInt32(verbale.IDAnagrafica));

                    cmd.ExecuteNonQuery();

                    TempData["Messaggio"] = "Verbale Inserito Correttamente";
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
                TempData["Errore"] = "Errore di validazione. Ricontrolla i Campi.";
                return View("Create");
            }

        }




        // GET: CompilazioneVerbale/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CompilazioneVerbale/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CompilazioneVerbale/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CompilazioneVerbale/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

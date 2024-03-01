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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trasgressore/Create
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

        // get per avere elenco trasgressori
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


                //cmd.ExecuteNonQuery();

                //TempData["Messaggio"] = "Trasgressore inserito correttamente";
                //return RedirectToAction("Index", "Verbali");
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
        // GET: Trasgressore/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Trasgressore/Edit/5
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

        //// GET: Trasgressore/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Trasgressore/Delete/5
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

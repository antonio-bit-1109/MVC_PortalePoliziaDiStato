using System;
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
            return View();
        }


        // POST: CompilazioneVerbale/Create
        [HttpPost]
        public ActionResult Create(FormCollection DatiForm)
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
                    cmd.Parameters.AddWithValue("@DataViolazione", Convert.ToDateTime(DatiForm["DataViolazione"]));
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", DatiForm["IndirizzoViolazione"]);
                    cmd.Parameters.AddWithValue("@NominativoAgente", DatiForm["NominativoAgente"]);
                    cmd.Parameters.AddWithValue("@DataTrascrizione_verbale", Convert.ToDateTime(DatiForm["DataTrascrizione_verbale"]));
                    cmd.Parameters.AddWithValue("@Importo", Convert.ToDecimal(DatiForm["Importo"]));
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", Convert.ToInt32(DatiForm["DecurtamentoPunti"]));
                    cmd.Parameters.AddWithValue("@IDViolazione", Convert.ToInt32(DatiForm["IDViolazione"]));
                    cmd.Parameters.AddWithValue("@IDAnagrafica", Convert.ToInt32(DatiForm["IDAnagrafica"]));

                    cmd.ExecuteNonQuery();

                    TempData["Messaggio"] = "Verbale Inserito Correttamente";
                    return RedirectToAction("Index", "Verbali");
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

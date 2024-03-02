using MVC_PortalePoliziaDiStato.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_PortalePoliziaDiStato.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

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

        public ActionResult LogoutAction()
        {
            Session["DatiAgenteLoggato"] = null;
            Session["benvenuto"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
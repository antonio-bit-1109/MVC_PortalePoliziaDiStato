using MVC_PortalePoliziaDiStato.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult VerbaliRaggruppatiTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string query;

            List<VerbaliRaggruppatiPerTrasgressori> LIstaDeiVerbaliRaggrup_Trasgressori = new List<VerbaliRaggruppatiPerTrasgressori>();


            try
            {

                conn.Open();
                query = "SELECT Cognome, Nome, COUNT(*) AS NumeroVerbali, SUM(Importo) AS TotaleImporti, SUM(DecurtamentoPunti) AS TotalePunti FROM Anagrafica INNER JOIN Verbale ON Anagrafica.IdAnagrafica = Verbale.IDAnagrafica GROUP BY Cognome, Nome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string CognomeTrasgressore = reader["Cognome"].ToString();
                    string NomeTrasgressore = reader["Nome"].ToString();
                    int NumeroVerbali = Convert.ToInt32(reader["NumeroVerbali"]);
                    //decimal TotaleImporti = Convert.ToDecimal(reader["TotaleImporti"]);
                    //int TotalePunti = Convert.ToInt32(reader["TotalePunti"]);

                    LIstaDeiVerbaliRaggrup_Trasgressori.Add(new VerbaliRaggruppatiPerTrasgressori(CognomeTrasgressore, NomeTrasgressore, NumeroVerbali));


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                TempData["Errore"] = ex.Message;
                return RedirectToAction("Index");
            }
            finally
            {
                conn.Close();
            }

            return View(LIstaDeiVerbaliRaggrup_Trasgressori);
        }

        public ActionResult TotalePuntiDecurtati_PerTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string query;

            List<TotalePuntiDecurtati_PerTrasgressore> ListaTotalePuntiDecurtati_Trasgressore = new List<TotalePuntiDecurtati_PerTrasgressore>();


            try
            {

                conn.Open();
                query = "SELECT Cognome, Nome, SUM(DecurtamentoPunti) AS TotalePunti FROM Anagrafica INNER JOIN Verbale ON Anagrafica.IdAnagrafica = Verbale.IDAnagrafica GROUP BY Cognome, Nome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string CognomeTrasgressore = reader["Cognome"].ToString();
                    string NomeTrasgressore = reader["Nome"].ToString();
                    int SommaPuntiDecurtati = Convert.ToInt32(reader["TotalePunti"]);


                    ListaTotalePuntiDecurtati_Trasgressore.Add(new TotalePuntiDecurtati_PerTrasgressore(CognomeTrasgressore, NomeTrasgressore, SommaPuntiDecurtati));


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                TempData["Errore"] = ex.Message;
                return RedirectToAction("Index");
            }
            finally
            {
                conn.Close();
            }

            return View(ListaTotalePuntiDecurtati_Trasgressore);
        }

        public ActionResult VerbaliImportoMaggiore400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string query;

            List<verbaliImportoMagg400> ListaVerbaliImportoMagg400 = new List<verbaliImportoMagg400>();

            try
            {

                conn.Open();
                query = "SELECT an.NOME , an.cognome , ve.importo , vio.descrizione FROM anagrafica as an inner join verbale as ve " +
                    "on ve.idanagrafica = an.IdAnagrafica inner join violazione as vio on ve.IDViolazione = vio.IDViolazione where ve.importo > 400";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader["Nome"].ToString();
                    string cognome = reader["Cognome"].ToString();
                    decimal importo = Convert.ToDecimal(reader["Importo"]);
                    string descrizione = reader["Descrizione"].ToString();

                    ListaVerbaliImportoMagg400.Add(new verbaliImportoMagg400(nome, cognome, importo, descrizione));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                TempData["Errore"] = ex.Message;
                return RedirectToAction("Index");
            }
            finally
            {
                conn.Close();
            }


            return View(ListaVerbaliImportoMagg400);
        }
    }
}
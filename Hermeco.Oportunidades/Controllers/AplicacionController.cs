using Hermeco.Oportunidades.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Hermeco.Oportunidades.Controllers
{
    public class AplicacionController : ApiController
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: api/Aplicacion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Aplicacion/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Aplicacion/Enviar")]
        [HttpPost]
        public bool Enviar(Application aplicacion)
        {
            return true;
        }

        // POST: api/Aplicacion        
        public HttpResponseMessage Post(Application aplicacion)
        {
            
            logger.Info("hola");
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            string from = ConfigurationManager.AppSettings["FromEmail"].ToString();
            string ruta = ConfigurationManager.AppSettings["RutaEmail"].ToString();
            string CCEmail = ConfigurationManager.AppSettings["CCEmail"].ToString();
            string email = ConfigurationManager.AppSettings["Email"].ToString();
            string errorEmail = ConfigurationManager.AppSettings["ErrorEmail"].ToString();
            string subject = string.Format("Aplicación a Oferta de Empleo {0}", aplicacion.Cargo);
            logger.Info("hola");
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    logger.Info("hola");
                    //var docfiles = new List<string>();
                    var filePath = "";
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                        string fileExtension = System.IO.Path.GetExtension(postedFile.FileName);
                        filePath = HttpContext.Current.Server.MapPath("~/Files/HVAplicaciones/" + fileName + DateTime.Now.ToString("ddMMyyyyhhmm") + fileExtension);
                        postedFile.SaveAs(filePath);
                        //docfiles.Add(filePath);
                    }

                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("{NombreCargo}", httpRequest.Form["Cargo"].ToString());
                    parameters.Add("{Nombre}", aplicacion.Nombre);
                    parameters.Add("{Email}", aplicacion.Email);
                    parameters.Add("{Tele}", aplicacion.Telefono);

                    String uri = HttpContext.Current.Request.Url.Scheme + "://" +
                                 HttpContext.Current.Request.Url.Host + ":" +
                                 HttpContext.Current.Request.Url.Port;
                    parameters.Add("{url}", uri + "/Files/HVAplicaciones/" + System.IO.Path.GetFileName(filePath));

                    Utilities.Utilities.SendEmail(subject, ruta, "", from, email, null, true, CCEmail, parameters);
                    logger.Info("hola");
                    result = Request.CreateResponse(HttpStatusCode.OK, "todo ok");
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                Utilities.Utilities.SendEmail("Error al enviar aplicación", "", ex.Message, "info@offcorss.com", errorEmail, null, false, "");
            }
            return result;
        }

        // PUT: api/Aplicacion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Aplicacion/5
        public void Delete(int id)
        {
        }
    }
}

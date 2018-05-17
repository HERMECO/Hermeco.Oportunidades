using Hermeco.Oportunidades.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hermeco.Oportunidades.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        
        // POST: api/Aplicacion        
        public HttpResponseMessage Post(Application app)
        {
            HttpResponseMessage result = null;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                Application aplicacion = JsonConvert.DeserializeObject<Application>(httpRequest.Form["Aplicacion"].ToString());
                //Application aplicacion = new Application();
                //aplicacion.Cargo = httpRequest.Form["Cargo"].ToString();
                //aplicacion.Email = httpRequest.Form["Email"].ToString();
                //aplicacion.IdRequisicion = int.Parse(httpRequest.Form["Idrequisicion"].ToString());
                //aplicacion.Nombre = httpRequest.Form["Nombre"].ToString();
                //aplicacion.Telefono = httpRequest.Form["Telefono"].ToString();

                
                string from = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string ruta = HttpRuntime.AppDomainAppPath + "Email\\Aplicacion.html";//ConfigurationManager.AppSettings["RutaEmail"].ToString();
                string CCEmail = ConfigurationManager.AppSettings["CCEmail"].ToString();
                string email = ConfigurationManager.AppSettings["Email"].ToString();
                string errorEmail = ConfigurationManager.AppSettings["ErrorEmail"].ToString();
                string subject = string.Format("Aplicación a Oferta de Empleo {0}", aplicacion.Cargo);
                
                try
                {
                    if (httpRequest.Files.Count > 0)
                    {                        
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
                        parameters.Add("{NombreCargo}", aplicacion.Cargo);
                        parameters.Add("{Nombre}", aplicacion.Nombre);
                        parameters.Add("{Email}", aplicacion.Email);
                        parameters.Add("{Tele}", aplicacion.Telefono);

                        String uri = HttpContext.Current.Request.Url.Scheme + "://" +
                                     HttpContext.Current.Request.Url.Host + ":" +
                                     HttpContext.Current.Request.Url.Port;
                        parameters.Add("{url}", uri + "/Files/HVAplicaciones/" + System.IO.Path.GetFileName(filePath));
                        
                        Utilities.Utilities.SendEmail(subject, ruta, "", from, email, null, true, CCEmail, parameters);                        
                        result = Request.CreateResponse(HttpStatusCode.OK, "todo ok");
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.BadRequest);
                        logger.Error("Error al adjuntar archivo");
                        Utilities.Utilities.SendEmail("Error al enviar aplicación", "", "Sin archivo adjunto", "info@offcorss.com", errorEmail, null, false, "");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    Utilities.Utilities.SendEmail("Error al enviar aplicación", "", ex.Message, "info@offcorss.com", errorEmail, null, false, "");
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
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

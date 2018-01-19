using System.IO;
using System.Web;

namespace Hermeco.Oportunidades.Models
{
    public class Application
    {
        public Application() { }
        public int IdRequisicion { get; set; }
        public string Cargo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string CV { get; set; }
        public HttpPostedFileBase Doc { get; set; }
    }
}
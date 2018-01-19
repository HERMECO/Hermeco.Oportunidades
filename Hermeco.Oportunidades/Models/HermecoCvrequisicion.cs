using System;


namespace Hermeco.Oportunidades.Models
{

    public class HermecoCvrequisicion {
        public HermecoCvrequisicion() { }
        public virtual int Idrequisicion { get; set; }
        public virtual string Consecutivo { get; set; }
        public virtual string Cargo { get; set; }
        public virtual string Arearequisicion { get; set; }
        public virtual string Experiencia { get; set; }
        public virtual string Profesion { get; set; }
        public virtual string Ciudadsolicitud { get; set; }
        public virtual DateTime? Fechaingreso { get; set; }
        public virtual string Actividades { get; set; }
        public virtual DateTime Fechainicio { get; set; }
        public virtual DateTime Fechafin { get; set; }
        public virtual bool Publicarinternet { get; set; }
        public virtual bool Publicarintranet { get; set; }
        public virtual string Propietario { get; set; }
        public virtual DateTime Fechacreacion { get; set; }
        public virtual string Usuarioactulizador { get; set; }
        public virtual DateTime? Fechaactulizacion { get; set; }
        public virtual string Funciones { get; set; }
        public virtual string Formacion { get; set; }
        public virtual string Conocimientos { get; set; }
        public virtual string Otros { get; set; }
    }
}

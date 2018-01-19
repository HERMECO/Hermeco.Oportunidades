using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace Hermeco.Oportunidades.Models
{
    public class HermecoCvrequisicionMap : ClassMapping<HermecoCvrequisicion> {
        
        public HermecoCvrequisicionMap() {
			Table("Hermeco_CVRequisicion");
			Schema("dbo");
			Lazy(true);
			Id(x => x.Idrequisicion, map => map.Generator(Generators.Identity));
			Property(x => x.Consecutivo, map => map.NotNullable(true));
			Property(x => x.Cargo, map => map.NotNullable(true));
			Property(x => x.Arearequisicion, map => map.NotNullable(true));
			Property(x => x.Experiencia, map => map.NotNullable(true));
			Property(x => x.Profesion, map => map.NotNullable(true));
			Property(x => x.Ciudadsolicitud, map => map.NotNullable(true));
			Property(x => x.Fechaingreso);
			Property(x => x.Actividades);
			Property(x => x.Fechainicio, map => map.NotNullable(true));
			Property(x => x.Fechafin, map => map.NotNullable(true));
			Property(x => x.Publicarinternet, map => map.NotNullable(true));
			Property(x => x.Publicarintranet, map => map.NotNullable(true));
			Property(x => x.Propietario, map => map.NotNullable(true));
			Property(x => x.Fechacreacion, map => map.NotNullable(true));
			Property(x => x.Usuarioactulizador);
			Property(x => x.Fechaactulizacion);
			Property(x => x.Funciones);
			Property(x => x.Formacion);
			Property(x => x.Conocimientos);
			Property(x => x.Otros);
			//Bag(x => x.HermecoCvrequisicionhojavida, colmap =>  { colmap.Key(x => x.Column("IdRequisicion")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}

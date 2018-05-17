using Hermeco.Oportunidades.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hermeco.Oportunidades.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OportunidadController : ApiController
    {   
        // GET: api/Oportunidad
        public List<HermecoCvrequisicion> Get()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            List<HermecoCvrequisicion> oportunidadesVigentes = new List<HermecoCvrequisicion>();

            try
            {                
                using (ITransaction tx = session.BeginTransaction())
                {
                    oportunidadesVigentes = session
                    .Query<HermecoCvrequisicion>()
                    .Where(c => c.Fechafin.Date >= DateTime.Now.Date)
                    .ToList();
                }
            }
            //catch(Exception ex)
            //{
                
            //}
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return oportunidadesVigentes;
        }

        // GET: api/Oportunidad/5
        public HermecoCvrequisicion Get(int id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            HermecoCvrequisicion oportunidadesVigentes = new HermecoCvrequisicion();

            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    oportunidadesVigentes = session
                    .Query<HermecoCvrequisicion>()
                    .Where(c => c.Idrequisicion == id).First();                    
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return oportunidadesVigentes;
        }

        // POST: api/Oportunidad
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Oportunidad/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Oportunidad/5
        public void Delete(int id)
        {
        }
    }
}

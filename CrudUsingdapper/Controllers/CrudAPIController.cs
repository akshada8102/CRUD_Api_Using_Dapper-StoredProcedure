using CrudUsingdapper.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsingdapper.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CrudAPIController : ControllerBase
    {
        String con;
        DB.DBCrud db = new DB.DBCrud();

        public CrudAPIController(IConfiguration config)
        {
              con = config["ConnectionStrings:connection"] ?? "";
              db.connectionString(con);
        }


        [HttpPost]
        [Route("api/Register/Post")]
        public ModelCrud Post(ModelCrud crud)
        {
            ModelCrud res = db.Post(crud, con);
            return res;
        }

        [HttpGet]
        [Route("api/Register/Get")]
        public IEnumerable<ModelCrud> Gets()
        {
            return db.Gets();
        }

        [HttpGet]
        [Route("api/Register/GetId")]
        public ModelCrud Get(int Idd)
        {
            return db.Get(Idd);
        }

        [HttpPut]
        [Route("api/Register/Delete")]
        public String Delete(int Idd)
        {
            return db.Delete(Idd);
        }

        [HttpPut]
        [Route("api/Register/Update")]
        public ModelCrud Update(ModelCrud crud)
        {
            return db.Update(crud);
        }
    }
}

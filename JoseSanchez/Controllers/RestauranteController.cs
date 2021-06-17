using JoseSanchez.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly AppDbContext context;

        public RestauranteController(AppDbContext context)
        {
            this.context = context;
        }
        

        /*
         * Metodo de retorno de todos loa campos
         */
        // GET : api/Restaurante
        [HttpGet]
        public async Task<List<Restaurante>> GetAllRestaurante()
        {
            //var list = context
            //           .Restaurantes
            //           .FromSqlRaw("call Sp_Restauranes('I',0,'0',0,0,'0',@d,@e)")
            //           .ToList();
            //return list;

            List<Restaurante> members = new List<Restaurante>();
            var result = await context.Restaurantes.FromSqlRaw(@"call Sp_Restauranes('I',0,'0',0,0,'0',@d,@e)").ToListAsync();

            foreach (var row in result)
            {
                members.Add(new Restaurante
                {
                    IDRestaurante = row.IDRestaurante,
                    NombreRestaurante = row.NombreRestaurante,
                    IDCiudad = row.IDCiudad,
                    NumeroAforo = row.NumeroAforo,
                    Telefono = row.Telefono,
                    FechaCreacion = row.FechaCreacion,
                    NombreCiudad = row.NombreCiudad

                });
            }
            return members;
        }


        /*
         * Metodo de retorno solo por el parametro @NombreRestaurante[Nombre del restaurante a buscar]
         */
        // GET api/Restaurante/5
        [HttpGet("{NombreRestaurante}")]
        public async Task<List<Restaurante>> GetNombreRestaurante(string NombreRestaurante)
        {

            //SqlParameter param2 = new SqlParameter("@p1", "value");
            List<Restaurante> members = new List<Restaurante>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "II"));
            parameter.Add(new MySqlParameter("@IDRestaurante", 0));
            parameter.Add(new MySqlParameter("@NombreRestaurante", NombreRestaurante));
            parameter.Add(new MySqlParameter("@IDCiudad", 0));
            parameter.Add(new MySqlParameter("@NumeroAforo",0));
            parameter.Add(new MySqlParameter("@Telefono", ""));
          

            var list = await context
                  .Restaurantes
                  .FromSqlRaw("call Sp_Restauranes(@opcion , @IDRestaurante ,@NombreRestaurante, @IDCiudad, @NumeroAforo, @Telefono, @d ,@e )",parameter.ToArray())
                  .ToListAsync();

            foreach (var row in list)
            {
                members.Add(new Restaurante
                {
                    IDRestaurante = row.IDRestaurante,
                    NombreRestaurante = row.NombreRestaurante,
                    IDCiudad = row.IDCiudad,
                    NumeroAforo = row.NumeroAforo,
                    Telefono = row.Telefono,
                    FechaCreacion = row.FechaCreacion,
                    NombreCiudad = row.NombreCiudad

                });
            }

            return members;
      


        }



        /*
        * Metodo de Insert 
        * parametro @Object  RestauranteTrx
        * return json 
        */
        // POST: api/RestaurantePost
        [HttpPost]
        public async Task<ActionResult> PostRestaurante([FromBody] RestauranteTrx obj)
        {
            List<RestauranteTrx> objRest = new List<RestauranteTrx>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "III"));
            //parameter.Add(new MySqlParameter("@IDRestaurante", 0));
            parameter.Add(new MySqlParameter("@NombreRestaurante", obj.NombreRestaurante));
            parameter.Add(new MySqlParameter("@IDCiudad", obj.IDCiudad));
            parameter.Add(new MySqlParameter("@NumeroAforo", obj.NumeroAforo));
            parameter.Add(new MySqlParameter("@Telefono", obj.Telefono));
            //parameter.Add(new MySqlParameter("@out_cod", 0));
            //parameter.Add(new MySqlParameter("@out_msj", ""));

            var list6 = await context
                 .RestaurantesTrx
                 .FromSqlRaw("call Sp_Restauranes(@opcion , 0 ,@NombreRestaurante, @IDCiudad, @NumeroAforo, @Telefono, @d ,@e )", parameter.ToArray())
                 .ToListAsync();

            var json = "";

            foreach (var row in list6)
            {
                //JsonConvert.SerializeObject(new { out_cod = row.out_cod, out_msj  = row.out_msj }, Formatting.None);
                objRest.Add(new RestauranteTrx
                {
                    out_cod = row.out_cod,
                    out_msj = row.out_msj
                });
            }

             json = JsonConvert.SerializeObject(objRest);

            return Content(json,  "application/json");
        }



        /*
        * Metodo de Update 
        * parametro @Object  Restaurante
        * @IDRestaurante usado en el where PK de la tabla
        * return json 
        */
        // PUT: api/RestaurantePost
        [HttpPut]
        public async Task<ActionResult> PutRestaurante([FromBody] RestauranteTrx obj)
        {
            List<RestauranteTrx> objRest = new List<RestauranteTrx>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "IV"));
            parameter.Add(new MySqlParameter("@IDRestaurante", obj.IDRestaurante));
            parameter.Add(new MySqlParameter("@NombreRestaurante", obj.NombreRestaurante));
            parameter.Add(new MySqlParameter("@IDCiudad", obj.IDCiudad));
            parameter.Add(new MySqlParameter("@NumeroAforo", obj.NumeroAforo));
            parameter.Add(new MySqlParameter("@Telefono", obj.Telefono));
           

            var list6 = await context
                 .RestaurantesTrx
                 .FromSqlRaw("call Sp_Restauranes(@opcion , @IDRestaurante ,@NombreRestaurante, @IDCiudad, @NumeroAforo, @Telefono, @d ,@e )", parameter.ToArray())
                 .ToListAsync();

            var json = "";

            foreach (var row in list6)
            {
                //JsonConvert.SerializeObject(new { out_cod = row.out_cod, out_msj  = row.out_msj }, Formatting.None);
                objRest.Add(new RestauranteTrx
                {
                    out_cod = row.out_cod,
                    out_msj = row.out_msj
                });
            }

            json = JsonConvert.SerializeObject(objRest);

            return Content(json, "application/json");
        }



        /*
        * Metodo de Delete 
        * parametro @Object  RestauranteTrx
        * return json 
        */
        // PUT:  api/RestauranteDelete/5
        [HttpDelete] // 
        public async Task<ActionResult> DeleteRestaurante(int IDRestaurante)
        {
            List<RestauranteTrx> objRest = new List<RestauranteTrx>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "V"));
            parameter.Add(new MySqlParameter("@IDRestaurante", IDRestaurante));
            parameter.Add(new MySqlParameter("@NombreRestaurante",""));
            parameter.Add(new MySqlParameter("@IDCiudad", 0));
            parameter.Add(new MySqlParameter("@NumeroAforo", 0));
            parameter.Add(new MySqlParameter("@Telefono",""));


            var list6 = await context
                 .RestaurantesTrx
                 .FromSqlRaw("call Sp_Restauranes(@opcion , @IDRestaurante ,@NombreRestaurante, @IDCiudad, @NumeroAforo, @Telefono, @d ,@e )", parameter.ToArray())
                 .ToListAsync();

            var json = "";

            foreach (var row in list6)
            {
                //JsonConvert.SerializeObject(new { out_cod = row.out_cod, out_msj  = row.out_msj }, Formatting.None);
                objRest.Add(new RestauranteTrx
                {
                    out_cod = row.out_cod,
                    out_msj = row.out_msj
                });
            }

            json = JsonConvert.SerializeObject(objRest);

            return Content(json, "application/json");
        }

    }
}

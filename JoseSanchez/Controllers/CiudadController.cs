using JoseSanchez.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {

        private readonly AppDbContext context;

        public CiudadController(AppDbContext context)
        {
            this.context = context;
        }


        /*
        * Metodo de retorno de todos loa campos
        */
        // GET : api/Ciudad
        [HttpGet]
        public async Task<List<Ciudad>> GetAllCiudades()
        {
            
            List<Ciudad> members = new List<Ciudad>();
            var result = await context.Ciudades.FromSqlRaw(@"call Sp_Ciudades('I',0,'0',@d,@e)").ToListAsync();

            foreach (var row in result)
            {
                members.Add(new Ciudad
                {
                    IDCiudad = row.IDCiudad,
                    NombreCiudad = row.NombreCiudad,
                    FechaCreacion = row.FechaCreacion


                });
            }
            return members;
        }





        /*
         * Metodo de retorno solo por el parametro @NombreRestaurante[Nombre del restaurante a buscar]
         */
        // GET api/Ciudad/Guaya
        [HttpGet("{NombreCiudad}")]
        public async Task<List<Ciudad>> GetNombreCiudad(string NombreCiudad)
        {

            List<Ciudad> members = new List<Ciudad>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "II"));
            parameter.Add(new MySqlParameter("@IDCiudad", 0));
            parameter.Add(new MySqlParameter("@NombreCiudad", NombreCiudad));

            var result = await context.Ciudades.FromSqlRaw(@"call Sp_Ciudades(@opcion , @IDCiudad,@NombreCiudad, @d ,@e )").ToListAsync();

           

            //foreach (var row in result)
            //{
            //    members.Add(new Ciudad
            //    {
            //        IDCiudad = row.IDCiudad,
            //        NombreCiudad = row.NombreCiudad,
            //        FechaCreacion = row.FechaCreacion


            //    });
            //}
            return result;

        }




        /*
        * Metodo de Insert 
        * parametro @Object  CiudadTrx
        * return json 
        */
        // POST: api/CiudadPost
        [HttpPost]
        public async Task<ActionResult> PostCiudad([FromBody] CiudadTrx obj)
        {
            List<CiudadTrx> objRest = new List<CiudadTrx>();



            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "III"));
            parameter.Add(new MySqlParameter("@IDCiudad", obj.IDCiudad));
            parameter.Add(new MySqlParameter("@NombreCiudad", obj.NombreCiudad));

            var list6 = await context
                 .RestaurantesTrx
                 .FromSqlRaw("call Sp_Ciudades(@opcion , @IDCiudad,@NombreCiudad, @d ,@e )", parameter.ToArray())
                 .ToListAsync();

            var json = "";

            foreach (var row in list6)
            {

                objRest.Add(new CiudadTrx
                {
                    out_cod = row.out_cod,
                    out_msj = row.out_msj
                });
            }

            json = JsonConvert.SerializeObject(objRest);

            return Content(json, "application/json");
        }




        /*
       * Metodo de Update 
       * parametro @Object  CiudadTrx
       * @IDCiudad usado en el where PK de la tabla
       * return json 
       */
        // PUT: api/CiudadPut
        [HttpPut]
        public async Task<ActionResult> PutCuidad([FromBody] CiudadTrx obj)
        {
            List<CiudadTrx> objRest = new List<CiudadTrx>();

            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "IV"));
            parameter.Add(new MySqlParameter("@IDCiudad", obj.IDCiudad));
            parameter.Add(new MySqlParameter("@NombreCiudad", obj.NombreCiudad));


            var list6 = await context
                 .RestaurantesTrx
                 .FromSqlRaw("call Sp_Ciudades(@opcion , @IDCiudad,@NombreCiudad, @d ,@e )", parameter.ToArray())
                 .ToListAsync();


            var json = "";

            foreach (var row in list6)
            {
                objRest.Add(new CiudadTrx
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
        * parametro @Object  CiudadTrx
        * return json 
        */
        // PUT:  api/CiudadDelete/5
        [HttpDelete] // 
        public async Task<ActionResult> DeleteCiudad(int IDCiudad)
        {
            List<CiudadTrx> objRest = new List<CiudadTrx>();


            var parameter = new List<MySqlParameter>();
            parameter.Add(new MySqlParameter("@opcion", "V"));
            parameter.Add(new MySqlParameter("@IDCiudad", IDCiudad));
            parameter.Add(new MySqlParameter("@NombreCiudad", ""));


            var list6 = await context
               .RestaurantesTrx
               .FromSqlRaw("call Sp_Ciudades(@opcion , @IDCiudad,@NombreCiudad, @d ,@e )", parameter.ToArray())
               .ToListAsync();

            var json = "";

            foreach (var row in list6)
            {
                objRest.Add(new CiudadTrx
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

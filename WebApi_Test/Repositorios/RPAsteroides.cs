using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace WebApi_Test.Repositorios
{
    public class RPAsteroides
    {
        public IEnumerable<Asteroide> ObtenerAsteroides()
        {
            string strFechaIni = DateTime.Now.ToString("yyyy-MM-dd");
            string strFechaFin = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

            var url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date=" + strFechaIni + "& end_date=" + strFechaFin + "&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            List<Asteroide> listAsteroides = new List<Asteroide>();

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            JObject obj = JObject.Parse(objReader.ReadToEnd());
                            JToken near_earth_objects = obj["near_earth_objects"];
                            
                            foreach (JToken jFechas in near_earth_objects)
                            {
                                foreach (JToken jItems in jFechas)
                                {
                                    foreach (JToken jAsteroides in jItems)
                                    {
                                        Asteroide asteroide = new Asteroide();
                                        asteroide.Nombre = jAsteroides["name"].ToString();
                                        asteroide.Planeta = jAsteroides["close_approach_data"][0]["orbiting_body"].ToString();

                                        decimal DiametroMin = Convert.ToDecimal(jAsteroides["estimated_diameter"]["kilometers"]["estimated_diameter_min"].ToString());
                                        decimal DiametroMax = Convert.ToDecimal(jAsteroides["estimated_diameter"]["kilometers"]["estimated_diameter_max"].ToString());
                                        asteroide.Diametro = (DiametroMin + DiametroMax) / 2;
                                        asteroide.Velocidad = Convert.ToDecimal(jAsteroides["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"].ToString());
                                        asteroide.Fecha = jAsteroides["close_approach_data"][0]["close_approach_date"].ToString();

                                        listAsteroides.Add(asteroide);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if(listAsteroides.Count > 3)
                listAsteroides = listAsteroides.OrderByDescending(x => x.Diametro).Take(3).ToList();

            return listAsteroides;
        }
    }
}

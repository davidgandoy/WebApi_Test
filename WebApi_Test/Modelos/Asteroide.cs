namespace WebApi_Test.Modelos
{
    public class Asteroide
    {
        /// <summary>
        /// Obtenido de "name"
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Tamaño medio calculado
        /// </summary>
        public decimal Diametro { get; set; }

        /// <summary>
        /// Velocidad relativa
        /// "close_approach_data:relative_velocity:kilometers_per_hour"
        /// </summary>
        public decimal Velocidad { get; set; }

        /// <summary>
        /// Fecha
        /// "close_approach_data:close_approach_date"
        /// </summary>
        public string Fecha { get; set; }

        /// <summary>
        /// Nombre del planeta
        /// "close_approach_date:orbiting_body"
        /// </summary>
        public string Planeta { get; set; }
    }
}

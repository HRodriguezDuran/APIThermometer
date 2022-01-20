namespace ApiThermometer.Models
{
    public class Thermometer
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public List<Thermometer> GetTemperature()
        {

            return Enumerable.Range(1, 5).Select(index => new Thermometer
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
            .ToList();
        }
    }
}

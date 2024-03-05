namespace FleetManagement.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public CarModel Car { get; set; }
        public string BrandName { get; set; }
        public int MaintainanceFrequency { get; set; }
        public Energy Energy { get; set; }
    }

    public enum Energy
    {
        Gasoline = 0,
        Diesel = 1,
        Electric = 2,
        Hybrid = 3
    }
}

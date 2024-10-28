namespace GarageMVC.Models.Vehicles
{
    public class VehicleCreateViewModel
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
        public int NumberOfWheels { get; set; }
    }
}


namespace GarageMVC.Models.Vehicles
{
    public interface IVehicle
    {
        int Id { get; set; }
        string RegistrationNumber { get; set; }
        string Color { get; set; }
        int NumberOfWheels { get; set; }
        string FuelType { get; set; }
        public string VehicleType => this.GetType().Name;
    }
}

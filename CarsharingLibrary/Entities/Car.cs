namespace CarsharingLibrary.Entities;

public partial class Car
{
    public int CarId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public int CarModelId { get; set; }

    public int YearOfManufacture { get; set; }

    public string Color { get; set; } = null!;

    public int Mileage { get; set; }

    public virtual ICollection<CarAvailability> CarAvailabilities { get; set; } = [];

    public virtual ICollection<CarCondition> CarConditions { get; set; } = [];

    public virtual CarModel CarModel { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = [];

    public virtual ICollection<RentalOrder> RentalOrders { get; set; } = [];
}

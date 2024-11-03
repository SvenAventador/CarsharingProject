namespace CarsharingLibrary.Entities;

public partial class Station
{
    public int StationId { get; set; }

    public string StationName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<CarAvailability> CarAvailabilities { get; set; } = [];

    public virtual ICollection<RentalOrder> RentalOrderEndStations { get; set; } = [];

    public virtual ICollection<RentalOrder> RentalOrderStartStations { get; set; } = [];
}

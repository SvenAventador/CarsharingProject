namespace CarsharingLibrary.Entities;

public partial class CarAvailability
{
    public int AvailabilityId { get; set; }

    public int CarId { get; set; }

    public int StationId { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Station Station { get; set; } = null!;
}

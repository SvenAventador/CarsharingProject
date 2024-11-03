namespace CarsharingLibrary.Entities;

public partial class RentalOrder
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public int EmployeeId { get; set; }

    public int StartStationId { get; set; }

    public int? EndStationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Station? EndStation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = [];

    public virtual Station StartStation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

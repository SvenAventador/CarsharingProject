namespace CarsharingLibrary.Entities;

public partial class CarCondition
{
    public int ConditionId { get; set; }

    public int CarId { get; set; }

    public string ConditionDescription { get; set; } = null!;

    public DateOnly DateReported { get; set; }

    public bool IsResolved { get; set; }

    public virtual Car Car { get; set; } = null!;
}

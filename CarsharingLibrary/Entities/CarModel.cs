namespace CarsharingLibrary.Entities;

public partial class CarModel
{
    public int CarModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public int Capacity { get; set; }

    public string TransmissionType { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = [];
}

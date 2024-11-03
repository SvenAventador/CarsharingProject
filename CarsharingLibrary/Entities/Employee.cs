namespace CarsharingLibrary.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public virtual ICollection<RentalOrder> RentalOrders { get; set; } = [];
}

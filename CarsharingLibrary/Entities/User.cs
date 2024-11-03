namespace CarsharingLibrary.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string DriverLicense { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = [];

    public virtual ICollection<RentalOrder> RentalOrders { get; set; } = [];
}

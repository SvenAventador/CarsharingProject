namespace CarsharingLibrary.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateOnly FeedbackDate { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

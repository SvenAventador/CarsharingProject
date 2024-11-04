using Microsoft.EntityFrameworkCore;

namespace CarsharingLibrary.Entities;

public partial class CarsharingDbContext : DbContext
{
    private readonly static CarsharingDbContext _context = new();
    public CarsharingDbContext()
    {
    }

    public CarsharingDbContext(DbContextOptions<CarsharingDbContext> options)
        : base(options)
    {
    }

    public static CarsharingDbContext GetContext() => _context ?? new();

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarAvailability> CarAvailabilities { get; set; }

    public virtual DbSet<CarCondition> CarConditions { get; set; }

    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RentalOrder> RentalOrders { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(local);Database=CarsharingDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0342EF32300B9");

            entity.ToTable("Car");

            entity.HasIndex(e => e.LicensePlate, "UQ__Car__026BC15C6936FB4B").IsUnique();

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.LicensePlate).HasMaxLength(10);

            entity.HasOne(d => d.CarModel).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Car_CarModel");
        });

        modelBuilder.Entity<CarAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__CarAvail__DA3979B1FD98E80B");

            entity.ToTable("CarAvailability");

            entity.Property(e => e.LastUpdated).HasColumnType("datetime");

            entity.HasOne(d => d.Car).WithMany(p => p.CarAvailabilities)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarAvailability_Car");

            entity.HasOne(d => d.Station).WithMany(p => p.CarAvailabilities)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarAvailability_Station");
        });

        modelBuilder.Entity<CarCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionId).HasName("PK__CarCondi__37F5C0CF5F97AB8A");

            entity.ToTable("CarCondition");

            entity.Property(e => e.ConditionDescription).HasMaxLength(255);

            entity.HasOne(d => d.Car).WithMany(p => p.CarConditions)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarCondition_Car");
        });

        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasKey(e => e.CarModelId).HasName("PK__CarModel__C585C08F1924C0A3");

            entity.ToTable("CarModel");

            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.ModelName).HasMaxLength(50);
            entity.Property(e => e.TransmissionType).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1116732531");

            entity.ToTable("Employee");

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Position).HasMaxLength(50);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD68AEC5D4C");

            entity.ToTable("Feedback");

            entity.Property(e => e.Comments).HasMaxLength(255);

            entity.HasOne(d => d.Car).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Car");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38C848C647");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_RentalOrder");
        });

        modelBuilder.Entity<RentalOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__RentalOr__C3905BCF70F9733B");

            entity.ToTable("RentalOrder");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Car).WithMany(p => p.RentalOrders)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalOrder_Car");

            entity.HasOne(d => d.Employee).WithMany(p => p.RentalOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalOrder_Employee");

            entity.HasOne(d => d.EndStation).WithMany(p => p.RentalOrderEndStations)
                .HasForeignKey(d => d.EndStationId)
                .HasConstraintName("FK_RentalOrder_EndStation");

            entity.HasOne(d => d.StartStation).WithMany(p => p.RentalOrderStartStations)
                .HasForeignKey(d => d.StartStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalOrder_StartStation");

            entity.HasOne(d => d.User).WithMany(p => p.RentalOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalOrder_User");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Station__E0D8A6BDEDC82B9C");

            entity.ToTable("Station");

            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.StationName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C2EC931E5");

            entity.ToTable("User");

            entity.HasIndex(e => e.Phone, "UQ__User__5C7E359E71EAF8CA").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D105341F669EF8").IsUnique();

            entity.Property(e => e.DriverLicense).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

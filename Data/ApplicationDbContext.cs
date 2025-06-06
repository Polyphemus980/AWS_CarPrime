﻿using Microsoft.EntityFrameworkCore;
using CarPrime.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarPrime.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Lease> Leases { get; set; }
    public DbSet<LeaseReturn> LeaseReturns { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<LeaseReturnPhoto> LeaseReturnPhotos { get; set; }
    
    public async Task SeedDb()
    {
        var customer = new Customer
        {
            Address = "Address",
            Email = "customer@customer.com",
            FirstName = "Cust",
            LastName = "Omer",
            Birthdate = DateTime.UtcNow,
            City = "England",
            Country = "Warsaw",
            CreatedAt = DateTime.UtcNow,
        };
        await Customers.AddAsync(customer);
        if (!CarModels.Any())
        {
            var carModels = new List<CarModel>
            {
                new CarModel { Brand = "Toyota", Name = "Corolla" },
                new CarModel { Brand = "Honda", Name = "Civic" },
                new CarModel { Brand = "Ford", Name = "Mustang" },
                new CarModel { Brand = "Chevrolet", Name = "Camaro" },
                new CarModel { Brand = "BMW", Name = "3 Series" },
                new CarModel { Brand = "Audi", Name = "A4" },
                new CarModel { Brand = "Mercedes-Benz", Name = "C-Class" },
                new CarModel { Brand = "Tesla", Name = "Model 3" },
                new CarModel { Brand = "Nissan", Name = "Altima" },
                new CarModel { Brand = "Hyundai", Name = "Elantra" }
            };
            await CarModels.AddRangeAsync(carModels);
        }
        if (!Cars.Any())
        {
            var cars = new List<Car>
            {
                new Car { ModelId = 1, ManufactureYear = new DateTime(2020, 1, 1) },
                new Car { ModelId = 2, ManufactureYear = new DateTime(2019, 5, 10) },
                new Car { ModelId = 3, ManufactureYear = new DateTime(2021, 3, 15) },
                new Car { ModelId = 4, ManufactureYear = new DateTime(2022, 6, 18) },
                new Car { ModelId = 5, ManufactureYear = new DateTime(2020, 9, 25) },
                new Car { ModelId = 6, ManufactureYear = new DateTime(2018, 11, 30) },
                new Car { ModelId = 7, ManufactureYear = new DateTime(2023, 4, 20) },
                new Car { ModelId = 8, ManufactureYear = new DateTime(2022, 7, 13) },
                new Car { ModelId = 9, ManufactureYear = new DateTime(2019, 12, 5) },
                new Car { ModelId = 10, ManufactureYear = new DateTime(2021, 8, 28) }
            };
            await Cars.AddRangeAsync(cars);
        }

        await SaveChangesAsync();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }

            builder.Entity<Lease>(entity =>
            {
                entity.HasOne(l => l.Leaser)
                    .WithMany(c => c.Leases)
                    .HasForeignKey("LeaserId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(l => l.Offer)
                    .WithOne()
                    .HasForeignKey<Lease>("OfferId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<LeaseReturn>(entity =>
            {
                entity.HasOne(lr => lr.Employee)
                    .WithMany(e => e.LeaseReturns)
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<LeaseReturnPhoto>(entity =>
            {
                entity.HasOne(p => p.LeaseReturn)
                    .WithMany(lr => lr.Photos)
                    .HasForeignKey("LeaseReturnId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<Offer>(entity =>
            {
                entity.HasOne(o => o.Car)
                    .WithOne(c => c.Offer)
                    .HasForeignKey<Offer>("CarId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(o => o.Company)
                    .WithMany()
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Offers)
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyLibrary.WebApi.Models;

public class MyLibraryAppDbContext : IdentityDbContext<IdentityUser>
{
    public MyLibraryAppDbContext(DbContextOptions<MyLibraryAppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Copy> Copies => Set<Copy>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Publisher> publishers => Set<Publisher>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Fine> Fines => Set<Fine>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookCategory> BookCategories => Set<BookCategory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // === Book Configurations ===
        builder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Isbn)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(b => b.Title)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(b => b.Subtitle)
                  .HasMaxLength(500);

            entity.Property(b => b.Language)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(b => b.Synopsis)
                  .HasMaxLength(int.MaxValue);

            entity.HasOne(b => b.Publisher)
                  .WithMany(p => p.Books)
                  .HasForeignKey(b => b.PublisherId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(b => b.Isbn).IsUnique();
        });

        // === Copy Configurations ===
        builder.Entity<Copy>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Barcode)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(c => c.Location)
                  .HasMaxLength(100);

            entity.Property(c => c.Condition)
                  .HasMaxLength(50);

            entity.HasOne(c => c.Book)
                  .WithMany(b => b.Copies)
                  .HasForeignKey(c => c.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(c => c.Barcode)
                  .IsUnique();
        });

        // === Author Configurations ===
        builder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.Nationality)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.Biography)
                  .HasMaxLength(int.MaxValue);
        });

        // === Publisher Configurations ===
        builder.Entity<Publisher>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.Country)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.WebSite)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.Email)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.PhoneNumber)
                  .HasMaxLength(50);
        });

        // === Category Configurations ===
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(c => c.Description)
                  .IsRequired()
                  .HasMaxLength(int.MaxValue);
        });

        // === User Configurations ===
        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FirstName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(u => u.LastName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.HasOne(u => u.AspNetUser)
                  .WithMany()
                  .HasForeignKey(u => u.AspNetUserId);
        });

        // === Loan Configurations ===
        builder.Entity<Loan>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.State)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.HasOne(l => l.Copy)
                  .WithMany(c => c.Loans)
                  .HasForeignKey(l => l.CopyId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.User)
                  .WithMany(u => u.Loans)
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // === Reservation Configurations ===
        builder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.State)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.HasOne(r => r.User)
                  .WithMany(u => u.Reservations)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Book)
                  .WithMany(b => b.Reservations)
                  .HasForeignKey(r => r.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.AssignedCopy)
                  .WithMany(c => c.AssignedReservations)
                  .HasForeignKey(r => r.IdAssignedCopy)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // === Fine Configurations ===
        builder.Entity<Fine>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Reason)
                  .HasMaxLength(100);

            entity.Property(f => f.Amount)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.Property(f => f.State)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.HasOne(f => f.Loan)
                  .WithMany(l => l.Fines)
                  .HasForeignKey(f => f.LoanId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.User)
                  .WithMany(u => u.Fines)
                  .HasForeignKey(f => f.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // === BookAuthor Configurations ===
        builder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(ba => new { ba.BookId, ba.AuthorId });

            entity.Property(ba => ba.RegisteredAt)
                  .IsRequired();
        });

        // === BookCategory Configurations ===
        builder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(bc => new { bc.BookId, bc.CategoryId });

            entity.Property(bc => bc.RegisteredAt)
                  .IsRequired();
        });
    }
}


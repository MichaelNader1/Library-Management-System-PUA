using System;
using System.Collections.Generic;
using System.ComponentModel;
using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models
{
    public class LibraryDbContext : IdentityDbContext<AdminUser>
    {
        private readonly ICurrentUserService _currentUserService;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////Date///////////


            modelBuilder.Entity<Book>()
                .Property(b => b.Reciving_Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Banned_User>()
                .Property(b => b.Ban_Start_Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Borrowing>()
                .Property(b => b.Date_Of_Borrowing)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Copying>()
                .Property(b => b.Start_Time)
                .HasDefaultValueSql("GETDATE()");

           modelBuilder.Entity<Discarding>()
                .Property(b => b.Discarding_Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Penalty>()
                .Property(b => b.Penalty_Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Reading>()
                .Property(b => b.Start_Time)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Transferring>()
                .Property(b => b.Transferring_Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Visits>()
                .Property(b => b.Visit_Date)
                .HasDefaultValueSql("GETDATE()");

            ////////////////Relations////////////////
            

            modelBuilder.Entity<Book_College>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Book_Colleges)
                .HasForeignKey(bc => bc.Book_ID);

            modelBuilder.Entity<Book_College>()
                .HasOne(bc => bc.College)
                .WithMany(c => c.Book_Colleges)
                .HasForeignKey(bc => bc.College_ID);

            modelBuilder.Entity<AdminUser>()
                .HasOne(u => u.Library)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LibraryID);

            modelBuilder.Entity<Curriculum>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Curriculums)
                .HasForeignKey(c => c.Department_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book_College>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Book_Colleges)
                .HasForeignKey(bc => bc.Book_ID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Book_Colleges)
                .WithOne()
                .HasForeignKey(bc => bc.Book_ID);



            modelBuilder.Entity<Book_College>()
                .HasOne(bc => bc.College)
                .WithMany(c => c.Book_Colleges)
                .HasForeignKey(bc => bc.College_ID);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Languages)
                .WithMany(l => l.books)
                .UsingEntity(j => j.ToTable("BookLanguage"));

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookSubheadings)
                .WithMany(s => s.books)
                .UsingEntity(j => j.ToTable("Book_Subheadings"));

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Publishers)
                .WithMany(p => p.Books)
                .UsingEntity(j => j.ToTable("Book_Publishers"));
            //////////////
            modelBuilder.Entity<BookLibraryBook>()
                .HasKey(blb => new { blb.BookID, blb.LibraryBookID });

            modelBuilder.Entity<BookLibraryBook>()
                .HasOne(blb => blb.Book)
                .WithMany(b => b.BookLibraryBooks)
                .HasForeignKey(blb => blb.BookID);

            modelBuilder.Entity<BookLibraryBook>()
                .HasOne(blb => blb.LibraryBook)
                .WithMany(lb => lb.BookLibraryBooks)
                .HasForeignKey(blb => blb.LibraryBookID);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.LibraryBook)
                .WithMany(lb => lb.Borrowing)
                .HasForeignKey(b => b.LibraryBookID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reading>()
                .HasOne(r => r.LibraryBook)
                .WithMany(lb => lb.Reading)  
                .HasForeignKey(r => r.LibraryBookID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Copying>()
                 .HasOne(c => c.LibraryBook)
                 .WithMany(lb => lb.Copyings)
                 .HasForeignKey(c => c.LibraryBookID)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Transferring>()
                  .HasOne(t => t.SourceLibraryBook)
                  .WithMany()
                  .HasForeignKey(t => t.SourceLibraryBookID)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transferring>()
                .HasOne(t => t.DestinationLibraryBook)
                .WithMany()
                .HasForeignKey(t => t.DestinationLibraryBookID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Discarding>()
                .HasOne(d => d.LibraryBook)
                .WithOne(lb => lb.Discarding)
                .HasForeignKey<Discarding>(d => d.LibraryBookID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdminUser>()
                .HasOne(u => u.Library)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LibraryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PowerCampusUser>()
                .ToView("vw_PowerCampusUser")
                .HasKey(p => p.User_Id);

            base.OnModelCreating(modelBuilder);
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }



        public DbSet<Book> Book { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Banned_User> BannedUser { get; set; }
        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Discarding> Discarding { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<LibraryBook> LibraryBook { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subheading> Subheadings { get; set; }
        public DbSet<Supply_Method> SupplyMethods { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Borrowing> Borrowing { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Transferring> Transferring { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                var log = new Log
                {
                    UserName = _currentUserService.UserName,
                    TableName = entry.Entity.GetType().Name,
                    OperationType = entry.State.ToString()
                };

                Logs.Add(log);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

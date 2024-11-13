using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ClientCard> Clients { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Cover> Covers { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookSubject> BookSubjects { get; set; }
    public DbSet<User> Users { get; set; }

    public LibraryContext() {}

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=postgres.db;Port=5432;Database=bookmaster_DB;Username=postgres;Password=1234");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookSubject>()
            .HasKey(bs => new { bs.BookId, bs.SubjectId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

        modelBuilder.Entity<BookSubject>()
            .HasOne(bs => bs.Book)
            .WithMany(b => b.BookSubjects)
            .HasForeignKey(bs => bs.BookId);

        modelBuilder.Entity<BookSubject>()
            .HasOne(bs => bs.Subject)
            .WithMany(s => s.BookSubjects)
            .HasForeignKey(bs => bs.SubjectId);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.ClientCard)
            .WithMany()
            .HasForeignKey(i => i.ClientCardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>().HasKey(a => a.Id);
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<ClientCard>().HasKey(c => c.Id);
        modelBuilder.Entity<Issue>().HasKey(i => i.Id);
        modelBuilder.Entity<Loan>().HasKey(l => l.Id);
        modelBuilder.Entity<Subject>().HasKey(s => s.Id);
        modelBuilder.Entity<User>().HasKey(s => s.Id);

        base.OnModelCreating(modelBuilder);
    }
    
}

using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Author> BookAuthors { get; set; }
    public DbSet<Subject> BookSubjects { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookmasterDB;Username=postgres;Password=1234");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookKey, ba.AuthorKey });

        modelBuilder.Entity<BookSubject>()
            .HasKey(bs => new { bs.BookKey, bs.SubjectId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookKey);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorKey);

        modelBuilder.Entity<BookSubject>()
            .HasOne(bs => bs.Book)
            .WithMany(b => b.BookSubjects)
            .HasForeignKey(bs => bs.BookKey);

        modelBuilder.Entity<BookSubject>()
            .HasOne(bs => bs.Subject)
            .WithMany(s => s.BookSubjects)
            .HasForeignKey(bs => bs.SubjectId);
        modelBuilder.Entity<Author>().HasKey(a => a.Key);
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Issue>().HasKey(i => i.Id);
        modelBuilder.Entity<Loan>().HasKey(l => l.Id);
        modelBuilder.Entity<Subject>().HasKey(s => s.Id);
    }
}

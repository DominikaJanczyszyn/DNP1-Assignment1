using Assignment1.Models;

namespace EfcDataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
public class PostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Post.db");
        //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Username);
        modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
        modelBuilder.Entity<Vote>().HasKey(vote => vote.Id);
    }
    
}
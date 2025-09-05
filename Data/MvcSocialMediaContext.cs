using Microsoft.EntityFrameworkCore;
using MvcSocialMedia.Models;

namespace MvcSocialMedia.Data;

public class MvcSocialMediaContext : DbContext
{
    public MvcSocialMediaContext (DbContextOptions<MvcSocialMediaContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Following> Followings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Like>()
            .HasIndex(l => new { l.UserId, l.PostId })
            .IsUnique();

        builder.Entity<Following>()
            .HasIndex(f => new { f.FollowerId, f.FolloweeId })
            .IsUnique();

        builder.Entity<Following>()
            .HasOne(f => f.Follower)
            .WithMany(u => u.Followees)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Following>()
            .HasOne(f => f.Followee)
            .WithMany(u => u.Followers)
            .HasForeignKey(f => f.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Post>()
            .HasMany(p => p.Likes)
            .WithOne(l => l.Post)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
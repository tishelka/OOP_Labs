using Microsoft.EntityFrameworkCore;
using OOP_ICT.Fifth.Entity;
namespace OOP_ICT.Fifth.Models;

public class PokerDbContext : DbContext
{
    public PokerDbContext(DbContextOptions<PokerDbContext> options)
        : base(options)
    {
    }

    public DbSet<PlayerEntity> Players { get; set; }
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<PlayerCardEntity> PlayerCards { get; set; }
    public DbSet<GamePlayerEntity> GamePlayers { get; set; }
    public DbSet<GameCardEntity> GameCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GamePlayerEntity>()
            .HasKey(g => new { g.GameId, g.PlayerId });
        modelBuilder.Entity<PlayerCardEntity>()
            .HasKey(p => new { p.PlayerId, p.CardId });

        modelBuilder.Entity<GamePlayerEntity>()
            .HasOne(gp => gp.Game)
            .WithMany(g => g.GamePlayers)
            .HasForeignKey(gp => gp.GameId);

        modelBuilder.Entity<GamePlayerEntity>()
            .HasOne(gp => gp.Player)
            .WithMany(p => p.GamePlayers)
            .HasForeignKey(gp => gp.PlayerId);

        modelBuilder.Entity<PlayerCardEntity>()
            .HasOne(pc => pc.Player)
            .WithMany(p => p.PlayerCards)
            .HasForeignKey(pc => pc.PlayerId);

        modelBuilder.Entity<PlayerCardEntity>()
            .HasOne(pc => pc.Card)
            .WithMany(c => c.PlayerCards)
            .HasForeignKey(pc => pc.CardId);

        modelBuilder.Entity<GameCardEntity>()
            .HasKey(gc => new { gc.GameId, gc.CardId });

        modelBuilder.Entity<GameCardEntity>()
            .HasOne(gc => gc.Game)
            .WithMany(g => g.CommunityCards)
            .HasForeignKey(gc => gc.GameId);

        modelBuilder.Entity<GameCardEntity>()
            .HasOne(gc => gc.Card)
            .WithMany(c => c.Games)
            .HasForeignKey(gc => gc.CardId);
    }

}

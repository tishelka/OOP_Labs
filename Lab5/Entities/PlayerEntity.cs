namespace OOP_ICT.Fifth.Entity;
using System.ComponentModel.DataAnnotations;

public class PlayerEntity
{
    [Key]
    public int PlayerId { get; set; }
    public string Name { get; set; }
    public int Balance { get; set; }

    // Navigation properties
    public ICollection<GamePlayerEntity> GamePlayers { get; set; }
    public ICollection<PlayerCardEntity> PlayerCards { get; set; }
}
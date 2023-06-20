namespace OOP_ICT.Fifth.Entity;
using System.ComponentModel.DataAnnotations;

public class GameEntity
{
    [Key]
    public int Id { get; set; }
    public int Pot { get; set; }

    // Navigation properties
    public ICollection<GamePlayerEntity> GamePlayers { get; set; }
    public ICollection<GameCardEntity> CommunityCards { get; set; }
}
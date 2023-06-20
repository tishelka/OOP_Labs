namespace OOP_ICT.Fifth.Entity;
using System.ComponentModel.DataAnnotations;

public class CardEntity
{
    [Key]
    public int CardId { get; set; }
    public string Suit { get; set; }
    public string Rank { get; set; }

    // Navigation properties
    public ICollection<PlayerCardEntity> PlayerCards { get; set; }
    public ICollection<GameCardEntity> Games { get; set; }
}
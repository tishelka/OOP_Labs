namespace OOP_ICT.Fifth.Entity;

public class PlayerCardEntity
{
    public int PlayerId { get; set; }
    public PlayerEntity Player { get; set; }

    public int CardId { get; set; }
    public CardEntity Card { get; set; }
}
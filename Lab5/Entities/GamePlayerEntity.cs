namespace OOP_ICT.Fifth.Entity;

public class GamePlayerEntity
{
    public int GameId { get; set; }
    public GameEntity Game { get; set; }

    public int PlayerId { get; set; }
    public PlayerEntity Player { get; set; }
}
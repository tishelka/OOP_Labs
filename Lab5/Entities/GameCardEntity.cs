namespace OOP_ICT.Fifth.Entity;


public class GameCardEntity
    {
        public int GameId { get; set; }
        public GameEntity Game { get; set; }

        public int CardId { get; set; }
        public CardEntity Card { get; set; }
    }

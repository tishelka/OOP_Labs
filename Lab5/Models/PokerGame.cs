namespace OOP_ICT.Fifth.Models;
using OOP_ICT.Fourth.Models;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Models;
using OOP_ICT.Third.Interfaces;


public class PokerGame
{
    private readonly PokerDbContext _context;
    public List<IPlayer> Players { get; private set; }
    public List<Card> CommunityCards { get; private set; }
    public int Pot { get; set; }
    public IBank Bank { get; private set; }
    private readonly PokerCasino _casino;

    public PokerGame(List<IPlayer> players, PokerDbContext context)
    {
        Players = players;
        _casino = new PokerCasino();
        Bank = new CasinoBankAdapter(_casino);
        CommunityCards = new List<Card>();
        Pot = 0;
        _context = context;
    }

    public void LeaveGame(IPlayer player)
    {
        Players.Remove(player);
        Bank.Withdraw(player, player.BetAmount);
        
        var playerEntity = _context.Players.FirstOrDefault(p => p.Name == player.Name);
        if(playerEntity != null)
        {
            _context.Players.Remove(playerEntity);
            _context.SaveChanges();
        }
    }

    public IPlayer DetermineWinner()
    {
        var winner = HandEvaluator.Evaluate(Players, CommunityCards);
        if(winner != null)
        {
            var dbPlayer = _context.Players.Find(winner.Name);
            dbPlayer.Balance += Pot;
            _context.SaveChanges();
        }

        return winner;
    }
}


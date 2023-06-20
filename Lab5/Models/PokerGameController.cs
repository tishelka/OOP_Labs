namespace OOP_ICT.Fifth.Models;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Models;
using OOP_ICT.Interfaces;
using OOP_ICT.Fifth.Entity;

public class PokerGameController
{
    private readonly IDealer _dealer;
    private readonly IGameConsole _gameConsole;
    private PokerGame _game;
    private PokerDbContext _context;

    public PokerGameController(IDealer dealer, IGameConsole gameConsole, PokerDbContext context)
    {
        _dealer = dealer;
        _gameConsole = gameConsole;
        _context = context;
    }

    public void StartGame(List<IPlayer> players)
    {
        _game = new PokerGame(players, _context); 
        _dealer.InitializeCardDeck();
        UserDeck shuffledDeck = _dealer.CreateShuffledUserDeck();
        _context.Games.Add(new GameEntity { Pot = _game.Pot });
        _context.SaveChanges();

        foreach (var player in _game.Players)
        {
            player.JoinGame();
            _gameConsole.WritePlaceBetMessage(player.Name);
            int betAmount = int.Parse(_gameConsole.ReadLine());
            bool canPlaceBet = _game.Bank.CanPlaceBet(player, betAmount);
            if (canPlaceBet)
            {
                _game.Bank.Withdraw(player, betAmount);
                player.PlaceBet(betAmount);
                _game.Pot += betAmount;
                List<Card> playerCards = new List<Card> { shuffledDeck.Cards[0], shuffledDeck.Cards[1] };
                shuffledDeck.Cards.RemoveRange(0, 2);
                player.DealCards(playerCards);
            }
            else
            {
                _gameConsole.WriteInsufficientFundsMessage(player.Name);
                _game.LeaveGame(player);
                if (_game.Players.Count == 0)
                {
                    _gameConsole.WriteAllPlayersLostMessage();
                    return;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            _game.CommunityCards.Add(shuffledDeck.Cards[0]);
            shuffledDeck.Cards.RemoveAt(0);
        }
        _gameConsole.WriteCommunityCardsMessage(_game.CommunityCards);
        ProcessBettingRound();

        _game.CommunityCards.Add(shuffledDeck.Cards[0]);
        shuffledDeck.Cards.RemoveAt(0);
        _gameConsole.WriteCommunityCardsMessage(_game.CommunityCards);
        ProcessBettingRound();

        _game.CommunityCards.Add(shuffledDeck.Cards[0]);
        shuffledDeck.Cards.RemoveAt(0);
        _gameConsole.WriteCommunityCardsMessage(_game.CommunityCards);
        ProcessBettingRound();

        IPlayer winner = _game.DetermineWinner();
        if (winner != null)
        {
            _gameConsole.WriteWinnerMessage(winner.Name, _game.Pot);
            _game.Bank.Deposit(winner, _game.Pot);
        }
        else
        {
            _gameConsole.WriteNoWinnerMessage();
        }
    }

    private void ProcessBettingRound()
    {
        foreach (var player in _game.Players)
        {
            _gameConsole.WritePlaceBetMessage(player.Name);
            int betAmount = int.Parse(_gameConsole.ReadLine());
            bool canPlaceBet = _game.Bank.CanPlaceBet(player, betAmount);
            if (canPlaceBet)
            {
                _game.Bank.Withdraw(player, betAmount);
                _game.Pot += betAmount;
                var gameEntity = _context.Games.FirstOrDefault();
                gameEntity.Pot = _game.Pot;
                _context.SaveChanges();
            }
            else
            {
                _gameConsole.WriteInsufficientFundsMessage(player.Name);
                _game.LeaveGame(player);
            }
        }
    }
}

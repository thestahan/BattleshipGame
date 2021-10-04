using Core.Entities;

namespace Core.Interfaces
{
    public interface IGameService
    {
        Game InitGame();
        bool GameHasFinished(Game game, bool playerOneMakingMove);
    }
}

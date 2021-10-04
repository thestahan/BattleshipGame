using Core.Entities;

namespace Core.Interfaces
{
    public interface IGameService
    {
        Game InitGame();
        Shot MakeShot(Game game, string position);
    }
}

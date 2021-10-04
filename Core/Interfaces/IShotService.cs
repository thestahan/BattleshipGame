using Core.Entities;

namespace Core.Interfaces
{
    public interface IShotService
    {
        Shot MakeShot(Game game, string position);
        bool ShipGotSank(Board enemyBoard, string position);
    }
}

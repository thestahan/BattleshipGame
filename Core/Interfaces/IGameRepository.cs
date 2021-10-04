using Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGameRepository
    {
        Task<bool> AddGameAsync(Game game, CancellationToken cancellationToken);
        Task<Game> GetGameByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateBoardAsync(Board game, CancellationToken cancellationToken);
        Task UpdateGameNextPlayer(Game game, CancellationToken cancellationToken);
        Task<bool> AddShotAsync(Shot shot, CancellationToken cancellationToken);
    }
}

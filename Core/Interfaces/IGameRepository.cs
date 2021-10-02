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
    }
}

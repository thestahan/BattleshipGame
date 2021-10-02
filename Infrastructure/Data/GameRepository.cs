using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddGameAsync(Game game, CancellationToken cancellationToken)
        {
            try
            {
                await _context.AddAsync(game, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Game> GetGameByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Games.Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

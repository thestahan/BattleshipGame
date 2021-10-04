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

        public async Task<bool> AddShotAsync(Shot shot, CancellationToken cancellationToken)
        {
            try
            {
                await _context.AddAsync(shot, cancellationToken);

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
                .Include("PlayerOne.SelfBoard.Ships")
                .Include("PlayerOne.SelfBoard.Shots")
                .Include("PlayerOne.EnemyBoard.Ships")
                .Include("PlayerOne.EnemyBoard.Shots")
                .Include("PlayerTwo.SelfBoard.Ships")
                .Include("PlayerTwo.SelfBoard.Shots")
                .Include("PlayerTwo.EnemyBoard.Ships")
                .Include("PlayerTwo.EnemyBoard.Shots")
                //.Include(x => x.PlayerOne)
                //    .ThenInclude(x => x.SelfBoard)
                //        .ThenInclude(x => x.Ships)
                //.Include(x => x.PlayerOne)
                //    .ThenInclude(x => x.SelfBoard)
                //        .ThenInclude(x => x.Shots)
                //.Include(x => x.PlayerOne)
                //    .ThenInclude(x => x.EnemyBoard)
                //.Include(x => x.PlayerTwo)
                //    .ThenInclude(x => x.SelfBoard)
                //.Include(x => x.PlayerTwo)
                //    .ThenInclude(x => x.EnemyBoard)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateBoardAsync(Board board, CancellationToken cancellationToken)
        {
            _context.Entry(board).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateGameNextPlayer(Game game, CancellationToken cancellationToken)
        {
            _context.Games.Attach(game);

            _context.Entry(game).Property(x => x.NextTurnPlayerId).IsModified = true;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

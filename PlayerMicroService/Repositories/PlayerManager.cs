using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MicroService2.Database;

namespace PlayerMicroService.Repositories
{
    public class PlayerManager : IDataRepository<Player>
    {
        readonly DatabaseContext _playercontext;
        public PlayerManager(DatabaseContext context)
        {
            _playercontext = context;
        }
        public IEnumerable<Player> GetAll()
        {
            return _playercontext.Players.ToList();
        }
        public void Add(Player player)
        {
            _playercontext.Players.Add(player);
            _playercontext.SaveChanges();
        }
        

        public bool Delete(int id)
        {
            Player player = _playercontext.Players.Find(id);
            if(player == null)
            {
                return false;
            }
            _playercontext.Players.Remove(player);
            _playercontext.SaveChanges();
            return true;
        }
    }
}

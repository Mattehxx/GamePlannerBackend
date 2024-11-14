using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IGameManager : IManager<Game>
    {
        public Task<Game> DisableGame(int gameid,bool isDisable);
    }
}

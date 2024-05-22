using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        PlayerStaticData ForPlayer(PlayerTypeId typeID);
    }
}
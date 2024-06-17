using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        PlayerStaticData ForPlayer(PlayerTypeId typeID);
    }
}
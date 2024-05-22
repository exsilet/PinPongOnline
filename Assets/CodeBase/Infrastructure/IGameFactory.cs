using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory : IService
    {
        GameObject CreateHubMenu();
        GameObject CreateHudMenuPlayer();
        GameObject CreateHudGame();
        GameObject CreatePlayingField();
        GameObject CreateBall();
        GameObject Hero1 { get; }
        GameObject Hero2 { get; }
        GameObject HudMenu { get; }
        GameObject GamePlayingField { get; }
        GameObject Ball { get; }
        GameObject CreateHero(PlayerStaticData staticData, SkillStaticData skillData);
    }
}
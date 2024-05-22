using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState, TPayload>(TPayload payload, PlayerStaticData payload1, SkillStaticData payload2) where TState : class, IPayloadedState1<TPayload, PlayerStaticData, SkillStaticData>;
    }
}
using Fusion;

namespace CodeBase.Photon
{
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer Local { get; set; }
        
        public override void Spawned()
        {
            if (Object.HasInputAuthority) 
                Local = this;
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority) 
                Runner.Despawn(Object);
        }
    }
}
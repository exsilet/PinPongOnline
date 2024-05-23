using Mirror;
using UnityEngine;

namespace CodeBase.Photon
{
    public class NetworkPong : NetworkManager
    {
        GameObject ball;

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            if (ball != null)
                NetworkServer.Destroy(ball);
            
            base.OnServerDisconnect(conn);
        }
    }
}
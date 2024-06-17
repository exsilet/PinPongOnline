using Fusion;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    public class PlayerDataNetworked : NetworkBehaviour
    {
        [Networked] public NetworkString<_16> NickName { get; private set; }
        [Networked] public int Score { get; private set; }
    }
}
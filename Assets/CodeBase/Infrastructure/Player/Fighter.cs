using CodeBase.Infrastructure.StaticData;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PlayerMoney))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private PlayerMoney _playerMoney;
        
        private Inventory _inventory;
        private PhotonView _photonView;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillPlayerData;
        private SkillStaticData _skillData;
        
        public PhotonView PhotonView => _photonView;

        public void Construct(PlayerStaticData data)
        {
            _playerData = data;
            _skillPlayerData = data.SkillData;
        }
        
        private void Start()
        {
            _photonView ??= GetComponent<PhotonView>();
        }
    }
}
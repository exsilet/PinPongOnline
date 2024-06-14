using Photon.Pun;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    public class Pun2PlayerSync : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private MonoBehaviour[] _localScripts;
        
        private Vector3 _latestPos;

        private void Start()
        {
            if (photonView.IsMine)
            {
            }
            else
            {
                for (int i = 0; i < _localScripts.Length; i++)
                {
                    _localScripts[i].enabled = false;
                }
            }
        }

        private void Update()
        {
            if (photonView.IsMine) return;
            
            transform.position = Vector3.Lerp(transform.position, _latestPos, Time.deltaTime * 5);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
            }
            else
            {
                _latestPos = (Vector3)stream.ReceiveNext();
            }
        }
    }
}
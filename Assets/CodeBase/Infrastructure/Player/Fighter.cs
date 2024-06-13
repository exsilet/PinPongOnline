using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private Transform _targetActivatedMirror;
        [SerializeField] private Transform _targetCenter;
        
        public bool PositionRight;
        
        private Inventory _inventory;
        private BallMovet _ball;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillPlayerData;
        private SkillStaticData _skillData;
        private float _targetPosition = 4.3f;
        private float _ballPosition;
        private float _mirrorPosition;
        private bool _touched;
        private bool _activatedMirror;
        private GameObject _mirrorActive;
        public GameObject TargetPosition => _targetActivatedMirror.gameObject;
        public bool TouchedPlayer => _touched;
        public bool ActivatedSkill => _activatedMirror;

        public void Construct(PlayerStaticData data, BallMovet ball)
        {
            _playerData = data;
            _skillPlayerData = data.SkillData;
            _ball = ball;
        }
        
        private void Start()
        {
            if (transform.position.x > 0)
            {
                SwapPosition(_targetActivatedMirror, _targetActivatedMirror.localPosition.x);
                SwapPositionCenter(_targetCenter, _targetCenter.localPosition.x);
            }

            _mirrorActive = _targetActivatedMirror.gameObject;
            _mirrorPosition = _targetActivatedMirror.position.x;
        }

        private void Update()
        {
            _ballPosition = _ball.transform.position.x;

            if (transform.position.x > 0)
            {
                MirrorRight();   
                TouchedPlayerRight();
            }
            else
            {
                MirrorLeft();
                TouchedPlayerLeft();
            }

        }

        private void MirrorRight()
        {
            if (_skillPlayerData.Type == SkillTypeId.Mirror)
            {
                _activatedMirror = _ball.transform.position.x <= _targetActivatedMirror.position.x;
            }
        }
        
        private void MirrorLeft()
        {
            if (_skillPlayerData.Type == SkillTypeId.Mirror)
            {
                _activatedMirror = _ball.transform.position.x >= _targetActivatedMirror.position.x;
            }
        }

        private void SwapPositionCenter(Transform target, float newPosition)
        {
            target.position = new Vector2(newPosition- target.localPosition.x, target.position.y);
        }

        private void SwapPosition(Transform target, float newPosition)
        {
            target.position = new Vector2(newPosition - target.localPosition.x - _targetPosition, target.position.y);
        }

        private void TouchedPlayerRight()
        {
            if (_ball.transform.position.x <= 0)
            {
                _touched = false;
            }

            PositionRight = false;
        }

        private void TouchedPlayerLeft()
        {
            if (_ball.transform.position.x >= 0)
            {
                _touched = false;
            }
            
            PositionRight = true;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<BallMovet>())
            {
                _touched = true;
            }
        }
    }
}
using System;
using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Skill
{
    public class ActivateSkill : MonoBehaviour
    {
        [SerializeField] private Image _iconCooldown;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        private float _cooldownTime = 0;
        private float _skillCooldown = 0;
        private SkillStaticData _skill;
        private bool _skillAvailable = true;
        private bool _cooldownVisible = false;
        private BallMovet _ball;
        private Fighter _fighter;

        private GameObject _mirrorActive;

        private void Start()
        {
            if (_skill == null)
            {
                _button.interactable = false;
                _icon.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_skillAvailable && _cooldownVisible)
            {
                _cooldownTime -= Time.deltaTime;
                _iconCooldown.fillAmount = _cooldownTime / _skillCooldown;

                if (_cooldownTime <= 0)
                {
                    _skillAvailable = true;
                    _cooldownVisible = false;
                    _iconCooldown.Deactivate();
                }
            }
        }

        public void Construct(SkillStaticData skill, BallMovet ball, Fighter fighter, GameObject mirror)
        {
            if (skill != null)
            {
                _fighter = fighter;
                _skill = skill;
                _ball = ball;
                _skillCooldown = skill.CooldownTime;
                _mirrorActive = mirror;
            }
        }

        public void Activated()
        {
            if (!_fighter.ActivatedSkill) return;

            switch (_skill.Type)
            {
                case SkillTypeId.Mirror:
                    //_skill.Activate(_ball.gameObject, _fighter.TargetPosition, _fighter.PositionRight);
                    _mirrorActive.SetActive(true);
                    StartCooldown();
                    break;
            }
            
            if (!_fighter.TouchedPlayer) return;

            switch (_skill.Type)
            {
                case SkillTypeId.Attacking:
                    break;
                case SkillTypeId.Protective:
                    break;
                case SkillTypeId.FireBoll:
                    _skill.Activate(_ball.gameObject);
                    StartCooldown();
                    break;
                case SkillTypeId.DirectBlow:
                    _skill.Activate(_ball.gameObject, _fighter.PositionRight);
                    StartCooldown();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartCooldown()
        {
            _skillAvailable = false;
            _cooldownVisible = true;
            _iconCooldown.fillAmount = 1f;
            _iconCooldown.Activate();

            _cooldownTime = _skillCooldown;
        }
    }
}
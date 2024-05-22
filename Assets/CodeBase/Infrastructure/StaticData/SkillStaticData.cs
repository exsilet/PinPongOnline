using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "StaticData/Skill")]
    public class SkillStaticData : ScriptableObject
    {
        public Sprite UIIcon;
        public string Label;
        public string Description;
        public int Price;

        [SerializeField] private int _count;
        
        public SkillTypeId Type;
        public bool IsDefault;
        public bool IsByu;

        public int Count => _count;
    }
}
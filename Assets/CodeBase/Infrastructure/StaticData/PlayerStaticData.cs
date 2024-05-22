using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "StaticData/PlayerID")]
    public class PlayerStaticData : ScriptableObject
    {
        public Sprite Icon;
        public GameObject Prefab;
        public PlayerTypeId PlayerTypeId;
        public SkillStaticData SkillData;
    }
}

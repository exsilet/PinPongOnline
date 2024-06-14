using System;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class OpenSkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _icon2;
        [SerializeField] private Shop _shop;
        [SerializeField] private Button _byuSkill;
        
        private bool _isInitialized;
        private SkillStaticData _skillStaticData;
        private SkillView _skillView;

        private void Start()
        {
            this.Deactivate();
            _byuSkill.Add(Byu);
        }

        private void OnDisable()
        {
            _skillStaticData = null;
            _icon.sprite = null;
            _icon2.sprite = null;
            _label.text = null;
            _description.text = null;
            _isInitialized = false;
            _byuSkill.Remove(Byu);
        }

        public void Initialize(SkillStaticData skillStaticData, SkillView skillView)
        {
            _skillStaticData = skillStaticData;
            _skillView = skillView;
            
            _icon.sprite = skillStaticData.UIIcon;
            _icon2.sprite = skillStaticData.UIIcon;
            _label.text = skillStaticData.Label;
            _description.text = skillStaticData.Description;
            _price.text = skillStaticData.Price.ToString();

            _isInitialized = true;
        }

        private void Byu()
        {
            _shop.TrySellBuy(_skillStaticData, _skillView);
            this.Deactivate();
        }
    }
}
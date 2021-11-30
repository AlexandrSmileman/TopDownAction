using System;
using System.Collections;
using System.Collections.Generic;
using TopDownAction;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownAction
{
    public class ProductionPanel : MonoBehaviour
    {
        private HomeBase _homebase;

        [SerializeField] private Image unitIcon;
        [SerializeField] private Text unitName;
        [SerializeField] private Text unitHealth;
        [SerializeField] private Text unitDamage;
        [SerializeField] private Text unitCount;
        [SerializeField] private Image productionProgress;

        private CreatureParameters _parameters;
        internal void SetHomeBase(HomeBase home)
        {
            _homebase = home;
            _parameters = _homebase.CreatureParameters;
            unitIcon.sprite = _parameters.icon;
            unitName.text = _parameters.name;
            unitHealth.text = _parameters.health.ToString();
            unitDamage.text = _parameters.damage.ToString();
            unitCount.text = _parameters.ToString();
        }

        public void AddUnits(int count)
        {
            _homebase.AddUnits(count);
        }

        public void AddUnits(AddCountButton addButton)
        {
            _homebase.AddUnits(addButton.Count);
        }

        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            if (_homebase != null)
            {
                unitCount.text = _homebase.CountToProduce.ToString();
                productionProgress.fillAmount = _homebase.ProductionState;

            }
        }

        private void OnEnable()
        {
            UpdateProgress();
        }
    }
}
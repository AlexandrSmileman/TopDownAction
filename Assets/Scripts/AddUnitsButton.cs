using UnityEngine;
using UnityEngine.UI;

namespace TopDownAction
{
    public class AddUnitsButton : MonoBehaviour
    {
        [SerializeField] private int count;
        private ProductionPanel _productionPanel;
        [SerializeField] private Text Text;

        private void Awake()
        {
            Text.text = count.ToString();
            _productionPanel = GetComponentInParent<ProductionPanel>();
        }

        public void AddUnits()
        {
            _productionPanel.AddUnits(count);
        }
    }
}
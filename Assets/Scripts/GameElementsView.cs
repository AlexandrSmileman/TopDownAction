using UnityEngine;

namespace TopDownAction
{
    public class GameElementsView : Singleton<GameElementsView>
    {
        [SerializeField] private ViewSetting viewSetting;
        
        public void AddSelectionMarker(ISelectable selectable, Transform transform)
        {
            var selectionMarker =
                Instantiate(viewSetting.selectionMarkerPrefab, transform).GetComponent<SelectionMarker>();

            selectionMarker.transform.localPosition = -Vector3.up;// * 1.5f;
            selectionMarker.Constructor(selectable);
        }

        public void AddHealthBar(IDestroyable destroyable, Transform transform)
        {
            var healthBar = Instantiate(viewSetting.healthBarPrefab, transform).GetComponent<HealthBar>();
            healthBar.transform.localPosition = Vector3.up * 1.5f;
            healthBar.Constructor(destroyable);
        }


    }
}
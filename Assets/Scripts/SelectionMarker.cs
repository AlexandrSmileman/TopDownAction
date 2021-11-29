using UnityEngine;

namespace TopDownAction
{
    public class SelectionMarker : MonoBehaviour
    {
        private ISelectable _selectable;

        public void Constructor(ISelectable selectable)
        {
            _selectable = selectable;
            _selectable.OnDeselect += OnDeselect;
        }
        public void OnDeselect()
        {
            _selectable.OnDeselect -= OnDeselect;
            Destroy(gameObject);
        }
    }
}
using System;
using UnityEngine;

namespace TopDownAction
{
    public class Selectable : MonoBehaviour, ISelectable
    {
        public event Action OnSelect;
        public event Action OnDeselect;
        private bool _selected;
        private GameElementsView _elementsView;
        private Transform _parentTransform;

        public void Constructor(Transform parentTransform)
        {
            _parentTransform = parentTransform;
            _elementsView = GameElementsView.Instance;
            InputManager.OnSelectAllUnitsInCameraView += OnSelectAllUnitsInCameraView;
        }
        
        public void Select()
        {
            _selected = true;
            OnSelect?.Invoke();
            _elementsView.AddSelectionMarker(this, _parentTransform);
        }

        public void Deselect()
        {
            _selected = false;
            OnDeselect?.Invoke();
        }

        public void OnDestroy()
        {
            Deselect();
            InputManager.OnSelectAllUnitsInCameraView -= OnSelectAllUnitsInCameraView;
        }

        public void SwitchSelection()
        {
            if (_selected)
                Deselect();
            else
                Select();
        }

        public void OnSelectAllUnitsInCameraView(Camera camera)
        {
            if(!_selected && CameraController.PointInCameraViewport(camera, _parentTransform.position))
                Select();
        }
    }
}
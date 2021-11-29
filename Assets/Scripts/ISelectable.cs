using System;
using UnityEngine;

namespace TopDownAction
{
    public interface ISelectable
    {
        public event Action OnSelect;
        public event Action OnDeselect;
        
        public void Deselect();

        public void Select();

        public void SwitchSelection();

        public void OnSelectAllUnitsInCameraView(Camera camera);
    }
}
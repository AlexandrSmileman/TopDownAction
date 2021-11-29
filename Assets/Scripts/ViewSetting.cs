using UnityEngine;

namespace TopDownAction
{
    [CreateAssetMenu(fileName = "ViewSetting", menuName = "Scriptable/ViewSetting")]
    public class ViewSetting : ScriptableObject
    {
        public GameObject selectionMarkerPrefab;
        public GameObject healthBarPrefab;
    }
}
using UnityEngine;

namespace TopDownAction
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Scriptable/Creature")]
    public class CreatureParameters : ScriptableObject
    {
        public new string name;
        public int health;
        public int damage;
        public GameObject prefab;
        public Sprite icon;
    }
}
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TopDownAction
{
    public class AddCountButton : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private Text countText;
        public int Count => count;

        private void Awake()
        {
            countText.text = Count.ToString();
        }
    }
}
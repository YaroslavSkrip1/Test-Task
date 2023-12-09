using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MapItemView : MonoBehaviour
    {
        [SerializeField] private Button mapButton;
        [SerializeField] private Image redPoint;
        public void EnableMap(bool isActive)
        {
            mapButton.interactable = isActive;
            redPoint.gameObject.SetActive(isActive);
        }
        public void EnableLastMap(bool isActive)
        {
            mapButton.interactable = isActive;
            redPoint.gameObject.SetActive(false);
        }
    }
}

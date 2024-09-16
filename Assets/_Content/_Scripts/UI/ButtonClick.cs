using PetroGXR.WhisperingDoubles.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PetroGXR.WhisperingDoubles.UI
{
    public class ButtonClick : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SoundManager.Instance.PlayClick();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPointerHoverTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuPointer pointer;
    public bool UpdateOnExit = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(pointer)
        {
            pointer.SetHovering(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(pointer && UpdateOnExit)
        {
            pointer.UnsetHovering(this);
        }
    }
}

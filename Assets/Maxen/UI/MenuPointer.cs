using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuPointer : MonoBehaviour
{
    public EventSystem eventSystem;
    public Image visualComponent;
    [Range(0, 1)] public float lerpRate = 0.2f;
    public bool AlignHorizontal = false;
    public bool AlignVertical = true;

    [SerializeField] private RectTransform rectTransform;

    private MenuPointerHoverTarget hoverTarget;

    private void Update()
    {
        if(!eventSystem)
        {
            return;
        }

        //Determine which object the pointer should be pointing at
        RectTransform pointerTarget = null;
        if (hoverTarget)
        {
            pointerTarget = hoverTarget.GetComponent<RectTransform>();
        }
        else if(eventSystem.currentSelectedGameObject)
        {
            pointerTarget = eventSystem.currentSelectedGameObject.GetComponent<RectTransform>();
        }

        //Make pointer point at its target
        if(pointerTarget)
        {
            visualComponent.enabled = true;

            Vector3 targetPosition = rectTransform.position;
            if(AlignHorizontal)
            {
                targetPosition.x = pointerTarget.position.x;
            }
            if(AlignVertical)
            {
                targetPosition.y = pointerTarget.position.y;
            }
            targetPosition = Vector3.Lerp(rectTransform.position, targetPosition, lerpRate);

            rectTransform.position = targetPosition;
        }
        else
        {
            visualComponent.enabled = false;
        }
    }

    public void SetHovering(MenuPointerHoverTarget target)
    {
        hoverTarget = target;
    }

    public void UnsetHovering(MenuPointerHoverTarget target)
    {
        if(hoverTarget == target)
        {
            hoverTarget = null;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
#endif
}

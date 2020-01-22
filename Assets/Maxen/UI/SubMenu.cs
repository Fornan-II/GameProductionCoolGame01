using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SubMenu : MonoBehaviour
{
    public GameObject FirstSelected;
    public UnityEvent OnBecomeActive;
    public UnityEvent OnBecomeInactive;

    public void SetActive(bool value)
    {
        bool wasActive = gameObject.activeSelf;
        gameObject.SetActive(value);

        if (value && !wasActive)
        {
            if (EventSystem.current && value)
            {
                EventSystem.current.SetSelectedGameObject(FirstSelected);
            }

            OnBecomeActive.Invoke();
        }
        else if(!value && wasActive)
        {
            OnBecomeActive.Invoke();
        }
    }
}

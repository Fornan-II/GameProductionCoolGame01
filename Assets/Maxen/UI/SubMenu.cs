using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SubMenu : MonoBehaviour
{
    public GameObject FirstSelected;
    public UnityEvent OnBecomeActive;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        if (EventSystem.current && value)
        {
            EventSystem.current.SetSelectedGameObject(FirstSelected);
            OnBecomeActive.Invoke();
        }
    }
}

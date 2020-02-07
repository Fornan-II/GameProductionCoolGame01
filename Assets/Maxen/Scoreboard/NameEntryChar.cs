using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NameEntryChar : Selectable
{
    [SerializeField] private NameEntry masterNameEntry = null;
    [SerializeField] private int finalStringIndex = 0;
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float scrollDelay = 0.5f;
    [SerializeField] private TextMeshProUGUI text = null;

    private float timeSinceScrollStart = 0.0f;

    private bool _isSelected = false;

    public override void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        base.OnSelect(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        base.OnDeselect(eventData);
    }

    private void Update()
    {
        if (!_isSelected)
            return;

        float vert = Input.GetAxis("Vertical");

        vert *= sensitivity * Time.deltaTime;

        if (vert == 0.0f)
            timeSinceScrollStart = 0.0f;
        else
        {
            if (timeSinceScrollStart == 0.0f)
            {
                vert = Mathf.Sign(vert);
            }
            else if(timeSinceScrollStart < scrollDelay)
            {
                vert = 0.0f;
            }

            timeSinceScrollStart += Time.deltaTime;
        }

        ScrollChars(vert);
    }

    public void ScrollChars(float scrollAmount)
    {
        if (!masterNameEntry)
            return;

        if (0 > finalStringIndex || finalStringIndex >= masterNameEntry.finalString.Length)
            return;

        masterNameEntry.finalString[finalStringIndex] += scrollAmount + NameEntry.AvailableChars.Length;
        masterNameEntry.finalString[finalStringIndex] %= NameEntry.AvailableChars.Length;

        if(text)
        {
            text.text = masterNameEntry.GetCharAtIndex(finalStringIndex).ToString();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
#endif
}

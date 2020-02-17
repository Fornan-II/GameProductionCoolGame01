using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFunction : MonoBehaviour
{
    public float ButtonActivateDelay = 1.0f;
    public Button[] ButtonsToActivate;
    public NameEntry nameEntry;

    private Coroutine ButtonTimerCoroutine;

    public void OnActivateAnimation()
    {
        //Prep score value for saving
        if (nameEntry)
            nameEntry.score = (int)PlayerScript.Instance.Score;

        transform.localScale = Vector3.one * 2;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 2, "easetype", iTween.EaseType.easeOutElastic));
    }

    public void WaitToActivateButtons()
    {
        if(ButtonTimerCoroutine != null)
        {
            StopCoroutine(ButtonTimerCoroutine);
        }
        ButtonTimerCoroutine = StartCoroutine(ButtonActivateTimer());
    }

    public void DeactivateButtons()
    {
        if (ButtonTimerCoroutine != null)
        {
            StopCoroutine(ButtonTimerCoroutine);
        }

        foreach(Button b in ButtonsToActivate)
        {
            b.interactable = false;
        }
    }

    private IEnumerator ButtonActivateTimer()
    {
        yield return new WaitForSecondsRealtime(ButtonActivateDelay);

        foreach(Button b in ButtonsToActivate)
        {
            b.interactable = true;
        }
    }
}

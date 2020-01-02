using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFunction : MonoBehaviour
{
    public void OnActivateAnimation()
    {
        transform.localScale = Vector3.one * 2;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 2, "easetype", iTween.EaseType.easeOutElastic));
    }
}

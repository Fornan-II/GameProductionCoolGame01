using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntry : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI playerNameText = null;
    [SerializeField] private TextMeshProUGUI playerScoreText = null;

    public void SetValues(ScoreManager.ScoreHolder scoreValue)
    {
        if(playerNameText)
            playerNameText.text = scoreValue.Name;
        if (playerScoreText)
            playerScoreText.text = scoreValue.Score.ToString();
    }

    public void SetVerticalPosition(float verticalPosition)
    {
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, -1 * verticalPosition);
    }

    public float GetHeight()
    {
        return _rectTransform.rect.height;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!_rectTransform)
            _rectTransform = GetComponent<RectTransform>();
    }
#endif
}
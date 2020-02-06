using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private ScoreboardEntry scoreboardEntryPrefab = null;
    [SerializeField] private List<ScoreboardEntry> activeScoreboardEntries = new List<ScoreboardEntry>();
    [SerializeField] private float entrySpacing = 10.0f;

    protected virtual void OnEnable()
    {
        ScoreManager.LoadScore();
        ScoreManager.SortScores();
        DisplayScoreboardEntries();
    }

    public void DisplayScoreboardEntries()
    {
        float entryDelta = scoreboardEntryPrefab.GetHeight() + entrySpacing;

        for (int i = 0; i < ScoreManager.scoreList.Count || i < activeScoreboardEntries.Count; i++)
        {
            if(i < ScoreManager.scoreList.Count)
            {
                if(i < activeScoreboardEntries.Count)
                {
                    activeScoreboardEntries[i].SetValues(ScoreManager.scoreList[i]);
                    activeScoreboardEntries[i].SetVerticalPosition(i * entryDelta);
                }
                else
                {
                    ScoreboardEntry newEntry = Instantiate(scoreboardEntryPrefab, transform);
                    newEntry.SetValues(ScoreManager.scoreList[i]);
                    newEntry.SetVerticalPosition(i * entryDelta);
                    activeScoreboardEntries.Add(newEntry);
                }
            }
            else
            {
                Destroy(activeScoreboardEntries[i].gameObject);
                activeScoreboardEntries.RemoveAt(i);
                i--;
            }
        }

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, entryDelta * activeScoreboardEntries.Count);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!_rectTransform)
            _rectTransform = GetComponent<RectTransform>();
    }
#endif
}

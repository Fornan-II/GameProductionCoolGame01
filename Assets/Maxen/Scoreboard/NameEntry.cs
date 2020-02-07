using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameEntry : MonoBehaviour
{
    public const string AvailableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public float[] finalString = new float[] { 0, 0, 0 };
    public int score;

    public string GetString()
    {
        string result = "";
        for(int i = 0; i < finalString.Length; i++)
        {
            result += GetCharAtIndex(i);
        }
        return result;
    }

    public char GetCharAtIndex(int index)
    {
        return AvailableChars[RoundFloatToInt(finalString[index])];
    }

    public static int RoundFloatToInt(float value)
    {
        return Mathf.FloorToInt(Mathf.Abs(value)) * (int)Mathf.Sign(value);
    }

    public void SaveScore()
    {
        ScoreManager.AddScore(new ScoreManager.ScoreHolder(score, GetString()));
    }
}

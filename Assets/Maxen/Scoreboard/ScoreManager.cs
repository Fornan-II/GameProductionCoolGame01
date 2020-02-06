using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreManager
{
    [System.Serializable]
    public class ScoreHolder
    {
        public int Score = 0;
        public int Version = 0;
        public string Name = "---";

        public ScoreHolder()
        {

        }

        public ScoreHolder(int score, string player)
        {
            Score = score;
            Name = player;
        }
    }


    private static readonly string _scoreFilePath = Application.persistentDataPath + "/score.binary";
    public static List<ScoreHolder> scoreList = new List<ScoreHolder>();

    public static void SaveScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream scoreFile = File.Create(_scoreFilePath);

        formatter.Serialize(scoreFile, scoreList);

        scoreFile.Close();
    }

    public static bool LoadScore()
    {
        if (!File.Exists(_scoreFilePath)) { return false; }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream scoreFile = File.Open(_scoreFilePath, FileMode.Open);

        object deserializedObject = formatter.Deserialize(scoreFile);
        
        if(deserializedObject is List<ScoreHolder>)
        {
            scoreList = (List<ScoreHolder>)deserializedObject;
            scoreFile.Close();
            return true;
        }
        else
        {
            scoreFile.Close();
            return false;
        }
    }

    public static void SortScores()
    {
        //Bubble sort
        bool somethingWasSwapped;
        do
        {
            somethingWasSwapped = false;
            for (int i = 0; i < scoreList.Count - 1; i++)
            {
                if (scoreList[i].Score < scoreList[i + 1].Score)
                {
                    ScoreHolder temp = scoreList[i];
                    scoreList[i] = scoreList[i + 1];
                    scoreList[i + 1] = temp;

                    somethingWasSwapped = true;
                }
            }
        } while (somethingWasSwapped);
    }
}

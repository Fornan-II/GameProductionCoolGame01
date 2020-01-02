using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static void ResumeTime()
    {
        if(Instance)
        {
            Time.timeScale = Instance.runTimeScale;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public static void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    public static void SetRunTimeScale(float scale)
    {
        if(Instance)
        {
            Instance.runTimeScale = scale;
        }

        if (Time.timeScale != 0.0f)
        {
            Time.timeScale = scale;
        }
    }

    private static TimeManager Instance;

    private float runTimeScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
        if(Instance == this)
        {
            Instance = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEMP_Health_UI : MonoBehaviour
{
    private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerScript.Instance)
        {
            if(PlayerScript.Instance.currentHealth <= 0)
            {
                healthText.text = "YOU ARE DEAD IF YOU COULD KINDLY STOP PLAYING";
            }
            else
            {
                healthText.text = PlayerScript.Instance.currentHealth.ToString();
            }
        }
    }
}

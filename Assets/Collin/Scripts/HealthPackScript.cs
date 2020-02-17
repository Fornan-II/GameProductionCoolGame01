using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPackScript : MonoBehaviour
{
    [SerializeField] private int minHealth = 0;
    [SerializeField] private int maxHealth = 0;

    private int healthToAdd = 0;

   [SerializeField] private TextMeshProUGUI bulletText;

    private void Start()
    {
        healthToAdd = Random.Range(minHealth, maxHealth);
        bulletText.text = healthToAdd.ToString();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag.Contains("Player"))
        {
            PlayerScript.Instance.GiveHealth(healthToAdd, transform);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPackScript : MonoBehaviour
{
    [SerializeField] private int minAmmo = 0;
    [SerializeField] private int maxAmmo = 0;

    private int ammoToAdd = 0;

   [SerializeField] private TextMeshProUGUI bulletText;

    private void Start()
    {
        ammoToAdd = Random.Range(minAmmo, maxAmmo);
        bulletText.text = ammoToAdd.ToString();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            PlayerScript.Instance.AddAmmo(ammoToAdd);
            Destroy(gameObject);
        }
    }
}

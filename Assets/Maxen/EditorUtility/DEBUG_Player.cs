using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript player = gameObject.AddComponent<PlayerScript>();
        player.enabled = false;
        PlayerScript.Instance = player;
    }
}

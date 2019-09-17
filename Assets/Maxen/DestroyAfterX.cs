using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterX : MonoBehaviour
{
    public float DestroyDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyDelay);
    }
}

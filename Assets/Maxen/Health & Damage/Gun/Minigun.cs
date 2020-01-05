using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun
{
    public override void Shoot()
    {
        StartCoroutine(ShootXBulletsForYSeconds(3, 0.5f));
    }
}

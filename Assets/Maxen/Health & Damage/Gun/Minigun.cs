using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun
{
    public override void Shoot(IDamageDealer shooter)
    {
        StartCoroutine(ShootXBulletsForYSeconds(3, 0.5f, shooter));
    }
}

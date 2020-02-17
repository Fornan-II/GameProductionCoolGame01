using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    [SerializeField] protected float SlowMotionRange = 10.0f;
    [SerializeField] protected LayerMask SlowMotionLayerMask;
    [SerializeField] [Range(0, 1)] protected float SlowMoSpeed = 0.5f;

    protected bool _slowMoActive = false;

    // Update is called once per frame
    void Update()
    {
        if(_equipped)
        {
            Vector2 boxCenter = transform.position + transform.right * SlowMotionRange * 0.5f;
            Vector2 boxSize = new Vector2(SlowMotionRange, 1.0f);

            //List<Collider2D> foundColliders = new List<Collider2D>();
            Collider2D[] foundColliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, transform.rotation.eulerAngles.z, SlowMotionLayerMask);

            bool targetFound = false;
            for(int i = 0; i < foundColliders.Length && !targetFound; i++)
            {
                if(foundColliders[i].TryGetComponent(out SniperTargetable target))
                {
                    targetFound = target.IsTargetable;
                }
            }

            if (targetFound)
            {
                TimeManager.SetRunTimeScale(SlowMoSpeed);
                _slowMoActive = true;
            }
            else if(_slowMoActive)
            {
                TimeManager.SetRunTimeScale(1.0f);
                _slowMoActive = false;
            }
        }
    }

    private void OnDestroy()
    {
        if(_slowMoActive)
        {
            TimeManager.SetRunTimeScale(1.0f);
        }
    }
}

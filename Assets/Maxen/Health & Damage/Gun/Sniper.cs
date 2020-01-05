using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    [SerializeField] protected float SlowMotionRange = 10.0f;
    [SerializeField] protected LayerMask SlowMotionLayerMask;

    protected bool _slowMoActive = false;

    // Update is called once per frame
    void Update()
    {
        if(_equipped)
        {
            Vector2 boxCenter = transform.position + transform.right * SlowMotionRange * 0.5f;
            Vector2 boxSize = new Vector2(SlowMotionRange, 1.0f);

            //List<Collider2D> foundColliders = new List<Collider2D>();
            Collider2D foundCollider = Physics2D.OverlapBox(boxCenter, boxSize, transform.rotation.eulerAngles.z, SlowMotionLayerMask);
            if (foundCollider)
            {
                TimeManager.SetRunTimeScale(0.5f);
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

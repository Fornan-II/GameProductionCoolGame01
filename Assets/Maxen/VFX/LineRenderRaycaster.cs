using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderRaycaster : MonoBehaviour
{
    [SerializeField]protected LineRenderer _line;
    public LayerMask raycastMask = Physics2D.AllLayers;
    public float maxDistance = 25.0f;
    
    protected virtual void FixedUpdate()
    {
        _line.positionCount = 2;
        Vector3 distantPoint;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, maxDistance, raycastMask);
        if (hitInfo)
        {
            distantPoint = Vector3.right * hitInfo.distance;
        }
        else
        {
            distantPoint = Vector3.right * maxDistance;
        }

        _line.SetPositions(new Vector3[] { Vector3.zero, distantPoint });
    }

    private void OnValidate()
    {
        if(!_line)
        {
            _line = GetComponent<LineRenderer>();
        }
    }
}

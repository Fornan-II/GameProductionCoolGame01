using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Convert3DTo2D : MonoBehaviour
{
    struct ColliderInfo
    {
        public Vector2 size;
        public Vector2 center;
        public bool isTrigger;

        public ColliderInfo(Vector2 s, Vector2 c, bool t)
        {
            size = s;
            center = c;
            isTrigger = t;
        }
    }

    public bool Convert = false;

    private void ConvertColliders()
    {
        BoxCollider[] allBoxColliders = FindObjectsOfType<BoxCollider>();
        Dictionary<GameObject, List<ColliderInfo>> goColInfo = new Dictionary<GameObject, List<ColliderInfo>>();

        foreach(BoxCollider col in allBoxColliders)
        {
            ColliderInfo info = new ColliderInfo(col.size, col.center, col.isTrigger);

            if (goColInfo.ContainsKey(col.gameObject))
            {
                goColInfo[col.gameObject].Add(info);
            }
            else
            {
                goColInfo.Add(col.gameObject, new List<ColliderInfo> { info });
            }

            DestroyImmediate(col);
        }

        foreach(GameObject go in goColInfo.Keys)
        {
            foreach(ColliderInfo info in goColInfo[go])
            {
                BoxCollider2D newCollider = go.AddComponent<BoxCollider2D>();
                newCollider.size = info.size;
                newCollider.offset = info.center;
                newCollider.isTrigger = info.isTrigger;
            }
        }
    }

    private void Update()
    {
        if(Convert)
        {
            ConvertColliders();
            Convert = false;
        }
    }
}

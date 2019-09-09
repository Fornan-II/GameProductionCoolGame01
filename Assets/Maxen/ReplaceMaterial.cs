using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplaceMaterial : MonoBehaviour
{
    public Material oldMaterial;
    public Material replacementMaterial;

    public bool Replace = false;

    // Update is called once per frame
    void Update()
    {
        if(Replace)
        {
            if(oldMaterial && replacementMaterial)
            {
                ReplaceMaterials();
                Replace = false;
            }
        }
    }

    private void ReplaceMaterials()
    {
        MeshRenderer[] allRenderers = FindObjectsOfType<MeshRenderer>();

        foreach(MeshRenderer mr in allRenderers)
        {
            if(mr.sharedMaterial == oldMaterial)
            {
                mr.sharedMaterial = replacementMaterial;
            }
        }
    }
}

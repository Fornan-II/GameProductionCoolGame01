using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public static CameraFX MainCamera { get; private set; }

    [SerializeField] protected Camera _camera;
    public Camera Camera { get { return _camera; } }

    [SerializeField] private float shakeAmount = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MainCamera = this;
    }

    //Shakes the screen (camera)
    public void ScreenShake()
    {
        iTween.ShakePosition(Camera.gameObject, iTween.Hash("amount", Vector3.one * shakeAmount, "time", 0.05f));
    }


#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if(!_camera)
        {
            _camera = GetComponent<Camera>();
        }
    }
#endif
}

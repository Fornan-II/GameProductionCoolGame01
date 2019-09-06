using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Singleton
    public static PlayerScript Instance;
    //Where the bullet will spawn
    [SerializeField] private Transform bulletSpawnPosition;
    //The bullet
    [SerializeField] private GameObject bulletPrefab;
    //Multipliers added to the player for movement/rotation
    [SerializeField] private float knockbackMultiplier = 0;
    [SerializeField] private float torqueMultiplier = 0;
    //The players rigidbody
    private Rigidbody2D playerRig;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerRig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CameraMovement();
        PlayerMovement();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    //Shakes the screen (camera)
    //TODO actually write the fuction
    private void ScreenShake()
    {

    }
    //Shoots the projectile and adds the force/torque to the player
    //TODO clamp the rotation speed somehow, maybe dampen it too
    private void Shoot()
    {
        ScreenShake();
        playerRig.AddForce(-transform.right * knockbackMultiplier);
        playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);      
        if (bulletPrefab && bulletSpawnPosition)
            Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        else
            Debug.LogError("Please set the bullet spawn position or the bullet prefab");
    }
    //Function that'll allow the player to move slightly to the left/right while falling, so that they can try and land somewhere desirable
    //TODO actually write the fuction
    private void PlayerMovement()
    {

    }
    //Keeps the camera locked to the player
    //TODO Make a smooth lerp
    private void CameraMovement()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

}

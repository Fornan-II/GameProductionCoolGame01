using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    //Singleton
    public static PlayerScript Instance;
    //The players rigidbody
    private Rigidbody2D playerRig;

    private bool alive = false;

    //Where the bullet will spawn
    [SerializeField] private Transform bulletSpawnPosition;
    //The bullet
    [SerializeField] private GameObject bulletPrefab;
    //Particle for when the bullet is fired
    [SerializeField] private ParticleSystem bulletFlash;

    //Multipliers added to the player for movement/rotation
    [SerializeField] private float knockbackMultiplier = 0;
    [SerializeField] private float torqueMultiplier = 0;
    [SerializeField] private float maxAngularVelocity = 3.0f;
    private bool switchDirection = false;

    [SerializeField] private GameObject background;
    [SerializeField] private GameObject ground;

    [SerializeField] private float shakeAmount = 1;

    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private int startingBulletAmount = 25;
    private int currentBulletAmount;


    [SerializeField]
    private GameObject playerUI;

    [SerializeField] private TextMeshProUGUI timerText;

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
        currentBulletAmount = startingBulletAmount;
        bulletText.text = currentBulletAmount.ToString();

        StartCoroutine(PlayerInput());
    }

    private IEnumerator PlayerInput()
    {
        int timeTilStart = 5;
        while (timeTilStart != 0)
        {
            timerText.text = timeTilStart.ToString();
            yield return new WaitForSecondsRealtime(1);
            timeTilStart--;
        }
        timerText.text = "";

        playerRig.AddForce(Vector2.up * knockbackMultiplier);
        playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);

        alive = true;

        while (alive)
        {
            //yield return new WaitForFixedUpdate();
            yield return null;
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentBulletAmount > 0)
                    Shoot();
            }
        }
    }

    private void Update()
    {
        CameraMovement();
        BackgroundScroll();

        //Clamp player's angular velocity so the player's rotation stays comprehensible
        if(Mathf.Abs(playerRig.angularVelocity) > maxAngularVelocity)
        {
            if(playerRig.angularVelocity < 0.0f)
            {
                playerRig.angularVelocity = -maxAngularVelocity;
            }
            else
            {
                playerRig.angularVelocity = maxAngularVelocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.transform.tag == "Floor" && alive)
        {
            alive = false;
            //Die, or start death timer
            Stall();
           
        }
    }

    private void BackgroundScroll()
    {
        background.transform.position = new Vector3(transform.position.x, transform.position.y, background.transform.position.z);
        Vector2 offset = background.GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.x = background.transform.position.x / background.transform.localScale.x;
        offset.y = background.transform.position.y / background.transform.localScale.y;
        background.GetComponent<MeshRenderer>().material.mainTextureOffset = offset;

        ground.transform.position = new Vector3(transform.position.x, ground.transform.position.y, ground.transform.position.z);
    }
    //Shakes the screen (camera)
    private void ScreenShake()
    {
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("amount", Vector3.one * shakeAmount, "time", 0.05f));
    }
    //Shoots the projectile and adds the force/torque to the player
    private void Shoot()
    {
        currentBulletAmount--;
        bulletText.text = currentBulletAmount.ToString();
        ScreenShake();
        playerRig.velocity = Vector3.zero;
        playerRig.AddForce(-transform.right * knockbackMultiplier);

        bulletFlash.Play();

        switchDirection = !switchDirection;
        if (switchDirection)
            playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);
        else
            playerRig.AddTorque(-torqueMultiplier, ForceMode2D.Impulse);
        if (bulletPrefab && bulletSpawnPosition)
            Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        else
            Debug.LogError("Please set the bullet spawn position or the bullet prefab");
    }
    //Keeps the camera locked to the player
    private void CameraMovement()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        playerUI.transform.position = transform.position;
    }

    public void AddAmmo(int aAmount)
    {
        currentBulletAmount += aAmount;
        bulletText.text = currentBulletAmount.ToString();
    }

    public void Stall()
    {
        playerRig.velocity = Vector3.zero;
    }
}

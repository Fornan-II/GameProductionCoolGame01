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
    private bool dying = false;

    [SerializeField] private int timeTilStart = 10;

    //Where the bullet will spawn
    [SerializeField] private Transform bulletSpawnPosition;
    //The bullet
    [SerializeField] private GameObject bulletPrefab;
    //Particle for when the bullet is fired
    [SerializeField] private ParticleSystem bulletFlash;

    //Multipliers added to the player for movement/rotation
    [SerializeField] private float knockbackMultiplier = 0;
    [SerializeField] private float torqueMultiplier = 0;
    private bool switchDirection = false;

    [SerializeField] private GameObject background;

    [SerializeField] private float shakeAmount = 1;

    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private int startingBulletAmount = 25;
    private int currentBulletAmount;


    [SerializeField]
    private GameObject playerUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private int timeTilDeath = 5;
    [SerializeField] private int timeTilDeathBuffer = 10;

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

        CountDownToStart(timeTilStart);


    }

    private IEnumerator PlayerInput()
    {
        playerRig.AddForce(Vector2.up * knockbackMultiplier);
        playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);

        alive = true;
        while (alive)
        {
            yield return null;
            playerRig.angularVelocity = Mathf.Clamp(playerRig.angularVelocity, 200, Mathf.Infinity);
           
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentBulletAmount > 0)
                    Shoot();
            }
        }
        Die();
    }

    private void Update()
    {
        CameraMovement();
        BackgroundScroll();
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.transform.tag == "Floor" && alive)
        {
            dying = true;
            StartCoroutine(Grounded());
        }
    }

    private void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.transform.tag == "Floor" && alive)
        {
            dying = false;          
        }
    }
    private void BackgroundScroll()
    {
        background.transform.position = new Vector3(transform.position.x, transform.position.y, background.transform.position.z);
        Vector2 offset = background.GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.x = background.transform.position.x / background.transform.localScale.x;
        offset.y = background.transform.position.y / background.transform.localScale.y;
        background.GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
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

        bulletFlash.Play();

        ScreenShake();

        playerRig.velocity = Vector3.zero;
        playerRig.AddForce(-transform.right * knockbackMultiplier);

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
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
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

    private void CountDownToStart(int aCurrentNumber)
    {
        if (aCurrentNumber == 0)
        {
            timerText.gameObject.transform.localScale = Vector3.zero;
            StartCoroutine(PlayerInput());
            return;
        }
        timerText.text = aCurrentNumber.ToString();
        timerText.gameObject.transform.localScale = Vector3.one * 4;
        iTween.ScaleTo(timerText.gameObject, iTween.Hash("scale", Vector3.one, "time", 1, "easetype", iTween.EaseType.easeOutElastic,
            "oncomplete", "CountDownToStart",
            "oncompleteparams", aCurrentNumber - 1,
            "oncompletetarget", gameObject));
    }
    private void CountDownToDeath(int aCurrentNumber)
    {
        if (!dying)
        {
            timerText.gameObject.transform.localScale = Vector3.zero;
            return;
        }

        if (aCurrentNumber == 0)
        {
            timerText.gameObject.transform.localScale = Vector3.zero;
            alive = false;
            return;
        }

        timerText.text = aCurrentNumber.ToString();
        timerText.gameObject.transform.localScale = Vector3.one * 4;
        iTween.ScaleTo(timerText.gameObject, iTween.Hash("scale", Vector3.one, "time", 1, "easetype", iTween.EaseType.easeOutElastic,
            "oncomplete", "CountDownToDeath",
            "oncompleteparams", aCurrentNumber - 1,
            "oncompletetarget", gameObject));
    }

    private IEnumerator Grounded()
    {
        yield return new WaitForSeconds(timeTilDeathBuffer);
        if (dying)
        {
            CountDownToDeath(timeTilDeath);
        }
    }

    private void Die()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        gameOverText.gameObject.transform.localScale = Vector3.one * 2;
        iTween.ScaleTo(gameOverText.gameObject, iTween.Hash("scale", Vector3.one, "time", 2, "easetype", iTween.EaseType.easeOutElastic));
    }
}

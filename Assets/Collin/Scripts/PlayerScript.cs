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
    [SerializeField] private float initialKnockback = 500.0f;

    [SerializeField] private Transform gunTransform;
    [SerializeField] private Gun currentWeapon;

    [SerializeField] private float torqueMultiplier = 0;
    private bool switchDirection = false;

    [SerializeField] private GameObject background;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private ParticleSystem healthParticle;
    [SerializeField] private ParticleSystem damageParticle;
    [SerializeField] private int maxHealth = 25;
    public int currentHealth
    {
        get;
        private set;
    }

    [SerializeField] private MenuScript menu;
    [SerializeField]
    private GameObject playerUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public float Score { get; private set; }

    [SerializeField] private int timeTilDeath = 5;
    [SerializeField] private int timeTilDeathBuffer = 10;

    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject floor;

    public GameObject restartManager;

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
        currentHealth = maxHealth;


        CountDownToStart(timeTilStart);
        StartCoroutine(GroundCheck());

        //Get the DamageReceiver and hook it's OnTakeDamageEvent to this script so that enemies can hurt the player's health
        DamageReceiver playerDamageReceiver = GetComponent<DamageReceiver>();
        if (playerDamageReceiver)
        {
            playerDamageReceiver.OnTakeDamage += TakeDamage;
        }

        currentWeapon.OnEquip();
    }

    private IEnumerator PlayerInput()
    {
        playerRig.AddForce(Vector2.up * initialKnockback);
        playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);

        alive = true;
        while (alive)
        {
            yield return null;
            playerRig.angularVelocity = Mathf.Clamp(playerRig.angularVelocity, 200, Mathf.Infinity);

            if (Input.GetButtonDown("Fire1") && Time.timeScale > 0.0f)
            {
                Shoot();
            }
        }
        Die();
    }

    private void Update()
    {
        CameraMovement();
        BackgroundScroll();
        ClampTransform();
        ManageScore();
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.transform.tag == "Floor" && alive && !dying)
        {
            dying = true;
        }
    }

    private void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.transform.tag == "Floor" && alive)
        {
            dying = false;
        }
    }

    private void ManageScore()
    {
        if (!scoreText)
            return;

        if (playerRig.velocity.y >= 0)
        {
            scoreText.color = Color.green;
        }
        else
        {
            scoreText.color = Color.red;
        }
        if (alive && transform.position.y > 0)
        {
            if(transform.position.y > Score)
            {
                Score = transform.position.y;
                scoreText.text = "Score: " + (int)Score;
            }
            
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
    
    //Shoots the projectile and adds the force/torque to the player
    private void Shoot()
    {
        playerRig.velocity = Vector3.zero;
        playerRig.AddForce(-transform.right * currentWeapon.ShootKnockback);

        switchDirection = !switchDirection;
        if (switchDirection)
            playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);
        else
            playerRig.AddTorque(-torqueMultiplier, ForceMode2D.Impulse);

        currentWeapon.Shoot();
    }
    //Keeps the camera locked to the player
    private void CameraMovement()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        playerUI.transform.position = transform.position;
    }
    //Clamps the players transform to the area and rotation of what we want
    private void ClampTransform()
    {

        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z));
        if (leftWall && rightWall && floor)
        {
            transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftWall.transform.position.x, rightWall.transform.position.x),
          Mathf.Clamp(transform.position.y, floor.transform.position.y, Mathf.Infinity), 0);
        }
        else
        {
            Debug.LogError("Please assign the walls and the floor.");
        }

    }
    public void AddHealth(int aAmount)
    {
        //Health can not exceed maxHealth
        currentHealth = Mathf.Min(maxHealth, currentHealth + aAmount);
        healthParticle.Play();
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

    private IEnumerator GroundCheck()
    {
        while (true)
        {
            yield return new WaitUntil(() => dying);
            int timer = timeTilDeathBuffer;
            while (timer > 0)
            {
                yield return new WaitForSeconds(1);
                timer--;
                if (!dying)
                    break;
            }
            if (dying)
            {
                CountDownToDeath(timeTilDeath);
            }
            yield return new WaitUntil(() => !dying);
        }

    }

    private void Die()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        menu.AllowPausing = false;
        menu.ChangeMenuTo(2);

        finalScoreText.text = string.Format("Your Final Score Is: {0}", (int)Score);
        //restartManager.SetActive(true);
    }

    private void TakeDamage(DamagePacket damage)
    {
        CameraFX.MainCamera.ScreenShake();
        currentHealth -= damage.DamageAmount;
        healthText.gameObject.transform.localScale = Vector3.one * 3f;
        iTween.ScaleTo(healthText.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "easetype", iTween.EaseType.easeOutElastic));
        damageParticle.Play();
        if (currentHealth <= 0)
        {
            alive = false;
        }
        //Check for if less than 0 and die
    }

    public void SetCurrentWeapon(Gun aGun)
    {
        Destroy(currentWeapon.gameObject);

        currentWeapon = aGun;
        aGun.transform.parent = gunTransform;
        aGun.transform.localPosition = Vector3.zero;
        aGun.transform.localRotation = Quaternion.identity;
        aGun.transform.localScale = Vector3.one;

        aGun.OnEquip();
    }

    
}

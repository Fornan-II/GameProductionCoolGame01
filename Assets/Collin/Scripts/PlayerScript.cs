using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour, IDamageDealer
{
    //Singleton
    public static PlayerScript Instance;
    //The players rigidbody
    private Rigidbody2D playerRig;

    [SerializeField] private int timeTilStart = 10;
    [SerializeField] private float initialKnockback = 500.0f;

    [SerializeField] private Transform gunTransform;
    [SerializeField] private Gun currentWeapon;

    [SerializeField] private float torqueMultiplier = 0;
    private bool switchDirection = false;

    [SerializeField] private GameObject background;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private ParticleSystem damageParticle;
    [SerializeField] private MoveToTarget healthParticle;

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private DamageReceiver playerDamageReceiver;
    public int CurrentHealth { get { return playerDamageReceiver.Health; } }

    [SerializeField] private MenuScript menu;
    [SerializeField]
    private GameObject playerUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public float Score { get; private set; }

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

        CountDownToStart(timeTilStart);
        //StartCoroutine(GroundCheck());

        //Get the DamageReceiver and hook it's OnTakeDamageEvent to this script so that enemies can hurt the player's health
        if (playerDamageReceiver)
        {
            playerDamageReceiver.OnTakeDamage += OnTakeDamage;
            playerDamageReceiver.OnDeath += Die;
        }

        currentWeapon.OnEquip();
    }

    private IEnumerator PlayerInput()
    {
        playerRig.AddForce(Vector2.up * initialKnockback);
        playerRig.AddTorque(torqueMultiplier, ForceMode2D.Impulse);

        while (playerDamageReceiver.IsAlive)
        {
            yield return null;
            playerRig.angularVelocity = Mathf.Clamp(playerRig.angularVelocity, 200, Mathf.Infinity);

            if (Input.GetButtonDown("Fire1") && Time.timeScale > 0.0f)
            {
                Shoot();
            }
        }
    }

    private void Update()
    {
        CameraMovement();
        BackgroundScroll();
        ClampTransform();
        ManageScore();
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
        if (playerDamageReceiver.IsAlive && transform.position.y > 0)
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

        currentWeapon.Shoot(this);
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

    private void Die(DamageReceiver reciever, IDamageDealer killer)
    {
        //playerDamageReceiver.DoDamageFlash = false;
        //playerDamageReceiver.ForceStopDamageFlash();
        //playerDamageReceiver.UnitSpriteRenderer.color = Color.red;

        menu.AllowPausing = false;
        menu.ChangeMenuTo(2);

        finalScoreText.text = string.Format("Your Final Score Is: {0}", (int)Score);
        //restartManager.SetActive(true);
    }

    private void OnTakeDamage(DamagePacket damage)
    {
        CameraFX.MainCamera.ScreenShake();
        healthText.gameObject.transform.localScale = Vector3.one * 3f;
        iTween.ScaleTo(healthText.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "easetype", iTween.EaseType.easeOutElastic));
        damageParticle.Play();
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

    public void OnDamageDealtTo(DamageReceiver dr)
    {
        //Unused, required to satisify IDamageDealer
    }

    public void OnKilled(DamageReceiver dr)
    {
        if(dr.TryGetComponent(out AIUnit aiVictim))
        {
            GiveHealth(aiVictim.DeathHealthReward, aiVictim.transform);
        }
    }

    public void GiveHealth(int amount, Transform source)
    {
        if(playerDamageReceiver.IsAlive)
        {
            playerDamageReceiver.SetHealth(Mathf.Min(playerDamageReceiver.Health + amount, maxHealth));
        }

        Instantiate(healthParticle, source.position, source.transform.rotation).DoMovement(transform, true, false);
    }
}

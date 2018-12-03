using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public bool canJump = true;
    public bool canReJump = true;
    float jumpDelay = 0;
    float jumpForce = 600;
    ContactDetector contactDetector;
    public float shieldAmount = 1;
    public bool canUseShield = true;
    public bool shieldActivated = false;
    public bool isBot = false;
    public GameObject shieldSprite;
    public Image shieldGauge;
    public Image[] hitStars;
    public Sprite[] playerSkins;
    public SpriteRenderer playerSkin;
    float hAxis, vAxis;
    public float hits = 3;
    bool btHit = false;
    public bool isTurnedLeft = false;
    public float lastHit = 0;
    float safeHitDelay = 0.3f;
    public GameObject dieParticle;

    public WeaponAnimation[] weapons;
    public int weaponId = 0;
    private Rewired.Player player; // The Rewired Player
    public int playerId;

    public bool CanJump
    {
        get
        {
            if (!canJump || jumpDelay < 0.05f)
                return false;
            return true;
        }
    }

    public bool isInTheAir
    {
        get
        {
            return contactDetector.isInTheAir;
        }
    }

    void Awake()
    {
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        contactDetector = transform.GetComponentInChildren<ContactDetector>();
        player = ReInput.players.GetPlayer(playerId);
        playerSkin.sprite = playerSkins[playerId];
        Debug.Log(player.name);
    }

    public bool sameSign(float val1, float val2)
    {
        if ((val1 >= 0 && val2 >= 0) || (val1 <= 0 && val2 <= 0))
            return true;
        return false;
    }

    void Update () {
        if (!BrawlCore.Instance.isRunning)
            return;
        hAxis = (lastHit > safeHitDelay ? player.GetAxis("hAxis") : 0);
        vAxis = player.GetAxis("vAxis");
        bool btJump = player.GetButtonDown("jump");
        bool btShield = player.GetButton("shield");
        physics();
        jumpDelay += Time.deltaTime;
        if (btJump && CanJump && !contactDetector.isInTheAir)
        {
            jump();
        }
        else if (btJump && canReJump)
            reJump();
        if (!isBot && !weapons[weaponId].isActive)
            useShield(btShield);
        handleShield();
        handleHit();
        contactDetector.collider.enabled = (rb.velocity.y > 0 ? false : true);
        if (hAxis < -0.1f)
        {
            isTurnedLeft = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (hAxis > 0.1f)
        {
            isTurnedLeft = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.y < -10)
        {
            var ps = GameObject.Instantiate(dieParticle);
            ps.transform.position = transform.position;
            ps.SetActive(true);
            transform.position = Vector2.zero;
        }
        lastHit += Time.deltaTime;
    }

    void physics()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.3f && !isInTheAir)
            rb.velocity = new Vector2(0, rb.velocity.y);
        float drag = 1f - Time.deltaTime * (isInTheAir ? 2f : 50f);
        float targetVelocity = rb.velocity.x * drag;
        if (!sameSign(rb.velocity.x, hAxis) ||
            (sameSign(rb.velocity.x, hAxis) && Mathf.Abs(rb.velocity.x) < Mathf.Abs(hAxis * 10)))
            targetVelocity = hAxis * 10;
        var velocity = Mathf.Lerp(rb.velocity.x, targetVelocity, 5f * Time.deltaTime);
        rb.velocity = new Vector2(velocity, rb.velocity.y - Time.deltaTime * 25f);
    }

    public void useShield(bool use = false)
    {
        if (canUseShield)
        {
            shieldActivated = use;
        }
    }

    public void handleShield()
    {
        if (!canUseShield && shieldAmount >= 1)
            canUseShield = true;
        if (shieldAmount <= 0)
        {
            shieldAmount = 0;
            canUseShield = false;
            shieldActivated = false;
        }
        if (shieldActivated)
            shieldAmount -= Time.deltaTime;
        else
            shieldAmount += Time.deltaTime;
        if (shieldAmount > 1)
            shieldAmount = 1;
        if (shieldActivated)
        {
            shieldSprite.SetActive(true);
        }
        else
            shieldSprite.SetActive(false);
        shieldGauge.fillAmount = shieldAmount;
        float shieldDisplayed = (shieldAmount >= 1 ? 0 : 0.35f);
        if (canUseShield)
            shieldGauge.color = new Color(0.35f, 1, 0.35f, shieldDisplayed);
        else
            shieldGauge.color = new Color(1, 0.35f, 0.35f, shieldDisplayed);
    }

    public void handleHit()
    {
        btHit = player.GetButtonDown("hit");
        if (btHit)
            hit();
        hits += Time.deltaTime * 1.5f;
        if (hits > 3)
            hits = 3;
        for (int i = 0; i < 3; i++)
        {
            hitStars[i].enabled = (hits - 0.99f > i);
        }
    }

    public void hit()
    {
        if (!shieldActivated && hits > 1)
        {
            if (weapons[weaponId].StartHit())
            {
                hits -= 1;
            }
        }
    }

    public void jump()
    {
        jumpDelay = 0;
        rb.AddForce(Vector2.up * jumpForce);
        canJump = false;
        canReJump = true;
    }

    public void reJump()
    {
        jumpDelay = 0;
        if (rb.velocity.y < 0)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce);
        canReJump = false;
    }

    public void hasTouchedDown()
    {
        canJump = true;
    }

    internal void getRekt(Vector2 hitPosition)
    {
        if (lastHit < safeHitDelay)
            return;
        float hitforce = (shieldActivated ? 600 : 1600);
        var vector = new Vector2(hitPosition.x < transform.position.x ? 1 : -1, 0.35f) * hitforce;
        rb.AddForce(vector);
        lastHit = 0;
    }
}

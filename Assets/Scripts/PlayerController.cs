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
    public float hAxis, vAxis;
    public float hits = 3;
    public bool btHit = false;
    public bool btJump = false;
    public bool btShield = false;
    public bool isTurnedLeft = false;
    public int damage = 0;
    public float lastHit = 0;
    float stunDelay = 0.3f;
    public GameObject dieParticle;

    public WeaponAnimation[] weapons;
    public int weaponId = 0;
    public  Rewired.Player player; // The Rewired Player
    public int playerId;
    public  int lives;

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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        contactDetector = transform.GetComponentInChildren<ContactDetector>();
        player = ReInput.players.GetPlayer(playerId);
        playerSkin.sprite = playerSkins[playerId];
        lives = 3;
        Debug.Log(player.name);
    }

    public bool sameSign(float val1, float val2)
    {
        if ((val1 >= 0 && val2 >= 0) || (val1 <= 0 && val2 <= 0))
            return true;
        return false;
    }

    void Update () {
        updateInfoCard();
        if (!isBot && BrawlCore.Instance.isRunning)
        {
            hAxis = player.GetAxis("hAxis");
            vAxis = player.GetAxis("vAxis");
            btJump = player.GetButtonDown("jump");
            btShield = player.GetButton("shield");
            btHit = player.GetButtonDown("hit");
        }
        if (lastHit < stunDelay)
            hAxis = 0;
        physics();
        jumpDelay += Time.deltaTime;
        if (btJump)
            jump();
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
            lives--;
            if (lives == -1)
                BrawlCore.Instance.stopGame();
            damage = 0;
        }
        lastHit += Time.deltaTime;
    }

    void updateInfoCard()
    {
        BrawlCore.Instance.InfoCards[playerId].skin.sprite = playerSkins[playerId];
        for (int i = 0; i < 4; i++)
            BrawlCore.Instance.InfoCards[playerId].weapons[i].gameObject.SetActive(false);
        BrawlCore.Instance.InfoCards[playerId].weapons[weaponId].gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
            BrawlCore.Instance.InfoCards[playerId].lives[i].gameObject.SetActive(lives > i ? true : false);
        BrawlCore.Instance.InfoCards[playerId].playerDamage.text = damage.ToString();
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
        if (btHit && lastHit > stunDelay)
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
                SoundManager.Instance.playClip(0);
                hits -= 1;
            }
        }
    }

    public void jump()
    {
        if (CanJump && !contactDetector.isInTheAir)
        {
            SoundManager.Instance.playClip(1);
            jumpDelay = 0;
            rb.AddForce(Vector2.up * jumpForce);
            canJump = false;
            canReJump = true;
        }
        else if (canReJump)
            reJump();
    }

    public void reJump()
    {
        SoundManager.Instance.playClip(1);
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

    internal void getRekt(Vector2 hitPosition, float powerMultiplier)
    {
        if (lastHit < stunDelay)
            return;
        SoundManager.Instance.playClip(shieldActivated ? 2 : 3);
        float hitforce = (shieldActivated ? 100 + 5 * damage : 400 + 10 * damage ) * powerMultiplier;
        if (!shieldActivated)
            damage += 10;
        var vector = new Vector2(hitPosition.x < transform.position.x ? 1 : -1, 0.35f) * hitforce;
        rb.AddForce(vector);
        lastHit = 0;
    }
}

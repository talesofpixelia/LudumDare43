using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public bool canJump = true;
    public bool canReJump = true;
    public float jumpDelay = 0;
    ContactDetector contactDetector;

    bool CanJump
    {
        get
        {
            if (!canJump || jumpDelay < 0.05f)
                return false;
            return true;
        }
    }

    bool CanReJump
    {
        get
        {
            if (!canJump && canReJump)
                return true;
            return false;
        }
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        contactDetector = transform.GetComponentInChildren<ContactDetector>();
	}
	
	void Update () {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        float targetVelocity = hAxis * 10;
        var velocity = Mathf.Lerp(rb.velocity.x, targetVelocity, 5f * Time.deltaTime);
        rb.velocity = new Vector2(velocity, rb.velocity.y - Time.deltaTime * 25f);
        jumpDelay += Time.deltaTime;

        if (Input.GetButtonDown("Action1") && CanJump && !contactDetector.isInAir)
            jump();
        else if (Input.GetButtonDown("Action1") && canReJump)
            reJump();
        contactDetector.collider.enabled = (rb.velocity.y > 0 ? false : true);
    }

    public void jump()
    {
        jumpDelay = 0;
        rb.AddForce(Vector2.up * 800f);
        canJump = false;
        canReJump = true;
    }

    public void reJump()
    {
        jumpDelay = 0;
        if (rb.velocity.y < 0)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 800f);
        canReJump = false;
    }

    public void hasTouchedDown()
    {
        canJump = true;
    }
}

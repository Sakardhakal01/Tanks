using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : Vehicle
{
    public Vehicle player;
    public Sprite Explosion;
    private bool ShouldExplode;

    // Start is called before the first frame update
    void Start()
    {
        facing = Facing.LEFT;
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = 40.0f;
        moveForce = 0.03f;
        Moving = 0;
        Enabled = false;
        CoolDown = 0.2f;
        ShouldExplode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enabled)
        {
            if (player.transform.position.x > transform.position.x)
                Moving = 1;
            else if (player.transform.position.x < transform.position.x)
                Moving = -1;

            if (Health <= 0)
            {
                spriteRenderer.sprite = Explosion;
                timeStamp = Time.time + CoolDown;
                ShouldExplode = true;
                Enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(Enabled)
        {
            var displacement = Vector3.right * moveForce * Moving;
            transform.position += displacement;
        }
            

        if (timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(gameObject);
            Enabled = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PlayerTank")
        {
            spriteRenderer.sprite = Explosion;
            timeStamp = Time.time + CoolDown;
            ShouldExplode = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "CannonBall(Clone)")
        {
            Health -= 20;
        }

        else if (collision.gameObject.name.Equals("Elevator")) { this.transform.parent = collision.transform; }
        else if (collision.gameObject.name.Contains("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnBecameVisible()
    {
        Enabled = true;
    }

    private void OnBecameInvisible()
    {
        Enabled = false;
    }
}

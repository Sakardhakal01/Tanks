using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Vehicle
{
    public Vehicle player;
    private bool ShouldExplode;
    public Sprite Explosion;
    private float timeOut;

    // Start is called before the first frame update
    void Start()
    {
        facing = Facing.LEFT;
        rigidBody2D = GetComponent<Rigidbody2D>();
        Health = 100.0f;
        moveForce = 0.05f;
        Moving = 0;
        CoolDown = 2.0f;
        Enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Enabled)
        {
            var d = 4 * Mathf.Cos(Mathf.PI / 4) * (4 * Mathf.Sin(Mathf.PI / 4) + Mathf.Sqrt(Mathf.Pow(4.0f * Mathf.Sin(Mathf.PI), 2)) + 2 * Physics2D.gravity.y * 0.51) / Physics2D.gravity.y;
            var playerDist = Mathf.Abs(player.transform.position.x - transform.position.x);

            if (playerDist - d <= 0.5f)
            {
                Moving = 1;
            }
            else if (playerDist - d >= 1.0f)
            {
                Moving = -1;
            }
            else
                Moving = 0;

            if (Health <= 0)
            {
                spriteRenderer.sprite = Explosion;
                timeStamp = Time.time + timeOut;
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
        

        /*if (facing == Facing.LEFT)
        {
            spriteRenderer.flipX = true;
        }
        else if (facing == Facing.RIGHT)
        {
            spriteRenderer.flipX = false;
        }*/

        if (timeStamp <= Time.time && Enabled)
        {
            timeStamp = Time.time + CoolDown;
            var pos = transform.position;
            pos.y = pos.y + 0.5f;
            var p = Instantiate(projectile, pos, transform.rotation);
            var s = p.GetComponent<CannonBall>();
            s.vehicle = this;
        }

        if(timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "CannonBall(Clone)")
        {
            Health -= 20;
        }

        if (collision.name == "Bomb(Clone)")
        {
            Health -= 60;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Elevator")) { this.transform.parent = collision.transform; }
        else if (collision.gameObject.name.Contains("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }
}

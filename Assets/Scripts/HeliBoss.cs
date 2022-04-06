using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliBoss : Vehicle
{
    public Sprite Explosion;
    private bool ShouldExplode;
    public Vehicle player;
    public Projectile cb;

    private float wait;
    private float timeOut;
    private float cbCooldown;
    private float expTime;
    // Start is called before the first frame update
    void Start()
    {
        facing = Facing.LEFT;
        Health = 200.0f;
        CoolDown = 3f;
        Enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveForce = 0.03f;
        cbCooldown = 1.2f;
        expTime = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (Health <= 0)
            {
                spriteRenderer.sprite = Explosion;
                timeStamp = Time.time + expTime;
                ShouldExplode = true;
                Enabled = false;
            }

            if (facing == Facing.LEFT)
                Moving = -1;
            else
                Moving = 1;

            var playerDist = Mathf.Abs(player.transform.position.x - transform.position.x);

            if (playerDist <= 1 && timeStamp <= Time.time)
                Fire = true;
        }

        
    }

    private void FixedUpdate()
    {
        if (Enabled)
        {
            var displacement = Vector3.right * moveForce * Moving;
            transform.position += displacement;
        }


        if (timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(gameObject);
        }


        if (Fire && timeStamp <= Time.time)
        {
            timeStamp = Time.time + CoolDown;
            Fire = false;
            var pos = transform.position;
            pos.y = pos.y - 0.7f;
            var p = Instantiate(projectile, pos, transform.rotation);
        }

        if (timeOut <= Time.time && Enabled)
        {
            timeOut = Time.time + cbCooldown;
            var pos = transform.position;
            pos.x = pos.x + (1f * Moving);
            var p = Instantiate(cb, pos, transform.rotation);
            var s = p.GetComponent<CannonBall>();
            s.vehicle = this;
        }
    }

    private void OnBecameInvisible()
    {
        if (facing == Facing.LEFT && player.transform.position.x > transform.position.x)
        {
            facing = Facing.RIGHT;
            spriteRenderer.flipX = true;
        }

        else if(facing == Facing.RIGHT && player.transform.position.x < transform.position.x)
        {
            facing = Facing.LEFT;
            spriteRenderer.flipX = false;
        }

        
    }

    private void OnBecameVisible()
    {
        Enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("CannonBall(Clone)"))
            Health -= 20;
    }
}

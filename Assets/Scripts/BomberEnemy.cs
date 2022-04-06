using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Vehicle
{
    public Vehicle player;
    public Sprite Explosion;
    private bool ShouldExplode;
    private float timeOut;
    private float wait;

    // Start is called before the first frame update
    void Start()
    {
        facing = Facing.LEFT;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = 20.0f;
        moveForce = 0.07f;
        Moving = 0;
        Enabled = true;
        CoolDown = 0.2f;
        ShouldExplode = false;
        timeOut = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enabled)
        {
            if (facing == Facing.LEFT)
                Moving = -1;
            else
                Moving = 1;

            var playerDist = Mathf.Abs(player.transform.position.x - transform.position.x);

            if (playerDist <= 1 && wait <= Time.time)
                Fire = true;

            if (Health <= 0)
            {
                spriteRenderer.sprite = Explosion;
                timeStamp = Time.time + CoolDown;
                ShouldExplode = true;
                Enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "CannonBall(Clone)")
        {
            Health -= 20;
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
        }


        if (Fire && timeStamp <= Time.time)
        {
            timeStamp = Time.time + CoolDown;
            Fire = false;
            var pos = transform.position;
            pos.y = pos.y - 0.7f;
            var p = Instantiate(projectile, pos, transform.rotation);
            wait = Time.time + timeOut;
        }
    }

    private void OnBecameVisible()
    {
        
    }

    private void OnBecameInvisible()
    {
        if (facing == Facing.LEFT)
        {
            facing = Facing.RIGHT;
            spriteRenderer.flipX = true;
        }
            
        else
        {
            facing = Facing.LEFT;
            spriteRenderer.flipX = false;
        }
            
    }
}

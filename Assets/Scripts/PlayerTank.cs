using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTank : Vehicle
{
    private bool flipped;
    
    // Start is called before the first frame update
    void Start()
    {

        facing = Facing.RIGHT;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        Health = 200.0f;
        moveForce = 0.05f;
        Moving = 0;
        Fire = false;
        CoolDown = 1.0f;
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Moving = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Moving = -1;
        }
        else
            Moving = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            facing = Facing.LEFT;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            facing = Facing.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Fire = true;

        if(Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            flipped = !flipped;
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
            
    }

    private void FixedUpdate()
    {
        //var move = Vector3.right * moveForce * Moving;
        //rigidBody2D.AddForce(move);

        var displacement = Vector3.right * moveForce * Moving;
        transform.position += displacement;

        if (flipped)
        {
            if (facing == Facing.LEFT)
            {
                spriteRenderer.flipX = false;
            }
            else if (facing == Facing.RIGHT)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (facing == Facing.LEFT)
            {
                spriteRenderer.flipX = true;
            }
            else if (facing == Facing.RIGHT)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Fire && timeStamp <= Time.time)
        {
            timeStamp = Time.time + CoolDown;
            Fire = false;
            var pos = transform.position;
            pos.y = pos.y + 0.5f;
            var p = Instantiate(projectile, pos, transform.rotation);
            var s = p.GetComponent<CannonBall>();
            s.vehicle = this;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("SuicideEnemy"))
            Health -= 30;
        if (collision.gameObject.name.Equals("Elevator")) { this.transform.parent = collision.transform; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name== "CannonBall(Clone)")
        {
            Health -= 20;
        }

        if (collision.name == "Bomb(Clone)")
        {
            Health -= 60;
        }

        if (collision.name == "Missle(Clone)")
        {
            Health -= 40;
        }
    }
}

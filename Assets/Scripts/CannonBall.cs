using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        startForce = 200f;
        rb2d = GetComponent<Rigidbody2D>();
        strt = true;
    }

    // Update is called once per frame
    void Update()
    {
        facing = vehicle.facing;
    }

    private void FixedUpdate()
    {
        if(facing == Vehicle.Facing.RIGHT && strt)
        {
            var force = startForce * Vector3.right;
            force = force + (200f * Vector3.up);
            rb2d.AddForce(force);
            strt = false;
        }

        else if (facing == Vehicle.Facing.LEFT && strt)
        {
            var force = startForce * Vector3.left;
            force = force + (200f * Vector3.up);
            rb2d.AddForce(force);
            strt = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.name == "PlayerTank"))
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.name == "PlayerTank"))
            Destroy(this.gameObject);
    }
}

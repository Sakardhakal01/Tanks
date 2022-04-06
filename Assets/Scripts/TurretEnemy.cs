using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Vehicle
{
    public Sprite Explosion;
    private bool ShouldExplode;
    private float timeOut;
    // Start is called before the first frame update
    void Start()
    {
        facing = Facing.LEFT;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = 80.0f;
        Enabled = false;
        CoolDown = 1f;
        timeOut = 0.2f;
        ShouldExplode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
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
        if (timeStamp <= Time.time && Enabled)
        {
            timeStamp = Time.time + CoolDown;
            var pos = transform.position;
            pos.y = pos.y + 0.5f;
            var p = Instantiate(projectile, pos, transform.rotation);
            var s = p.GetComponent<CannonBall>();
            s.vehicle = this;
        }

        if (timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(gameObject);
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
}

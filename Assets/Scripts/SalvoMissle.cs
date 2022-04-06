using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvoMissle : Projectile
{
    private bool isUp;
    public Sprite Explosion;
    private SpriteRenderer spriteRenderer;
    private float cooldown;
    private float timeStamp;
    private bool ShouldExplode;

    // Start is called before the first frame update
    void Start()
    {
        startForce = 200f;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isUp = true;
        cooldown = 0.2f;
        ShouldExplode = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isUp)
        {
            rb2d.AddForce(1000f*Vector3.up);
            isUp = false;
        }
        else
        {
            rb2d.AddForce(-0.2f*Vector3.up);
        }

        if (timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(gameObject);
        }

        if(transform.position.y > 4)
        {
            
            var pos = new Vector3(vehicle.transform.position.x, transform.position.y);
            transform.position = pos;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipY = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.name.Contains("SalvoBoss"))
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Explosion;
            timeStamp = Time.time + cooldown;
            ShouldExplode = true;
        }
    }
}

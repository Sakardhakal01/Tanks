using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    public Sprite Explosion;
    private SpriteRenderer spriteRenderer;
    private float cooldown;
    private float timeStamp;
    private bool ShouldExplode;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cooldown = 0.08f;
        ShouldExplode = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (timeStamp <= Time.time && ShouldExplode)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.name.Contains("HeliBoss"))
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Explosion;
            timeStamp = Time.time + cooldown;
            ShouldExplode = true;
        }
    }
}

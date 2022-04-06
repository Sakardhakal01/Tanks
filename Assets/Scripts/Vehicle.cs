using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Health;
    public float moveForce;
    public enum Facing { LEFT , RIGHT };
    public GameObject projectile;

    [HideInInspector]
    public Facing facing;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidBody2D;
    protected int Moving;
    protected bool Fire;
    protected float CoolDown;
    protected float timeStamp;
    protected bool Enabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Elevator")) { this.transform.parent = null; }
        else if (col.gameObject.name.Contains("Platform")) { this.transform.parent = null; }
    }
}

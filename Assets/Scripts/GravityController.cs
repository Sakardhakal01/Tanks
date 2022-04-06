using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityController : MonoBehaviour
{
    /*private const int DOWN = 0;
    private const int RIGHT = 1;
    private const int UP = 2;
    private const int LEFT = 3;*/
    // public int currentDir;
    //public const float MAX_GRAV = 2.0f;

    private Vector2 gravity;
    [SerializeField]
    private float gravStrength;

    // Start is called before the first frame update
    void Start()
    {
        // currentDir = DOWN;
        gravity = Physics2D.gravity;
        gravStrength = 1.0f;
    }

    /*public int getDirection()
    {
        return currentDir;
    }*/

    public float getGravStrength()
    {
        return gravStrength;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gravStrength += 0.2f;
            if (gravStrength > 2.0f) { gravStrength = 2.0f; }
            gravity.y = -9.80f * gravStrength;
            Physics2D.gravity = gravity;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gravStrength -= 0.2f;
            if (gravStrength < 0.0f) { gravStrength = 0.0f; }
            gravity.y = -9.80f * gravStrength;
            Physics2D.gravity = gravity;
        }

        /*float temp = Input.mouseScrollDelta.y * 0.1f;
        gravStrength += temp;
        if (gravStrength < 0.1f) { gravStrength = 0.1f; }
        else if (gravStrength > 2.0f) { gravStrength = 2.0f; }*/

        // Old code for gravity direction
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            switch (currentDir)
            {
                case DOWN:
                    currentDir = RIGHT;
                    gravity.x = 9.8f;
                    gravity.y = 0;
                    break;
                case RIGHT:
                    currentDir = UP;
                    gravity.x = 0;
                    gravity.y = 9.8f;
                    break;
                case UP:
                    currentDir = LEFT;
                    gravity.x = -9.8f;
                    gravity.y = 0;
                    break;
                case LEFT:
                    currentDir = DOWN;
                    gravity.x = 0;
                    gravity.y = -9.8f;
                    break;
            }
            Physics2D.gravity = gravity;
        }*/
    }

    /* private void FixedUpdate()
     {
     
     }*/
}

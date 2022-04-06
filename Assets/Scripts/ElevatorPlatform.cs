using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    [SerializeField]
    private float velocity;
    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    private float platformTimer = 0.0f;
    public float pauseDuration = 2.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platformTimer >= pauseDuration)
        {
            Move();
        }
        else
        {
            platformTimer += Time.deltaTime;
        }
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, velocity * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
            platformTimer = 0.0f;
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }
}

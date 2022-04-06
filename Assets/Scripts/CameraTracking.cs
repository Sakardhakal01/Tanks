using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject tank;

    [SerializeField]
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - tank.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newXPosition = tank.transform.position.x + offset.x;
        float newYPosition = tank.transform.position.y + offset.y;

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);
    }
}

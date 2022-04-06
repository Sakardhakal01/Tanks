using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OOBTrigger : MonoBehaviour
{
    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = gameObject.scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("PlayerTank"))
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (other.gameObject.name.Contains("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}

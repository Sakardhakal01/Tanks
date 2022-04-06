using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image fillImage;

    public Vehicle tank;

    private float percentage;

    private void Awake()
    {
        fillImage = transform.Find("Health_Fill").GetComponent<Image>();
        fillImage.fillAmount = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percentage = (float)tank.Health / 200;
        percentage = Mathf.Max(0, percentage);
        percentage = Mathf.Min(1, percentage);
        fillImage.fillAmount = percentage;
    }
}

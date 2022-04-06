using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBarController : MonoBehaviour
{
    private Image fillImage;

    public GravityController gravity;
    //public RectTransform bar;
    private float percentage;
    //private float startingWidth;
    //private float targetRightX;

    private void Awake()
    {
        fillImage = transform.Find("StrengthFill").GetComponent<Image>();
        fillImage.fillAmount = 0.5f;
    }
    // Start is called before the first frame update
    /*void Start()
    {
        startingWidth = bar.sizeDelta.x;
        targetRightX = bar.transform.localPosition.x - (startingWidth / 2.0f);
    }*/

    // Update is called once per frame
    void Update()
    {
        float temp = gravity.getGravStrength();
        temp *= 50.0f;
        percentage = (float)temp / 100;
        percentage = Mathf.Max(0, percentage);
        percentage = Mathf.Min(1, percentage);
        UpdateBar();
    }

    private void UpdateBar()
    {
        // calculate new height
        /* var newWidth = startingWidth * percentage;
         var newHeight = bar.sizeDelta.y;
         bar.sizeDelta = new Vector2(newWidth, newHeight);*/

        // calculate new Y position
        /* var newY = bar.localPosition.y;
         var newX = targetRightX + (newWidth / 2.0f);
         var newZ = bar.localPosition.z;*/

        // set new position
        // bar.localPosition = new Vector3(newX, newY, newZ);

        fillImage.fillAmount = percentage;
    }
}

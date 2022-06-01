using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorLerp : MonoBehaviour
{

    Image image;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        colorIndex = (int)Random.Range(0f, 9f);
    }

    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(image.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= myColors.Length) ? 0 : colorIndex;
        }
    }
}

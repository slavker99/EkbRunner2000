using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackBackground : MonoBehaviour
{
    UnityEngine.UI.Image imageScr;
    // Start is called before the first frame update
    void Start()
    {
        imageScr = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public IEnumerator ChangeTransparentCorutine(int startValue, int endValue, bool disabledInEnd = false, float speed = 0.01f)
    {
        gameObject.SetActive(true);
        imageScr = gameObject.GetComponent<UnityEngine.UI.Image>();
        float val = 0f;
        imageScr.color = new Color(imageScr.color.r, imageScr.color.g, imageScr.color.b, startValue);
        while (imageScr.color.a != endValue)
        {
            imageScr.color = new Color(imageScr.color.r, imageScr.color.g, imageScr.color.b, Mathf.Lerp(startValue, endValue, val));
            val = val + speed;
            yield return new WaitForSeconds(0.005f);
        }
        if (disabledInEnd) 
            gameObject.SetActive(false);
        yield return null;

    }
}

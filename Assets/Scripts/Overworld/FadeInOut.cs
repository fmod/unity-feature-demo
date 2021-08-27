using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Color FadeClear;
    public Color FadeBlack;
    public float fadeTo;
    public float fadeTime;

    public Image image;

	void Start ()
    {
        image.color = FadeBlack;
        fadeTo = FadeClear.a;
	}
	
	void Update ()
    {
        if (image.color.a != fadeTo)
            Fade(fadeTo);
	}

    public void FadeIn()
    {
        fadeTo = FadeClear.a;
    }

    public void FadeOut()
    {
        fadeTime = 0.05f;
        fadeTo = FadeBlack.a;
    }

    void Fade(float to)
    {
        float a = Mathf.Lerp(image.color.a, to, fadeTime);
        if (Mathf.Abs(image.color.a - fadeTo) > 0.05)
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        else
            image.color = new Color(image.color.r, image.color.g, image.color.b, to);
    }
}

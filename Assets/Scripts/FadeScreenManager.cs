using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenManager : MonoBehaviour
{
    [SerializeField]
    private Image image;

    bool fadeIn = true;
    bool stateIn = false;
    float velFade = 1f;

    public static FadeScreenManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (fadeIn != stateIn)
        {
            if (fadeIn)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (velFade * Time.deltaTime));
                if (image.color.a >= 1)
                    stateIn = fadeIn;
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (velFade * Time.deltaTime));
                if (image.color.a <= 0)
                    stateIn = fadeIn;
            }
        }
    }

    public void SetFadeInOrOut(bool _fadeIn)
    {
        fadeIn = _fadeIn;
    }
}

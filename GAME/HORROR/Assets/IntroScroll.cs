using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroScroll : MonoBehaviour
{
    public float scrollSpeed = 30f;  // szybkoœæ przesuwania (piksele/sekundê)
    public RectTransform container;  // odniesienie do ScrollContainer

    private RectTransform rectTransform;
    private float startY;
    private float endY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Pocz¹tek: pod spodem kontenera
        startY = -rectTransform.rect.height;
        // Koniec: nad górn¹ krawêdzi¹ kontenera
        endY = container.rect.height + rectTransform.rect.height;

        // Ustaw pozycjê pocz¹tkow¹
        rectTransform.anchoredPosition = new Vector2(0, startY);
    }

    void Update()
    {
        // Przesuwaj tekst w górê
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Jeœli tekst wyszed³ poza ekran – mo¿esz zakoñczyæ intro
        if (rectTransform.anchoredPosition.y >= endY)
        {
            // np. przejœcie do innej sceny
            // SceneManager.LoadScene("MainMenu");
            Debug.Log("Intro finished");
        }
    }
}


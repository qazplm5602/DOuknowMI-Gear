using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCamera : SkillController
{
    public Image screenImage;

    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
        CaptureAndCastScreen();
    }

    void CaptureAndCastScreen()
    {
        string filePath = Application.persistentDataPath + "/screenshot.png";
        ScreenCapture.CaptureScreenshot("screenshot.png");

        Texture2D texture = LoadTextureFromFile(filePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        screenImage.sprite = sprite;
    }

    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
}

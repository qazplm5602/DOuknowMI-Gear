using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillCamera : SkillController
{
    [SerializeField] private LayerMask _enemyLayerMask;
    List<Enemy> _targets;

    private void Start()
    {
        StartCoroutine(CaptureAndCastScreen());
        StartCoroutine(MoveRoutine(transform));
    }

    //"C:\Develop\GitHubDesktop\DOuknowMI-Gear\Screenshots\2024-04-30-23-05-55screenshot.png"
    //"C:\Develop\GitHubDesktop\DOuknowMI-Gear\Screenshots\2024-04-30-23-05-55screenshot.png"
    IEnumerator CaptureAndCastScreen()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = timestamp + "screenshot.png";

        string filePath = $"C:/Develop/GitHubDesktop/DOuknowMI-Gear/Screenshots/{fileName}";
        ScreenCapture.CaptureScreenshot($"Screenshots/{fileName}");

        //(Map.Instance.CurrentStage as NormalStage).CurrentEnemies;
        try
        {
            _targets = (Map.Instance.CurrentStage as NormalStage).CurrentEnemies;
        }
        catch
        {
            Debug.LogError("맵없어서그냥FIndObjectsOfType시작 ㅅㄱ개똥같은거 ToList까지한다ㅋㅋ");
            _targets = FindObjectsOfType<Enemy>().ToList();
            if (_targets.Count <= 0)
            {
                Debug.LogError("적도없음님아ㅈ[ㅔ바ㅣㄹ");
            }
        }
        yield return new WaitForSeconds(0.15f);

        Texture2D texture = LoadTextureFromFile(filePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        StageManager.Instance._screenshotImage.gameObject.SetActive(true);
        StageManager.Instance._screenshotImage.sprite = sprite;

        for (int i = 0; i < 3; ++i)
        {
            foreach (Enemy enemy in _targets)
            {
                if (enemy.TryGetComponent(out IDamageable health))
                {
                    health.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
                    print("님아");
                }
            }
        }

        yield return new WaitForSeconds(_destroyTime - (_destroyTime * 0.3f));

        print("set");

        StageManager.Instance._screenshotImage.gameObject.SetActive(false);
        StageManager.Instance._screenshotImage.sprite = null;
    }

    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
}

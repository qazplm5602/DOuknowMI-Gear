using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCamera : SkillController
{
    [SerializeField] private LayerMask _enemyLayerMask;
    List<Enemy> _targets;

    private void Start()
    {
        StartCoroutine(CaptureAndCastScreen());
        StartCoroutine(MoveRoutine(transform));
    }

    IEnumerator CaptureAndCastScreen()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = timestamp + "screenshot.png";

        string filePath = $"C:/Develop/GitHubDesktop/DOuknowMI-Gear/Screenshots/{fileName}";
        ScreenCapture.CaptureScreenshot($"Screenshots/{fileName}");

        _targets = (Map.Instance.CurrentStage as NormalStage).CurrentEnemies;
        if(_targets.Count == 0)
        {
            Debug.Log("적이없거나 맵매니저가없음");
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
                    PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, enemy.transform);
                    health.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
                }
            }
        }
        yield return new WaitForSeconds(_destroyTime - (_destroyTime * 0.3f));

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

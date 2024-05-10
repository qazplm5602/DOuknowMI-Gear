using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCamera : SkillController
{
    [SerializeField] private LayerMask _enemyLayerMask;
    List<Collider2D> colls;

    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundType.CamShutter);
        StartCoroutine(CaptureAndCastScreen());
        StartCoroutine(MoveRoutine(transform));
    }

    IEnumerator CaptureAndCastScreen()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = timestamp + "screenshot.png";

        string filePath = $"C:/Develop/GitHubDesktop/DOuknowMI-Gear/Screenshots/{fileName}";
        ScreenCapture.CaptureScreenshot($"Screenshots/{fileName}");

        colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(18, 10), 0, _enemyLayerMask).ToList();
        if(colls.Count == 0)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.15f);

        Texture2D texture = LoadTextureFromFile(filePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        GearManager.Instance.MakeImage(sprite);

        for (int i = 0; i < 3; ++i)
        {
            foreach (Collider2D enemy in colls)
            {
                if (enemy.TryGetComponent(out IDamageable health))
                {
                    PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, enemy.transform);
                    health.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
                }
            }
        }
        yield return new WaitForSeconds(_destroyTime - (_destroyTime * 0.3f));
    }

    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
}

using FSM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public List<Enemy> _enemies = new();

    [SerializeField] private TextMeshProUGUI _clearText;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    [SerializeField] private string _clearSceneName;
    [SerializeField] private string _gameOverSceneName;

    bool _isClear = false;  
    bool _isGameOver = false;  
    private void Update()
    {
        foreach (Enemy e in _enemies)
        {
            if(e.isDead) _enemies.Remove(e);
        }

        if (_enemies.Count <= 0) _isClear = true; 
        if(PlayerManager.instance.player.isDead) _isGameOver = true;

        if (_isClear)
        {
            _clearText.gameObject.SetActive(true);
            StartCoroutine(ChangeSceneToGameClear());
        }

        if (_isGameOver)
        {
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(ChangeSceneToGameOver());
        }
    }

    private IEnumerator ChangeSceneToGameClear()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_clearSceneName);
    }

    private IEnumerator ChangeSceneToGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_gameOverSceneName);
    }
}

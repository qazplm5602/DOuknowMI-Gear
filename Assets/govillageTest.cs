using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class govillageTest : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.insertKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Village");
        }
    }
}

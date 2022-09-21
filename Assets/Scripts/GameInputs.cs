using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputs : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){ Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.Backspace)){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}

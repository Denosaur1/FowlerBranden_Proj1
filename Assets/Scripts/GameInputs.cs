using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputs : MonoBehaviour
{


    private static GameInputs instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;

        }
        else { Destroy(this.gameObject); }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){ QuitGame();  }
        if (Input.GetKeyDown(KeyCode.Backspace)){ ReloadScene(); }
        if (Input.GetKeyDown(KeyCode.Return)){ ReloadGame(); }
    }

    public void NextScene() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void ReloadScene() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public void QuitGame() {
        Time.timeScale = 1.0f;
        Application.Quit(); }

    public void ReloadGame() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scene_Controller : MonoBehaviour
{
    // objects to swap activity if needed
    GameObject _on, _off;

    // name of level to load when button is clicked
    string _level_name;

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int build_index)
    {
        PlayerPrefs.Save();

        // reset time scale if we were paused
        Time.timeScale = 1;

        if (build_index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(build_index);
        else
            Debug.Log("Scene index out of range.");
    }
    public void LoadLevel(string name)
    {
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void LoadMainMenu()
    {
        LoadLevel("MainMenu");
    }

    public void SwapObjects()
    {
        _off.SetActive(false);
        _on.SetActive(true);
    }

    // set the object to set active
    public void SetObjectOn(GameObject on)
    {
        _on = on;
    }

    // set the object to deactivate
    public void SetObjectOff(GameObject off)
    {
        _off = off;
    }

    public void SetLevelToPlay(string level_name)
    {
        _level_name = level_name;
    }

    public void PlayButtonClick()
    {
        LoadLevel(_level_name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public Slider volumeSlider;
    public AudioSource volumeAudio;

    public void PlayGame()
    {
        Debug.Log("PlayGame Clicked");
        Cursor.visible = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        Debug.Log("PlayLevel2 Clicked");
        Cursor.visible = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void PlayLevel3()
    {
        Debug.Log("PlayLevel3 Clicked");
        Cursor.visible = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame Clicked");
        Application.Quit();
    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}

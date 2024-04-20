using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private string newGameScene = "SampleScene";

    public AudioClip bg_music;
    public AudioSource bg_music_source;

    void Start()
    {
        bg_music_source.PlayOneShot(bg_music);

        // Set the high score text to the saved high score
        int highScore = SaveLoadmanager.Instance.LoadHighScore();
        highScoreText.text = "Top Wave Survived: " + highScore;
    }

   public void StartNewGame()
    {
        bg_music_source.Stop();

        // Load the new game scene
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;

    public TextMeshProUGUI PlayerHealthUI;
    public GameObject gameOverUI;

    public bool isDead = false;

    private void Start()
    {
        PlayerHealthUI.text = "Health : " + HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            // Game over
            // Respawn player
            playerDeath();
            isDead = true;
        }
        else
        {
            print(damageAmount + " damage taken. Player HP: " + HP);
            StartCoroutine(ShowBloodyScreen());
            PlayerHealthUI.text = "Health : " + HP;
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void playerDeath()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverSound;
        SoundManager.Instance.playerChannel.PlayDelayed(1f);

        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        //Dying animation
        GetComponentInChildren<Animator>().enabled = true;
        PlayerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        int waveSurvived = GlobalReferences.Instance.waveNumber;

        if(waveSurvived-1 > SaveLoadmanager.Instance.LoadHighScore())
        {
            SaveLoadmanager.Instance.SaveHighScore(waveSurvived - 1);
        }

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ShowBloodyScreen()
    {
        if(bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyScreen.activeInHierarchy == true)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damageAmount);
            }
        }
    }
}

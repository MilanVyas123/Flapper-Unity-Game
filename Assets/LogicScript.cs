using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text levelText;
    public GameObject gameOverScreen;
    public BirdScript bird;
    public AudioSource onSpacePressAudio;

    public Sprite newBgSprite1; // New sprite for level 1
    public Sprite newBgSprite2; // New sprite for level 2
    public Sprite newBgSprite3; // New sprite for level 3

    private SpriteRenderer bg1Renderer;
    private SpriteRenderer bg2Renderer;

    private int level = 1;

    private void Start()
    {
        // Ensure AudioSource is assigned
        if (onSpacePressAudio == null)
        {
            onSpacePressAudio = GetComponent<AudioSource>();
        }

        // Get the SpriteRenderer components of bg1 and bg2
        Transform bgTransform = GameObject.Find("BG").transform; // Ensure "BG" is the correct name
        bg1Renderer = bgTransform.Find("Background1").GetComponent<SpriteRenderer>();
        bg2Renderer = bgTransform.Find("Background2").GetComponent<SpriteRenderer>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        PlaySound();
        playerScore += scoreToAdd;
        PipeSpawnScript pipeSpawnScript = GameObject.Find("Pipe Spawner").GetComponent<PipeSpawnScript>();

        // Check for background change and level up
        if (playerScore == 10)
        {
            level++;
            pipeSpawnScript.spawnRate = 3;
            StartCoroutine(ChangeBackground(newBgSprite1));
        }
        else if (playerScore == 20)
        {
            level++;
            pipeSpawnScript.spawnRate = 2;

            StartCoroutine(ChangeBackground(newBgSprite2));
        }
        else if (playerScore == 30)
        {
            level++;
            pipeSpawnScript.spawnRate = 1.8f;

            StartCoroutine(ChangeBackground(newBgSprite3));
        }

        // Update UI
        levelText.text = "Level " + level.ToString();
        scoreText.text = "Score - "+playerScore.ToString();
    }

    private IEnumerator ChangeBackground(Sprite newSprite)
    {
        // Fade out the current sprites
        float fadeDuration = 1f; // Duration for fading
        Color fadeOutColor = new Color(1, 1, 1, 0); // Transparent color
        Color originalColor1 = bg1Renderer.color;
        Color originalColor2 = bg2Renderer.color;

        // Fade out bg1
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            bg1Renderer.color = Color.Lerp(originalColor1, fadeOutColor, elapsedTime / fadeDuration);
            bg2Renderer.color = Color.Lerp(originalColor2, fadeOutColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Change the sprite
        bg1Renderer.sprite = newSprite;
        bg2Renderer.sprite = newSprite;

        // Fade in bg1
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            bg1Renderer.color = Color.Lerp(fadeOutColor, originalColor1, elapsedTime / fadeDuration);
            bg2Renderer.color = Color.Lerp(fadeOutColor, originalColor2, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void PlaySound()
    {
        if (onSpacePressAudio.isPlaying)
        {
            onSpacePressAudio.Stop();
        }
        onSpacePressAudio.Play();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}

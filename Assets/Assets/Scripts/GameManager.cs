using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool gameHasEnded = false;

    [HideInInspector] public float timeDelay = .25f;

    public PlayerController playerController;
    public TextMeshPro scoreUI;
    public Spawn spawner;
    public Transform pauseMenu;
    public Volume vignette;
    public Transform cameraAnchor;
    public RectMask2D gameOverMask;

    public int score = 0;

    public int maxKingdom = 3;

    private int[] cartNumbers = { -1, -1, -1 };

    private int kingdomIncrementer;
    public int kingdomPoolIncreaseThreshold;

    public int scoreMultiplier = 1;

    private int scoreToAdd = 0;

    private void Awake()
    {
        instance = this;
    }

    public void EndGame()
    {
        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(false);

        SaveScore();

        while (vignette.weight < 1f)
        {
            cameraAnchor.position = Vector3.Lerp(cameraAnchor.position, Vector3.up * 50, Time.unscaledDeltaTime * 0.25f);
            vignette.weight += Time.unscaledDeltaTime * 0.5f;
            yield return null;
        }

        while (gameOverMask.padding.magnitude > 1f)
        {
            gameOverMask.padding = Vector4.Lerp(gameOverMask.padding, Vector4.zero, Time.unscaledDeltaTime * 2);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(0);
    }

    private void SaveScore()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    public void AddToScore(int count)
    {
        if (scoreToAdd == 0)
        {
            StartCoroutine(AddScoreRoutine());
        }
        scoreToAdd += count;
    }

    private IEnumerator AddScoreRoutine()
    {
        yield return null;
        while (scoreToAdd > 0)
        {
            scoreToAdd--;
            IncrementScore(scoreMultiplier);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void IncrementScore(int amount)
    {
        score += amount;
        scoreUI.text = score.ToString();
    }

    public void RestartGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", timeDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetNextKingdomNumber(int cartIndex)
    {
        int output = -1;
        while (true)
        {
            output = Random.Range(0, maxKingdom + 1);
            bool allowed = true;
            for (int i = 0; i < 3; i++)
            {
                if (output == cartNumbers[i])
                {
                    allowed = false;
                }
            }
            if (allowed) break;
        }

        cartNumbers[cartIndex] = output;
        kingdomIncrementer++;

        if (kingdomIncrementer > kingdomPoolIncreaseThreshold)
        {
            kingdomIncrementer = 0;
            if (maxKingdom == 7)
            {
                scoreMultiplier++;
            }
            else
            {
                maxKingdom++;
            }
        }

        return output;
    }
}

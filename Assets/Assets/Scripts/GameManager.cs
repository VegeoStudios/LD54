using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool gameHasEnded = false;

    [HideInInspector] public float timeDelay = .25f;

    public PlayerController playerController;

    public int maxKingdom = 3;

    private int[] cartNumbers = { -1, -1, -1 };

    private int kingdomIncrementer;
    public int kingdomPoolIncreaseThreshold;

    public int scoreMultiplier = 1;

    private void Awake()
    {
        instance = this;
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
        cartNumbers[cartIndex] = -1;
        int output = -1;
        for (int j = 0; j < 20; j++)
        {
            output = Random.Range(0, maxKingdom);
            bool allowed = true;
            for (int i = 0; i < 2; i++)
            {
                if (output == cartNumbers[i])
                {
                    allowed = false;
                    break;
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

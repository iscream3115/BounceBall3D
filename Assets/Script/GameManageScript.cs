using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManageScript : MonoBehaviour
{
    public static GameManageScript instance;
    [SerializeField] int score;
    private int PointCount;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointCount = GameObject.FindGameObjectsWithTag("Point").Length;
    }

    public void PointCollected()
    {
        score++;

        if (score >= PointCount)
        {
            GoToNextScene();
        }
    }

    // Update is called once per frame
    void GoToNextScene()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetTotalPoints()
    {
        return PointCount;
    }

    public int GetCurrentScore()
    {
        return score;
    }
}

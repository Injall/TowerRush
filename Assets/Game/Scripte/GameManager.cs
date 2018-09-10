using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelAmount = 10;
    public Level[] levelsPrefabs;
    public TweenPosition cameraTween;
    private static GameManager instance;
    private int currentLevel = 0;

    private List<Level> levelInstantied = new List<Level>();
    private float levelUnitHeight = 0;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();
            instance = value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
            NextLevel();
    }

    // Use this for initialization
    void Start()
    {
        levelUnitHeight = 1080 / (float)levelsPrefabs[0].SpriteRenderer.sprite.pixelsPerUnit;

        RestartGame();
    }

    public void RestartGame()
    {
        // Clear rooms pool
        for (int i = 0; i < levelInstantied.Count; ++i)
            Destroy(levelInstantied[i]);

        levelInstantied.Clear();

        // Create new rooms pool
        for (int i = 0; i < levelAmount; ++i)
        {
            Level level = Instantiate(levelsPrefabs[Random.Range(0, levelsPrefabs.Length)].gameObject).GetComponent<Level>();
            level.transform.position = new Vector3(0, levelUnitHeight * i, 0);

            levelInstantied.Add(level);
        }

        GoToLevel(0);
    }

    private void GoToLevel(int level)
    {
        // Move the camera to the level
        cameraTween.src = cameraTween.dst;
        cameraTween.dst = new Vector3(0, levelUnitHeight * level, -2);
        cameraTween.ResetAtBeginning();
        cameraTween.PlayForward();

        // Init the level
        levelInstantied[level].StartLevel();
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel == levelAmount) // If all levels have been succeed, win the game
        {
            print("You WON ! Let's do it again !");
            RestartGame();
        }
        else // If there is the levels to play, go to the next
        {
            GoToLevel(currentLevel);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int roomAmount = 10;
    public Level[] roomsPrefabs;
    public TweenPosition cameraTween;
    private static GameManager instance;
    private int currentLevel = 0;

    private List<Level> roomsInstantied = new List<Level>();
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

    // Use this for initialization
    void Start()
    {
        levelUnitHeight = 1080 / (float)roomsPrefabs[0].SpriteRenderer.sprite.pixelsPerUnit;

        RestartGame();
    }

    public void RestartGame()
    {
        // Clear rooms pool
        for (int i = 0; i < roomsInstantied.Count; ++i)
            Destroy(roomsInstantied[i]);

        roomsInstantied.Clear();

        // Create new rooms pool
        for (int i = 0; i < roomAmount; ++i)
        {
            Level r = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)].gameObject).GetComponent<Level>();

            r.transform.position = new Vector3(0, levelUnitHeight * i, 0);

            roomsInstantied.Add(r);
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
        roomsInstantied[level].StartLevel();
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel == roomAmount) // If all levels have been succeed, win the game
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

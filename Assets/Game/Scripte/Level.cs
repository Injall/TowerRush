using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class Level : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    // Is automatically called when the level start
    public virtual void StartLevel()
    {
        print("Start Level " + gameObject.name);
    }

    // Need to be called when the level end
    public virtual void EndLevel(bool success)
    {
        if (success)
        {
            print("Level succeed, go to the next level");
            GameManager.Instance.NextLevel();
        }
        else
        {
            print("You died, restart the game");
            GameManager.Instance.RestartGame();
        }
    }
}

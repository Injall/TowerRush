using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExample : Level
{
    // Is automatically called when the level start
    public override void StartLevel()
    {
        base.StartLevel();
    }

    // Need to be called when the level end
    public override void EndLevel(bool success)
    {
        base.EndLevel(success);
    }
}

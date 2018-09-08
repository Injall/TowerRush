using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int roomAmount = 10;
    public SpriteRenderer[] roomsPrefabs;

    private List<SpriteRenderer> roomsInstantied = new List<SpriteRenderer>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < roomAmount; ++i)
        {
            SpriteRenderer r = GameObject.Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)].gameObject).GetComponent<SpriteRenderer>();

            r.transform.position = new Vector3(0, (1080 / (float)r.sprite.pixelsPerUnit) * i, 0);

            roomsInstantied.Add(r);
        }
    }

}

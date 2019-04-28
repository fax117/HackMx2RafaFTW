using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    public Transform player;
    public GamerManager gm;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + 6, 0, -10);

        if (!player)
        {
            gm.EndGame();
        }
    }
}

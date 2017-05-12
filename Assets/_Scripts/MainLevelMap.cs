using UnityEngine;
using System.Collections;

public class MainLevelMap : TutorialLevelMap
{

    private bool start = false;

    void Start()
    {
        TutorialLevelMap.OnMapEndReached += HandleOnComplete;
    }

    void Update()
    {
        if (start) { 
            Vector3 playerPos = player.GetPosition();
            if (Mathf.Abs(playerPos.x - endPositionX) <= error && 
                Mathf.Abs(playerPos.z - endPositionZ) <= error &&
                OnMapEndReached != null)
            {
                OnMapEndReached();
            }
        }
    }

    void HandleOnComplete()
    {
        player.SetPosition(new Vector3(startPositionX, startHeight, startPositionZ));
        start = true;
    }
}

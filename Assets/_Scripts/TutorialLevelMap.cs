using UnityEngine;
using System.Collections;

public class TutorialLevelMap : MonoBehaviour
{
    public delegate void MapEndReached();
    public static MapEndReached OnMapEndReached;

    public Player player;

    public int startPositionX;
    public int startPositionZ;
    public int endPositionX;
    public int endPositionZ;
    public int startHeight = 100;
    public int error = 10;

    private bool start = false;

    void Start()
    {
        Countdown.OnCalibrationComplete += HandleOnCalibrationComplete;
    }

    void Update()
    {
        if (start)
        {
            Vector3 playerPos = player.GetPosition();
            if (Mathf.Abs(playerPos.x - endPositionX) <= error &&
                Mathf.Abs(playerPos.z - endPositionZ) <= error &&
                OnMapEndReached != null)
            {
                OnMapEndReached();
            }
        }
    }

    void HandleOnCalibrationComplete()
    {
        player.SetPosition(new Vector3(startPositionX, startHeight, startPositionZ));
        start = true;
    }

    public float GetDistanceToGoal()
    {
        Vector3 playePos = player.GetPosition();
        return Mathf.Sqrt(Mathf.Pow(playePos.x - endPositionX, 2) + Mathf.Pow(playePos.z - endPositionZ, 2));
    }
}

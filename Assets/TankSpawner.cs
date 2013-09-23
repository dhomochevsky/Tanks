using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TankSpawner : MonoBehaviour, IComparable
{
    private List<GameObject> spawnQueue = new List<GameObject>();

    private uint spawnTimer = 1000;

    public int spawnCount
    {
        get { return spawnQueue.Count; }
    }

    public void spawnTank(GameObject tank)
    {
        spawnQueue.Add(tank);
        startSpawnTimer();
    }

    private void startSpawnTimer()
    {
        if (spawnQueue.Count > 0)
        {
            var timer = new Timer(spawnTimer);
            timer.Elapsed += actuallySpawnTank;
        }
    }

    private void actuallySpawnTank (object sender, ElapsedEventArgs elapsedEventArgs)
    {
        spawnQueue.RemoveAt((spawnQueue.Count - 1));

        startSpawnTimer();
    }

    public int CompareTo(object obj)
    {
        var other = (TankSpawner) obj;

        return other.spawnCount - this.spawnCount;
    }
}

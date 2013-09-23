using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class TankSpawnCoordinator : MonoBehaviour
{
    public GameObject enemyTank;

    public GameObject spawner;

    public GameObject enemyLifeCounter;

    public uint concurrentEnemyMax = 4;

    private List<GameObject> livingTanks = new List<GameObject>();

    private TankSpawner[] spawners;

    public void initWithSpawners(Vector2[] spawnerPositions)
    {
        spawners = new TankSpawner[spawnerPositions.Length];
        var i = 0u;
        foreach (var spawnerPos in spawnerPositions)
        {
            var newSpawner = Instantiate(spawner, spawnerPos, Quaternion.identity) as GameObject;
            Debug.Assert(newSpawner != null, "newSpawner != null");
            var tankSpawner = newSpawner.GetComponent<TankSpawner>();
            spawners[i] = tankSpawner;
            i++;
        }

        while (livingTanks.Count < concurrentEnemyMax)
        {
            spawnTank();
        }
    }

    public void tankDestroyed(GameObject tank)
    {
        livingTanks.Remove(tank);

        this.spawnTank();
    }

    private void spawnTank()
    {
        var lifeCounter = enemyLifeCounter.GetComponent<LifeCounter>();
        if (lifeCounter.lives > 0)
        {
            var newTank = Instantiate(enemyTank, new Vector2(0, 0), Quaternion.identity) as GameObject;
            livingTanks.Add(newTank);
            spawners.Min().spawnTank(newTank);
            lifeCounter.lives--;
        }
    }
}

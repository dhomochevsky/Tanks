using UnityEngine;

public class Tanks : MonoBehaviour
{
    public GameObject level;
    public GameObject lifeCounter;
    public GameObject tankSpawnCoordinator;

	// Use this for initialization
	void Start () {
        var map = level.GetComponent<MapDrawer>();
        map.loadMapFile("map");

	    var enemyLife = lifeCounter.GetComponent<LifeCounter>();

        enemyLife.position = new Vector2(map.map.GetLength(0), map.map.GetLength(1));
	    enemyLife.lives = 10;

        tankSpawnCoordinator.GetComponent<TankSpawnCoordinator>().initWithSpawners(map.spawners);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

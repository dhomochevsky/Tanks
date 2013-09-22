using System;
using System.Collections.Generic;
using JsonFx.Json;
using JsonFx.Serialization;
using UnityEngine;
using System.Runtime.Serialization;
using JsonFx.Serialization.Resolvers;

public class MapDrawer : MonoBehaviour
{
    public GameObject tile;

    [DataContract]
    private class MapFile
    {
        [DataMember] public Dictionary<string, GameObjectDefinition> tiles;

        [DataMember] public int mapSize;

        [DataMember] public string[] map;
    }

    [DataContract]
    private class GameObjectDefinition
    {
        // if tanks or bullets stop when hitting it
        [DataMember] public bool collidable = false;

        // how far your tank slides after you let go of controls
        [DataMember] public int slipperyness = 0;

        // if you bullet does less than this much piercing, doesn't deal damage
        [DataMember] public int armor = 0;

        [DataMember] public string sprite = "";
    }


    public GameObject[,] map;

	// Use this for initialization
	void Start ()
	{
        loadMapFile("map");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void loadMapFile(string fileName)
    {
	    var map = Resources.Load<TextAsset>(fileName);

        var reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
        var mapFile = reader.Read<MapFile>(map.text);
        if (mapFile.map.Length != Math.Pow(mapFile.mapSize, 2))
        {
            throw new Exception("invalid map size");
        }

        this.map = new GameObject[mapFile.mapSize,mapFile.mapSize];
        for (var row = 0; row < mapFile.mapSize; row++)
        {
            for (var column = 0; column < mapFile.mapSize; column++)
            {
                var tileName = mapFile.map[row*mapFile.mapSize + column];
                this.map[row, column] = makeGameObject(mapFile.tiles[tileName].sprite, row, column);
            }
        }
    }

    GameObject makeGameObject(string texture, int x, int y)
    {
        Debug.Log(texture + " " + x + " " + y);
	    var newGameObject = Instantiate(tile, new Vector3((float)x / 2, (float)y / 2), Quaternion.identity) as GameObject;

	    newGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(texture);

        return newGameObject;
    }
}

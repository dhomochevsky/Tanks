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
        [DataMember] public Dictionary<string, TileDefinition> tiles;

        [DataMember] public int mapSize;

        [DataMember] public string[] map;
    }

    [DataContract]
    private class TileDefinition
    {
        // if tanks or bullets stop when hitting it
        [DataMember] public bool collidable = false;

        // how far your tank slides after you let go of controls
        [DataMember] public int slipperyness = 0;

        // if you bullet does less than this much piercing, doesn't deal damage
        [DataMember] public int armor = 0;

        [DataMember] public string sprite = "";
    }

    private MapFile mapDefinition;


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
        mapDefinition = reader.Read<MapFile>(map.text);
        int mapSize = mapDefinition.mapSize;
        if (mapDefinition.map.Length != mapSize * mapSize)
        {
            throw new Exception("invalid map size");
        }

        this.map = new GameObject[mapSize,mapSize];
        for (var row = 0; row < mapSize; row++)
        {
            for (var column = 0; column < mapSize; column++)
            {
                var tileName = mapDefinition.map[row*mapSize + column];
                makeGameObject(tileName, row, column);
            }
        }
    }

    public GameObject makeGameObject(string defName, int x, int y)
    {
        Debug.Log(defName + " " + x + " " + y);

        var def = this.mapDefinition.tiles[defName];
        var position = new Vector2((float) x/2, (float) y/2);
	    var newGameObject = Instantiate(tile, position, Quaternion.identity) as GameObject;

	    newGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(def.sprite);

        var tileComponent = newGameObject.GetComponent<Tile>();
        tileComponent.collidable = def.collidable;
        tileComponent.slipperyness = def.slipperyness;
        tileComponent.armor = def.armor;
        tileComponent.position = position;
        tileComponent.map = this;

        this.map[x, y] = newGameObject;

        return newGameObject;
    }
}

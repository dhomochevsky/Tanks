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

        [DataMember] public uint mapSize;

        [DataMember] public string[] map;

        [DataMember] public uint mapScale = 2;

        [DataMember] public uint baseX = 6;
        [DataMember] public uint baseY = 0;
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

    public void loadMapFile(string fileName)
    {
	    var map = Resources.Load<TextAsset>(fileName);

        var reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
        mapDefinition = reader.Read<MapFile>(map.text);
        var mapSize = mapDefinition.mapSize;
        var mapScale = mapDefinition.mapScale;

        if (mapDefinition.map.Length != mapSize * mapSize)
        {
            throw new Exception("invalid map size");
        }

        this.map = new GameObject[mapSize * mapScale,mapSize * mapScale];
        for (var row = 0u; row < this.map.GetLength(0) / mapScale; row++)
        {
            for (var column = 0u; column < this.map.GetLength(1) / mapScale; column++)
            {
                var tileName = mapDefinition.map[row*mapSize + column];
                makeTileFromTemplate(tileName, row * mapScale, column * mapScale, mapScale);
            }
        }

        var baseTile = makeTile(mapDefinition.baseX, mapDefinition.baseY);
        baseTile.GetComponent<Tile>().destroyed += (sender, args) => Debug.Log("YOU LOSE");
        setTiles(baseTile, mapDefinition.baseX, mapDefinition.baseY, 4);
    }

    private void setTiles(GameObject tile, uint x, uint y, uint size)
    {
        x = x*mapDefinition.mapScale;
        for(var row = x; row < x + size; row++)
        {
            for(var column = y; column < y + size; column++)
            {
                map[row, column] = tile;
            }
        }
    }

    public GameObject makeTile(uint x, uint y)
    {
        var position = new Vector2(x, y);
        return makeTile(position);
    }

    public GameObject makeTile(Vector2 position)
    {
	    var newGameObject = Instantiate(tile, position, Quaternion.identity) as GameObject;
        return newGameObject;
    }

    public void makeTileFromTemplate(string defName, uint x, uint y, uint size)
    {
        for (var row = x; row < x + size; row++)
        {
            for (var column = y; column < y + size; column++)
            {
                makeTileFromTemplate(defName, column, row);
            }
        }
    }

    public GameObject makeTileFromTemplate(string defName, uint x, uint y)
    {
        var def = this.mapDefinition.tiles[defName];
        var position = new Vector2(x, y) / 2;
        var newGameObject = makeTile(position);

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

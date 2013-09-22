using UnityEngine;
using System.Runtime.Serialization;

public class Tile : MonoBehaviour
{
    // if tanks or bullets stop when hitting it
    public bool collidable = false;

    // how far your tank slides after you let go of controls
    public int slipperyness = 0;

    // if you bullet does less than this much piercing, doesn't deal damage
    public int armor = 0;

    public string sprite = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

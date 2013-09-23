using UnityEngine;

public class Tile : MonoBehaviour
{
    public MapDrawer map;

    // if tanks or bullets stop when hitting it
    public bool collidable = false;

    // how far your tank slides after you let go of controls
    public int slipperyness = 0;

    // if you bullet does less than this much piercing, doesn't deal damage
    public int armor = 0;

    public string sprite = "";

    public Vector2 position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void destroy()
    {
        this.map.makeGameObject("empty", (int) position.x, (int) position.y);
    }
}

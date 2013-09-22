using UnityEngine;
using System.Collections;

public class MapDrawer : MonoBehaviour
{
    public GameObject tile;

	// Use this for initialization
	void Start ()
	{
	    var newTile = Instantiate(tile, new Vector3(0, 0), Quaternion.identity) as GameObject;

	    newTile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("brick");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

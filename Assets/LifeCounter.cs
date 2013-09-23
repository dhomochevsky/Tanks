using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour
{
    public GameObject enemyLifeSprite;

    private uint _lives = 9;
    public uint lives
    {
        get { return _lives; }
        set { _lives = value;
            updateSprites();
        }
    }

    private Vector2 position = new Vector2(6,6);

    private int columns = 2;

    private void updateSprites()
    {
        for (var i = 0; i < _lives; i++)
        {
            var offset = new Vector2(i % 2, -i / 2);
            Instantiate(enemyLifeSprite, (position + offset) / 2, Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
        updateSprites();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

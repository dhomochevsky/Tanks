using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public GameObject enemyLifeSprite;
    public GameObject map;

    private uint _lives = 9;
    public uint lives
    {
        get { return _lives; }
        set { _lives = value;
            updateSprites();
        }
    }

    public Vector2 position;

    private int columns = 2;

    private void updateSprites()
    {
        Debug.Log("ASDFSD" + _lives);
        for (var i = 0; i < _lives; i++)
        {
            var offset = new Vector2(i % columns, -i / columns);
            Instantiate(enemyLifeSprite, (position + offset) / 2, Quaternion.identity);
        }
    }
}

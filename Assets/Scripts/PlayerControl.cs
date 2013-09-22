using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 2f;				// The fastest the player can travel in the x axis.

    private bool moveHorizontal;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        moveHorizontal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Up") || Input.GetButtonDown("Down")) ||
            (Input.GetButtonUp("Left") || Input.GetButtonUp("Right")))
        {
            moveHorizontal = false;
        }
        if ((Input.GetButtonDown("Left") || Input.GetButtonDown("Right")) ||
            (Input.GetButtonUp("Up") || Input.GetButtonUp("Down")))
        {
            moveHorizontal = true;
        }
    }

    // ReSharper disable CompareOfFloatsByEqualityOperator
    private void FixedUpdate()
    {
        float x = (Input.GetButton("Left") ? -1 : 0) + (Input.GetButton("Right") ? 1 : 0);
        float y = (Input.GetButton("Down") ? -1 : 0) + (Input.GetButton("Up") ? 1 : 0);

        if (moveHorizontal)
        {
            rigidbody2D.velocity = new Vector2(x*maxSpeed, 0f);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0f, y*maxSpeed);
        }
    }
    // ReSharper restore CompareOfFloatsByEqualityOperator
}

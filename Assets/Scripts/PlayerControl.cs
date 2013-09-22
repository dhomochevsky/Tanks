using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 2f;				// The fastest the player can travel in the x axis.

    private float prevX, prevY;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        prevX = 0f;
        prevY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Debug.Log("foo");
        }
    }

    // ReSharper disable CompareOfFloatsByEqualityOperator
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Debug.Log(y);
        x = (x == 0f ? 0 : Mathf.Sign(x));
        y = (y == 0f ? 0 : Mathf.Sign(y));

        if (true)
        {
            rigidbody2D.velocity = new Vector2(0f, y * maxSpeed);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(x * maxSpeed, 0f);
        }

        prevX = x;
        prevY = y;
    }
    // ReSharper restore CompareOfFloatsByEqualityOperator
}

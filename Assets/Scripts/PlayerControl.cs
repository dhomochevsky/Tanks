using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 2f;				// The fastest the player can travel in the x axis.
    public float animationSpeed = 10f;
    private Animator anim;

    private bool moveHorizontal;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        moveHorizontal = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ReSharper disable CompareOfFloatsByEqualityOperator
    private void FixedUpdate()
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

        float x = (Input.GetButton("Left") ? -1 : 0) + (Input.GetButton("Right") ? 1 : 0);
        float y = (Input.GetButton("Down") ? -1 : 0) + (Input.GetButton("Up") ? 1 : 0);

        if (x != 0f && y != 0f)
        {
            x = (moveHorizontal ? x : 0f);
            y = (moveHorizontal ? 0f : y);
        }
        rigidbody2D.velocity = new Vector2(x*maxSpeed,y*maxSpeed);

        float speed = ((x == 0f && y == 0f) ? 0f : 1f);
        anim.speed = speed*animationSpeed;

        SetFacing();
    }

    private void SetFacing()
    {
        float x = rigidbody2D.velocity.x;
        float y = rigidbody2D.velocity.y;

        if (x > 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0,0,-90);
            transform.localScale = new Vector3(1,1,1);
        }
        else if (x < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, 90);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (y > 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (y < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    // ReSharper restore CompareOfFloatsByEqualityOperator
}

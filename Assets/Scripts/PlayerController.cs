using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool onGround;
    // Movement settings
    [SerializeField] float jumpVelocity = 2.0f;
    [SerializeField] float groundSpeedMult = 5.0f;
    [SerializeField] float airSpeedMult = 1.0f;

    // Dampening settings
    [SerializeField] float groundMoveDampening = 10.0f;
    [SerializeField] float groundStopDampening = 20.0f;
    [SerializeField] float airMoveDampening = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        // Assume no input
        float dirInput = 0;

        // Get move input
        if (Input.GetKey(KeyCode.D)) dirInput += 1;
        if (Input.GetKey(KeyCode.A)) dirInput -= 1;
 
        // What to do if on ground
        if (onGround)
        {
            rb.velocity += new Vector3(dirInput * groundSpeedMult, 0, 0) * Time.deltaTime;
        }
        else
        {
            rb.velocity += new Vector3(dirInput * airSpeedMult, 0, 0) * Time.deltaTime;
        }

        // How much to slow down player if on ground
        if (onGround)
        {
            // If no input or(|| means or) trying to move in opposite direction
            // Mathf.Sign returns either 1 or -1 (or 0 I guess) so we can compare if they're the same.
            if (dirInput == 0 || Mathf.Sign(rb.velocity.x) != Mathf.Sign(dirInput)) 
                rb.velocity -= new Vector3(Time.deltaTime * rb.velocity.x * groundStopDampening, 0, 0);
            else rb.velocity -= new Vector3(Time.deltaTime * rb.velocity.x * groundMoveDampening, 0, 0);
            // By subtracting velocity with itself by a lower value (Time.deltaTime is very small!) we get resistance!
            // Higher velocity means higher restistance, just like real physics!
        }
        else
        {
            // If in air, slow down with air dampening variable!
            rb.velocity -= new Vector3(Time.deltaTime * rb.velocity.x * airMoveDampening, 0, 0);
        }

        // If space is pressed, call Jump method!
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void Jump()
    {
        // Can only jump if on ground!
        if (onGround)
        {
            // If jumped, you are no longer on the ground! (Safety measure)
            onGround = false;
            // Set y velocity to jump velocity. This will make all jumps exactly the same height (unless collision or something)
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            // Reset jump timer
        }
    }

    public void SetOnGround(bool onGround)
    {
        this.onGround = onGround;
    }
}

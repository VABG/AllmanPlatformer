using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_2D : MonoBehaviour
{
    bool onGround = false;
    [SerializeField] LayerMask layerMask;
    int direction = -1;
    Rigidbody rb;
    [SerializeField] float xForwardOffset = .5f;
    [SerializeField] float moveVel = 1.0f;
    [SerializeField] float groundDist = .3f;
    [SerializeField] float wallCheckDist = .5f;
    [SerializeField] float fallDistance = .5f;
    [SerializeField] float fallDistanceCheckExtraDist = .1f;
    AI_Anim anim;
    float rndTime = 1.0f;
    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<AI_Anim>();
    }

    // Update is called once per frame
    void Update()
    {
        OnGroundCheck();
        if (onGround)
        {
            RandomStop();
            WallCheck();
            GroundFallCheck();
            if (stop) rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            else rb.velocity = new Vector3(moveVel * direction, rb.velocity.y, rb.velocity.z);
        }

        anim.SetMoveSpeed(rb.velocity.x);

    }

    void WallCheck()
    {
        if (Physics.Raycast(new Ray(transform.position, new Vector3(direction, 0,0)), wallCheckDist, layerMask))
        {
            direction *= -1;
        }
    }

    void RandomStop()
    {
        rndTime -= Time.deltaTime;
        if (rndTime < 0)
        {
            stop = !stop;
            if (stop)
            {
                rndTime = Random.value * 2 + .5f;
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rndTime = Random.value * 5 + 2.0f;
            }
        }
    }

    void OnGroundCheck()
    {
        //onGround = false;

        Ray r = new Ray();
        r.origin = transform.position;
        r.direction = -transform.up;

        if (Physics.Raycast(r, out RaycastHit hit, 2.0f, layerMask))
        {
            // stuff here
            if (hit.distance < groundDist)
            {
                ChangeOnGround(true);                

            }
            else ChangeOnGround(false);
        }
        else ChangeOnGround(false);
    }

    void ChangeOnGround(bool groundState)
    {
        if (onGround == groundState) return;
        onGround = groundState;
        anim.ToggleOnGround(onGround);
    }

    void GroundFallCheck()
    {
        Ray r = new Ray();
        r.origin = transform.position + new Vector3(xForwardOffset * direction, 0,0);
        r.direction = -transform.up;

        if (Physics.Raycast(r, out RaycastHit hit, 2.0f, layerMask))
        {
            if (hit.distance > fallDistance)
            {
                r.origin += Vector3.right * direction * fallDistanceCheckExtraDist;
                if (Physics.Raycast(r, out RaycastHit hit2, 2.0f, layerMask))
                {
                    if (hit2.distance > fallDistance)
                    {
                        direction *= -1;
                    }
                }
            }
        }
    }

    public void DoJump(float height, float width)
    {
        float t = Mathf.Sqrt(Mathf.Abs(height / (Physics.gravity.y * .5f)));
        float v = t * Physics.gravity.y;
        transform.position += Vector3.up * .05f;
        rb.velocity = new Vector3(width/t, -v, 0);
        ChangeOnGround(false);
    }

    public void DoJump2(float height, float width, float xSpeed)
    {
        // x-vel based jump
        float t = Mathf.Abs(width/xSpeed);
        float vY = height + .5f * Physics.gravity.y * t*t;

        transform.position += Vector3.up * .05f;

        rb.velocity = new Vector3(xSpeed*direction, -vY, 0);
        ChangeOnGround(false);
    }
}

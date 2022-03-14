using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    bool onGround = false;
    bool facing = true;

    int moveSpeedID;
    int facingID;
    int onGroundID;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedID = Animator.StringToHash("MoveSpeed");
        facingID = Animator.StringToHash("Facing");
        onGroundID = Animator.StringToHash("OnGround");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnGround(bool onGround)
    {
        if (this.onGround != onGround)
        {
            this.onGround = onGround;
            anim.SetBool(onGroundID, onGround);
        }
    }

    public void SetSpeed(float speed)
    {
        anim.SetFloat(moveSpeedID, Mathf.Abs(speed));

        if ((speed >= 0 && !facing) || speed < 0 && facing)
        {
            facing = !facing;
            anim.SetBool(facingID, facing);
        }
    }
}

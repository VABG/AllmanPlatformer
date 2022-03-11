using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Anim : MonoBehaviour
{
    [SerializeField] Animator anim;

    int onGroundID;
    int moveSpeedID;
    int facingID;
    int fallSpeedID;

    bool lastDir = true;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedID = Animator.StringToHash("MoveSpeed");
        onGroundID = Animator.StringToHash("OnGround");
        facingID = Animator.StringToHash("Facing");
        fallSpeedID = Animator.StringToHash("FallSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOnGround(bool onGround)
    {
        anim.SetBool(onGroundID, onGround);
    }

    public void SetMoveSpeed(float speed)
    {
        anim.SetFloat(moveSpeedID,Mathf.Abs(speed));

        if (speed <= 0) Direction(false);
        else Direction(true);
    }

    public void SetFallSpeed(float speed)
    {
        anim.SetFloat(fallSpeedID, Mathf.Abs(speed));
    }

    public void Direction(bool right)
    {
        if (lastDir == right) return;
        lastDir = right;
        anim.SetBool(facingID, lastDir);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    [SerializeField] float jumpSpeed = 3.0f;
    float jumpDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        jumpDirection = Mathf.Sign(endPoint.position.x - transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AI_2D ai = other.gameObject.GetComponent<AI_2D>();
        if (ai != null)
        {
            float dir = Mathf.Sign(transform.position.x - ai.transform.position.x);
            if (dir == jumpDirection)
            {
                ai.DoJump(endPoint.position.y - other.transform.position.y, endPoint.position.x - other.transform.position.x);
                //ai.DoJump2(endPoint.position.y - other.transform.position.y, endPoint.position.x - other.transform.position.x, jumpSpeed);
            }
        }
    }
}

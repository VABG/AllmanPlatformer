using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController p = collision.rigidbody.GetComponent<PlayerController>();
        p.TakeDamage(50);
        p.Bounce(Vector3.Normalize(p.transform.position - transform.position) * 3);
    }
}

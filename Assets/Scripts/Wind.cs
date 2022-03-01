using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] Vector3 windStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        other.attachedRigidbody.AddForce(windStrength * Time.fixedDeltaTime);
    }
}

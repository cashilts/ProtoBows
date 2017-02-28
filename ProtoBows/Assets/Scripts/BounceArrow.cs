using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArrow : GenericArrow {

    bool bounced = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isStick = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<GenericObject>().isStick)
        {
            if (!bounced)
            {
                Vector3 impulse = collision.impulse;
                rb.AddForce(impulse, ForceMode.Impulse);
                bounced = true;
            }
            else
            {
                rb.isKinematic = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArrow : GenericArrow {

    bool bounced = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isStick = false;
        decayTime = (int)(decayTime*0.75f);
    }

    public override void Update()
    {
        if (bounced) {
            decayTime--;
            if (decayTime == 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<GenericObject>().isStick)
        {
            rb.freezeRotation = true;
            gameObject.transform.up = collision.impulse;
            Vector3 impulse = collision.impulse * 2;
            rb.AddForce(impulse, ForceMode.Impulse);
            bounced = true;
        }
    }
}

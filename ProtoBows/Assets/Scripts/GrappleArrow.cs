using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArrow : GenericArrow {

    GameObject player;
    const int pullFactor = 60;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isStick = false;
        player = GameObject.Find("Player");
        triggerable = true;
        decayTime = -1;
    }

    

    public override void Trigger()
    {
        if (launched && Input.GetAxisRaw("Fire") !=0) {
            Rigidbody playerBody = player.GetComponent<Rigidbody>();
            Vector3 pullVector = transform.position - player.transform.position;
            pullVector.Normalize();
            pullVector *= pullFactor; 
            playerBody.AddForce(pullVector, ForceMode.Force);
            player.GetComponent<Player>().grappled = true;
            base.Trigger();
        }
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<GenericObject>().isStick)
        {
            rb.isKinematic = true;
        }
    }

}

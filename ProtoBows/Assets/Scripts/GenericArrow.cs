using UnityEngine;
using System.Collections;

public class GenericArrow : GenericObject {

    protected Rigidbody rb;
    bool launched = false;
    int decayTime = 100;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isStick = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.isKinematic) {
            decayTime--;
            if (decayTime == 0) {
                GameObject.Destroy(gameObject);
            }
        }
	}

    public void Launch(Vector3 force) {
        rb.AddForce(force, ForceMode.Impulse);
        launched = true;
    }

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.GetComponent<GenericObject>().isStick)
        {
            rb.isKinematic = true;
        }
    }
}

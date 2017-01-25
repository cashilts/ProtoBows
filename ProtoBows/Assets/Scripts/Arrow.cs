using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    Rigidbody rb;
    bool launched = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Launch(Vector3 force) {
        rb.AddForce(force, ForceMode.Impulse);
        launched = true;
    }

    void OnCollisionEnter(Collision collision) {
        if (name != "Player")
        {
            rb.isKinematic = true;
        }
    }
}

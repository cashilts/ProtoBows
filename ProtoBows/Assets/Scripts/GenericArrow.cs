using UnityEngine;
using System.Collections;

public class GenericArrow : GenericObject {

    protected Rigidbody rb;
    public bool triggerable = false;
    public bool launched = false;
    protected int decayTime = 100;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        isStick = false;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        
        if (rb.isKinematic) {
            if (!triggerable)
            {
                decayTime--;
                if (decayTime == 0)
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }
	}

    public virtual void Trigger() { triggerable = false; }

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

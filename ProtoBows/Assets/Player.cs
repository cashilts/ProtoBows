using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    private int jumpFrames = 0;
    private const int jumpMax = 5;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 heading = transform.forward;
            heading *= .1f;
            transform.Translate(heading, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Vector3 heading = transform.forward;
            heading *= -.1f;
            transform.Translate(heading, Space.World);
        }
       
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(transform.up, -1f);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(transform.up, 1f);
        }
        if (Input.GetKey(KeyCode.Space)) {
            if (jumpFrames != jumpMax)
            {
                jumpFrames++;
                rb.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
            }
        }

    }

    void OnCollisionEnter(Collision collision) {
        
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "Cube") {
            jumpFrames = 0;
        }
    }
}

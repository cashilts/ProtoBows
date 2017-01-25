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
            heading *= 0.1f;
            transform.Translate(heading);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Vector3 heading = transform.forward;
            heading *= -0.1f;
            transform.Translate(heading);
        }
       
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 left = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
            left *= 0.1f;
            transform.Translate(left);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            Vector3 left = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
            left *= -0.1f;
            transform.Translate(left);
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

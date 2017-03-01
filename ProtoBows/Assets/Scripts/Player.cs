using UnityEngine;
using System.Collections;

public class Player : GenericObject
{
    Rigidbody rb;
    private int jumpFrames = 0;
    private const int jumpMax = 5;
    private bool arrowReady = false;
    public bool grappled = false;
    GameObject newArrow;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isStick = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Vector3 heading = transform.forward;
            heading *= .1f * Input.GetAxisRaw("Vertical");
            transform.Translate(heading, Space.World);
            if (grappled) {
                grappled = false;
                rb.velocity = Vector3.zero;
                rb.useGravity = true;
            }
        }   
        if (Input.GetAxisRaw("Horizontal") != 0){
            transform.Rotate(transform.up, 1f * Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetAxisRaw("Jump") != 0) {
            if (jumpFrames != jumpMax)
            {
                jumpFrames++;
                rb.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
                rb.useGravity = true;
                if (grappled)
                {
                    grappled = false;
                    rb.velocity = Vector3.zero;
                    rb.useGravity = true;
                }
            }
        }
        if (Input.GetAxisRaw("Mouse X") != 0) {
            Vector3 heading = Camera.main.transform.forward;
            heading = Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X")*2, Vector3.up) * heading;
            float lookAngle = Vector3.Angle(transform.forward, heading);
            if (lookAngle < 70)
            {
                Camera.main.transform.forward = heading;
            }
        }
        if (Input.GetAxisRaw("RHorizontal") != 0)
        {
            Vector3 heading = Camera.main.transform.forward;
            heading = Quaternion.AngleAxis(Input.GetAxisRaw("RHorizontal") * 1, Vector3.up) * heading;
            float lookAngle = Vector3.Angle(transform.forward, heading);
            if (lookAngle < 70)
            {
                Camera.main.transform.forward = heading;
            }
        }
        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            Vector3 heading = Camera.main.transform.forward;
            Vector3 left = Vector3.Cross(heading.normalized, Vector3.up.normalized);
            heading = Quaternion.AngleAxis(Input.GetAxisRaw("Mouse Y") * 2, left) * heading;
            float lookAngle = Vector3.Angle(transform.forward, heading);
            if (lookAngle < 40)
            {
                Camera.main.transform.forward = heading;
            }
        }
        if (Input.GetAxisRaw("RVertical") != 0)
        {
            Vector3 heading = Camera.main.transform.forward;
            Vector3 left = Vector3.Cross(heading.normalized, Vector3.up.normalized);
            heading = Quaternion.AngleAxis(Input.GetAxisRaw("RVertical") * 1, left) * heading;
            float lookAngle = Vector3.Angle(transform.forward, heading);
            if (lookAngle < 40)
            {
                Camera.main.transform.forward = heading;
            }
        }
        if (Input.GetAxisRaw("Fire") == 1 || Input.GetAxisRaw("Fire") == -1)
        {
            if (!arrowReady)
            {
                newArrow = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GrappleArrow"));
                newArrow.transform.up = Camera.main.transform.forward;
                newArrow.transform.position = Camera.main.transform.position;
                newArrow.transform.Translate(transform.forward * 1, Space.World);
                arrowReady = true;
            }
            else if (!newArrow.GetComponent<GenericArrow>().launched)
            {
                newArrow.transform.up = Camera.main.transform.forward;
                newArrow.transform.position = Camera.main.transform.position;
                newArrow.transform.Translate(transform.forward * 1, Space.World);
            }
            else {
                Debug.Log("Trigger");
                newArrow.GetComponent<GenericArrow>().Trigger();
            }

        }
        else {
            if (arrowReady && !newArrow.GetComponent<GenericArrow>().launched)
            {
                newArrow.GetComponent<GenericArrow>().Launch(newArrow.transform.up * 15f);
                if (!newArrow.GetComponent<GenericArrow>().triggerable)
                {
                    arrowReady = false;
                }
            }
            else if (arrowReady && !newArrow.GetComponent<GenericArrow>().triggerable) {
                arrowReady = false;
            }
        }

    }

    void OnTriggerEnter(Collider collision) {
        if (collision.name == "Cube")
        {
            jumpFrames = 0;
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = 0;
            rb.velocity = newVelocity;
            rb.useGravity = false;
        }
        else if (collision.name.Contains("Grapple")) {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}

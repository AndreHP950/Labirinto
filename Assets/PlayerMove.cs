using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float forceAmount = 1000f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))    
            rb.AddForce(Vector3.forward * forceAmount * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back * forceAmount * Time.deltaTime);
        else if(Input.GetKey(KeyCode.D)) 
            rb.AddForce(Vector3.right * forceAmount * Time.deltaTime);
        else if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * forceAmount * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hockey : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float speed = 6;

    [SerializeField]
    float LimitSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > LimitSpeed)
        {
            rb.velocity = rb.velocity.normalized * LimitSpeed;         
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            //Debug.Log(rb.velocity.magnitude);
            rb.velocity = rb.velocity * 1.13f;
        }
    }
}

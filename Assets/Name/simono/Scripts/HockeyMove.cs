using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HockeyMove : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float speed = 6.0f;

    [SerializeField]
    private float LimitSpeed = 10.0f;

    static string tagWall = "Wall";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //ホッケーの速度が上限を超えているなら、速度を上限まで下げる
        if (rb.velocity.magnitude > LimitSpeed)
        {
            rb.velocity = rb.velocity.normalized * LimitSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == tagWall)
        {
            //壁反射SEとか
            Debug.Log("反射");
            
        }
    }

    public void AddForce(Vector3 direction, float pow)
    {
        rb.AddForceAtPosition(direction * pow, transform.position, ForceMode.Impulse);
    }
}
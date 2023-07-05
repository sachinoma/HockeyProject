
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

    [SerializeField]
    private GameObject HitEffect;

    static string tagWall = "Wall";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //�z�b�P�[�̑��x������𒴂��Ă���Ȃ�A���x������܂ŉ�����
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
            //�ǔ���SE�Ƃ�
            Debug.Log("����");
            Instantiate(HitEffect,transform.position,transform.rotation);
        }
    }

    public void AddForce(Vector3 direction, float pow)
    {
        rb.AddForceAtPosition(direction * pow, transform.position, ForceMode.Impulse);
    }
}
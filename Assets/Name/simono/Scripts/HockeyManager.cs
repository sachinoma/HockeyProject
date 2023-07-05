using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HockeyManager : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float speed = 6.0f;

    [SerializeField]
    float LimitSpeed = 10.0f;

    static string tagWall = "Wall";

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
            
        }
    }
}
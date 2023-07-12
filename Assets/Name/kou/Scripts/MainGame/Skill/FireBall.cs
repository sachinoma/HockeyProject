using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FireBall : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 moveVec;
    [SerializeField]
    float fireBallPower = 300;

    [SerializeField]
    private float speed = 6.0f;

    [SerializeField]
    private float LimitSpeed = 10.0f;

    [SerializeField]
    float lifeTime = 2.0f;

    [SerializeField]
    private GameObject HitEffect;
    [SerializeField]
    private GameObject hitParticle;

    static string tagWall = "Wall";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AddForce();
        Invoke("Destroy", lifeTime);
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
        if (tag == tagWall)
        {
            //�ǔ���SE�Ƃ�
            Debug.Log("����");
            Instantiate(HitEffect, transform.position, transform.rotation);
        }
    }

    public void AddForce()
    {
        rb.AddForceAtPosition(moveVec * fireBallPower, transform.position, ForceMode.Impulse);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hockey")
        {
            Debug.Log("TriggerEnter");
            Rigidbody rigidbody = other.attachedRigidbody;
            if (rigidbody != null)
            {
                //���͕��������3D�x�N�g��
                //Vector3 forceDirection = forceVec;

                //====================================================================================================
                //���ꂩ��z�b�P�[�ɓn�������l
                //forceDirection�͗͂̕���(����AttackTrigger�̑O���x�N�g��)
                Vector3 forceDirection = rb.velocity.normalized;

                //�z�b�P�[�ɓn���͂̑傫��
                float power = fireBallPower;
                //====================================================================================================

                //�z�b�P�[�ɗ͂�^����i���̏����͂��ꂩ��z�b�P�[���ł�������������j
                Debug.Log(other.name);
                other.transform.parent.GetComponent<HockeyMove>()?.AddForce(forceDirection, power);

                // �Փˈʒu���擾����
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(hitParticle, hitPos, Quaternion.identity);
            }
        }
    }
}

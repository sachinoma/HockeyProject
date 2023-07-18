using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    private AudioClip land;

    [SerializeField] private Vector3 localGravity;
    private Rigidbody rBody;

    [SerializeField] private GameObject LandEffect;
    private GameObject createObject;

    [SerializeField] private float lifeTime = 6.0f;

    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //�ŏ���rigidBody�̏d�͂��g��Ȃ�����
        Invoke(nameof(Destroy), lifeTime);
    }

    private void FixedUpdate()
    {
        SetLocalGravity(); //�d�͂�AddForce�ł����郁�\�b�h���ĂԁBFixedUpdate���D�܂����B
    }

    private void SetLocalGravity()
    {
        rBody.AddForce(localGravity, ForceMode.Acceleration);
    }

    private void Destroy()
    {
        Destroy(createObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            audioSource.PlayOneShot(land, 1.0f);
            createObject = Instantiate(LandEffect, transform.position, transform.rotation);
        }
    }
}

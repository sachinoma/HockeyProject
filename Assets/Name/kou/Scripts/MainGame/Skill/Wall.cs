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
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする
        Invoke(nameof(Destroy), lifeTime);
    }

    private void FixedUpdate()
    {
        SetLocalGravity(); //重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
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

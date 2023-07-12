using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Vector3 localGravity;
    private Rigidbody rBody;

    // Use this for initialization
    private void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする
        Invoke(nameof(Destroy), 8.0f);
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
        Destroy(gameObject);
    }
}

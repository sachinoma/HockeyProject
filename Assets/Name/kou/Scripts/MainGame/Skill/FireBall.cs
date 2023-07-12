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
        Invoke(nameof(Destroy), lifeTime);
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
        if (tag == tagWall)
        {
            //壁反射SEとか
            Debug.Log("反射");
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
                //入力方向からの3Dベクトル
                //Vector3 forceDirection = forceVec;

                //====================================================================================================
                //これからホッケーに渡したい値
                //forceDirectionは力の方向(今はAttackTriggerの前方ベクトル)
                Vector3 forceDirection = rb.velocity.normalized;

                //ホッケーに渡す力の大きさ
                float power = fireBallPower;
                //====================================================================================================

                //ホッケーに力を与える（この処理はこれからホッケー内でやった方がいい）
                Debug.Log(other.name);
                other.transform.parent.GetComponent<HockeyMove>()?.AddForce(forceDirection, power);

                // 衝突位置を取得する
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(hitParticle, hitPos, Quaternion.identity);
            }
        }
    }
}

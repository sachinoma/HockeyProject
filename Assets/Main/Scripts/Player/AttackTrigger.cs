using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude = 10.0f;

    [SerializeField]
    private GameObject hitParticle;

    private Vector2 inputVec;
    private Vector3 forceVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInputVec(Vector2 vec)
    {
        inputVec = vec;
        forceVec.x = -inputVec.x;
        forceVec.z = -inputVec.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);

            // è’ìÀà íuÇéÊìæÇ∑ÇÈ
            Vector3 hitPos = collision.contacts[0].point;

            Instantiate(hitParticle, hitPos, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hockey")
        {
            Debug.Log("TriggerEnter");
            Rigidbody rigidbody = other.attachedRigidbody;
            if (rigidbody != null)
            {
                //Vector3 forceDirection = other.transform.position - transform.position;
                Vector3 forceDirection = forceVec;
                //forceDirection.y = 0f;
                //forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, other.gameObject.transform.position, ForceMode.Impulse);

                // è’ìÀà íuÇéÊìæÇ∑ÇÈ
                Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                Instantiate(hitParticle, hitPos, Quaternion.identity);
            }
        }
    }
}

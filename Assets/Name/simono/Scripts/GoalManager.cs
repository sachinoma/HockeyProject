using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private bool isLeft;

    static string tagHockey = "Hockey";

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == tagHockey)
        {
            Debug.Log("ÉSÅ[Éã");
            gameManager.ScorePlus(isLeft, 1);
            Destroy(collision.gameObject);
        }
    }
}

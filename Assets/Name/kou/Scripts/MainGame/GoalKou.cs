using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKou : MonoBehaviour
{
    private GameManagerKou gameManager;

    [SerializeField]
    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hockey")
        {
            gameManager.ScorePlus(isLeft, 1);
            Destroy(collision.gameObject);
        }
    }
}

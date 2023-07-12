using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private GameManagerKou gameManager;

    private bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("IdleBlend", Random.Range(0.0f, 1.0f));
        DicedeIdle();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGoal)
        {
            animator.SetInteger("GoalMotion", Random.Range(0, 3));
            animator.SetBool("isGoal", true);
            isIdle = false;
        }
        else
        {           
            animator.SetBool("isGoal", false);
            DicedeIdle();
        }
    }

    private void DicedeIdle()
    {
        if(isIdle == false)
        {
            animator.SetFloat("IdleBlend", Random.Range(0.0f, 1.0f));
            isIdle = true;
        }
    }
}

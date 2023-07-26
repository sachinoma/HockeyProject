using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleHuman : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private CharacterController controller;

    [SerializeField]
    private float playerSpeed = 2.0f; //ˆÚ“®‘¬“x


    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Start()
    {
        animator.SetInteger("WalkNum",Random.Range(0,5));
        animator.SetBool("ChangeWalk", true);
        Invoke(nameof(DestroyObject), 20.0f);
    }
    void Update()
    {
        controller.Move(transform.forward * Time.deltaTime * playerSpeed);

        if(transform.position.x > 6.0f || transform.position.x < -6.0f)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}

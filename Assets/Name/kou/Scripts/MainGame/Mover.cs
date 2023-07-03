using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private CharacterController controller;
    
    private bool groundedPlayer; //接地フラグ
    [SerializeField]
    private float playerSpeed = 2.0f; //移動速度
    [SerializeField]
    private float jumpHeight = 1.0f; //ジャンプ力
    [SerializeField]
    private float gravityValue = -9.81f; //重力

    private Vector3 moveDirection = Vector2.zero; //移動方向
    private Vector2 inputVector = Vector2.zero; //入力方向
    private bool attacked = false; //攻撃フラグ

    [SerializeField]
    private float forceMagnitude = 10.0f; //攻撃の力加減

    [SerializeField]
    private GameObject attackTrigger; //攻撃判定トリガー


    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {     
        attackTrigger = transform.Find("AttackTrigger").gameObject;
        attackTrigger.SetActive(false);
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    //入力を感知してからの移動処理
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //もし移動ベクトルが0の場合戻す（入力していない）
        if(inputVector == Vector2.zero) 
        {
            return;
        }
        float angle = Vector2ToAngle(inputVector);
        transform.rotation = Quaternion.Euler(0, -angle - 90, 0);
        animator.SetBool("isRun", true);
    }

    //入力を感知してからのAttack処理
    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
        animator.SetBool("isAttack", attacked);
    }

    void Update()
    {
        //プレイヤーが地面にいるかどうか
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
        }

        //入力方向を基にmoveベクトルを作る
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        //デバッグのジャンプ処理
        /*
        if (attacked && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */

        //移動する
        moveDirection.y += gravityValue * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public static float Vector2ToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    //AnimationEventで呼ぶ　AttackTriggerを有効にする
    public void AttackStart()
    {
        attackTrigger.SetActive(true);
    }
    //AnimationEventで呼ぶ　AttackTriggerを無効にする
    public void AttackEnd()
    {
        attackTrigger.SetActive(false);
    }

    //プレイヤー自身の衝突判定
    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if(rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }


}

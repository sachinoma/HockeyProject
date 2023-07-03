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
    
    private bool groundedPlayer; //�ڒn�t���O
    [SerializeField]
    private float playerSpeed = 2.0f; //�ړ����x
    [SerializeField]
    private float jumpHeight = 1.0f; //�W�����v��
    [SerializeField]
    private float gravityValue = -9.81f; //�d��

    private Vector3 moveDirection = Vector2.zero; //�ړ�����
    private Vector2 inputVector = Vector2.zero; //���͕���
    private bool attacked = false; //�U���t���O

    [SerializeField]
    private float forceMagnitude = 10.0f; //�U���̗͉���

    [SerializeField]
    private GameObject attackTrigger; //�U������g���K�[


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

    //���͂����m���Ă���̈ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //�����ړ��x�N�g����0�̏ꍇ�߂��i���͂��Ă��Ȃ��j
        if(inputVector == Vector2.zero) 
        {
            return;
        }
        float angle = Vector2ToAngle(inputVector);
        transform.rotation = Quaternion.Euler(0, -angle - 90, 0);
        animator.SetBool("isRun", true);
    }

    //���͂����m���Ă����Attack����
    public void OnAttack(InputAction.CallbackContext context, float power)
    {
        attackTrigger.GetComponent<AttackTrigger>().ChangeForce(power);
        attacked = context.action.triggered;
        animator.SetBool("isAttack", attacked);
    }

    void Update()
    {
        //�v���C���[���n�ʂɂ��邩�ǂ���
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
        }

        //���͕��������move�x�N�g�������
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        //�f�o�b�O�̃W�����v����
        /*
        if (attacked && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */

        //�ړ�����
        moveDirection.y += gravityValue * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public static float Vector2ToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    //AnimationEvent�ŌĂԁ@AttackTrigger��L���ɂ���
    public void AttackStart()
    {
        attackTrigger.SetActive(true);
    }
    //AnimationEvent�ŌĂԁ@AttackTrigger�𖳌��ɂ���
    public void AttackEnd()
    {
        attackTrigger.SetActive(false);
    }

    //�v���C���[���g�̏Փ˔���
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

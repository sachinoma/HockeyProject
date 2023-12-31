using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private Mover mover;
    private GameManagerKou gameManager;

    [SerializeField]
    private SkinnedMeshRenderer playerMesh;
    private PlayerController controls;

    [SerializeField]
    private int hitPower;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        controls = new PlayerController();
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
    }

    //PlayerConfigsから貰った設定でplayerを設定する
    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        if(pc.PlayerMaterial != null) 
        {
            playerMesh.material = pc.PlayerMaterial;
        }
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    //Inputイベント
    private void Input_onActionTriggered(CallbackContext context)
    {
        if(gameManager.GetIsStartCountDown() == true) { return; }

        if(context.action.name == controls.Player.Movement.name)
        {
            OnMove(context);
        }
        if (context.action.name == controls.Player.Attack.name)
        {
            OnAttack(context, hitPower);
        }
        if(context.action.name == controls.Player.Skill1.name)
        {
            OnSkill(context, 0);
        }
        if(context.action.name == controls.Player.Skill2.name)
        {
            OnAttack(context, hitPower);
        }
        if(context.action.name == controls.Player.Skill3.name)
        {
            OnAttack(context, hitPower);
        }
    }

    //移動中
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
        {
            mover.OnMove(context);
        }
    }
    //Attack中
    public void OnAttack(CallbackContext context, float power)
    {
        if (mover != null)
        {
            mover.OnAttack(context, power);
        }
    }

    private void OnSkill(CallbackContext context, int num)
    {
        mover.OnSkill(context, num);
    }
}

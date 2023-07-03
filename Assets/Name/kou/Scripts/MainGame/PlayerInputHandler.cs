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

    [SerializeField]
    private SkinnedMeshRenderer playerMesh;
    private PlayerController controls;

    [SerializeField]
    private int normalPower;
    [SerializeField]
    private int bigPower;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        controls = new PlayerController();
    }

    //PlayerConfigsÇ©ÇÁñ·Ç¡ÇΩê›íËÇ≈playerÇê›íËÇ∑ÇÈ
    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        if(pc.PlayerMaterial != null) 
        {
            playerMesh.material = pc.PlayerMaterial;
        }
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    //InputÉCÉxÉìÉg
    private void Input_onActionTriggered(CallbackContext context)
    {
        if(context.action.name == controls.Player.Movement.name)
        {
            OnMove(context);
        }
        if (context.action.name == controls.Player.Attack.name)
        {
            OnAttack(context, normalPower);
        }
        if(context.action.name == controls.Player.Skill1.name)
        {
            OnAttack(context, bigPower);
        }
        if(context.action.name == controls.Player.Skill2.name)
        {
            OnAttack(context, bigPower);
        }
        if(context.action.name == controls.Player.Skill3.name)
        {
            OnAttack(context, bigPower);
        }
    }

    //à⁄ìÆíÜ
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
        {
            mover.OnMove(context);
        }
    }
    //AttackíÜ
    public void OnAttack(CallbackContext context, float power)
    {
        if (mover != null)
        {
            mover.OnAttack(context, power);
        }
    }
}

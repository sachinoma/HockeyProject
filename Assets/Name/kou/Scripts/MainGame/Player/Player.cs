using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public virtual void SetSkill1(GameObject atk, GameObject ball)
    {
    }
    public virtual void Skill1()
    {
        Debug.Log("í èÌçUåÇ");
    }
}
public class PlayerType0 : Player
{
    private GameObject attackTrigger;
    private GameObject fireBall;

    public override void SetSkill1(GameObject atk,GameObject ball)
    {
        attackTrigger = atk;
        fireBall = ball;
    }

    public override void Skill1()
    {
        Vector3 forceDirection = attackTrigger.transform.forward;
        GameObject ball = Instantiate(fireBall, transform.position, transform.rotation);
        ball.GetComponent<FireBall>().moveVec = forceDirection;
    }
}
public class PlayerType1 : Player
{
    public override void Skill1()
    {
        Debug.Log("Player1ÇÃçUåÇ");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public virtual void SetSkill1(GameObject atk, GameObject obj)
    {
    }
    public virtual void Skill1(Transform trans)
    {
        Debug.Log("í èÌçUåÇ");
    }
}
public class PlayerType0 : Player
{
    private GameObject attackTrigger;
    private GameObject fireBall;

    public override void SetSkill1(GameObject atk,GameObject obj)
    {
        attackTrigger = atk;
        fireBall = obj;
    }

    public override void Skill1(Transform trans)
    {
        Debug.Log("Player0ÇÃçUåÇ");
        Vector3 forceDirection = attackTrigger.transform.forward;
        GameObject ball = Instantiate(fireBall, trans.position, trans.rotation);
        ball.GetComponent<FireBall>().moveVec = forceDirection;
    }
}
public class PlayerType1 : Player
{
    private GameObject attackTrigger;
    private GameObject wallObj;
    public override void SetSkill1(GameObject atk, GameObject obj)
    {
        attackTrigger = atk;
        wallObj = obj;
    }
    public override void Skill1(Transform trans)
    {
        Debug.Log("Player1ÇÃçUåÇ");
        Vector3 pos = trans.position + trans.forward * 2;
        pos.y += 5;

        Vector3 eulerAngles = trans.rotation.eulerAngles;
        eulerAngles.y += 90;
        Quaternion rot = Quaternion.Euler(eulerAngles);

        Vector3 forceDirection = attackTrigger.transform.forward;
        GameObject wall = Instantiate(wallObj, pos, rot);
    }
}
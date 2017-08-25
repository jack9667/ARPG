using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private float viewAngle = 180; //攻击视野
    private Transform player;
    public float attackDistance = 3;    //攻击距离
    public float moveSpeed = 1.5f;
    public float timeInterval = 1;  //攻击间隔
    private float timer = 0;
    private bool isAttacking = false;

    public float[] attackArray;
    private GameObject attacke01GameObject;

    void Awake()
    {
        player = TranscriptManager.instance.player.transform;
        attacke01GameObject = transform.Find("Bip001 Weapon/attack01").gameObject;
    }

    void Update()
    {
        if (isAttacking) return;
        Vector3 playPos = player.position;
        playPos.y = transform.position.y;   //保持y轴一致
        float angle = Vector3.Angle(playPos - transform.position, transform.forward);
        if (angle < viewAngle / 2)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playPos - transform.position);  //控制下朝向
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
            //在攻击视野之内
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < attackDistance)
            {
                //攻击
                if (!isAttacking)
                {
                    GetComponent<Animation>().Play("idle");
                    timer += Time.deltaTime;
                    if (timer > timeInterval)
                    {
                        Attack();
                        timer = 0;
                    }
                }
            }
            else
            {
                isAttacking = false;
                //追击
                GetComponent<Animation>().Play("walk");
                GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            //在攻击视野之外 进行
            //Quaternion targetRotation = Quaternion.LookRotation(playPos - transform.position);  //控制下朝向
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            GetComponent<Animation>().Play("idle");
            //GetComponent<Animation>().Play("walk");
            //transform.LookAt(playPos);
        }
    }

    private int attackIndex = 0;
    void Attack()
    {
        isAttacking = true;
        attackIndex++;
        if (attackIndex == 4)
            attackIndex = 1;
        GetComponent<Animation>().CrossFade("attack0" + attackIndex);

    }

    void BackToStand()
    {
        isAttacking = false;
    }

    void PlayAttack01Effect()
    {
        attacke01GameObject.SetActive(true);
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", attackArray[0]);
            //GetComponent<Rigidbody>().AddForce()
        }
    }

}

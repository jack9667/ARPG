using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject damageEffectPrefab;   //出血特效
    public int hp = 200;
    private int hpTotal;
    private Transform bloodPoint;


    public float speed = 2.0f;
    private CharacterController cc;

    private int attackDamage = 20;  //攻击力
    private float attackRate = 2;//攻击速率
    private float attackTimer = 0;
    private float attackDistance = 1.5f;
    private float distance;
    private float downDistance;

    //血量和受到攻击数字
    private GameObject hpBarObj;
    private UISlider hpBarSlider;
    private GameObject hudTextObj;
    private HUDText hudText;
    void Start()
    {
        TranscriptManager.instance.enemyList.Add(this.gameObject);
        hpTotal = hp;
        bloodPoint = this.transform.Find("BloodPoint").GetComponent<Transform>();
        cc = this.transform.GetComponent<CharacterController>();
        InvokeRepeating("CaleDistance", 0,0.1f);    //一秒调用10次
        //生成血条
        Transform hpPoint = transform.Find("HpBarPoint");
        hpBarObj= HpBarManager.instance.GetHpBar(hpPoint.gameObject);
        hpBarSlider = hpBarObj.transform.Find("Bg").GetComponent<UISlider>();
        hudTextObj = HpBarManager.instance.GetHudText(hpPoint.gameObject);
        hudText = hudTextObj.GetComponent<HUDText>();
        
    }

    void Update()
    {
        if (hp <= 0)
        {
            //移到地下
            downDistance += 0.5f * Time.deltaTime;
            transform.Translate(-transform.up * 0.5f * Time.deltaTime);
            if (downDistance >= 1.0f)
            {
                hpBarObj.SetActive(false);
                gameObject.SetActive(false);
                hudTextObj.SetActive(false);
            }
            return;
        } 
        if (distance < attackDistance)
        {
            //攻击
            //attackTimer += Time.deltaTime;
            //if (attackTimer > attackRate)
            //{
            //jing xing gong ji
            //Transform player = TranscriptManager.instance.player.transform;
            //Vector3 targetPos = player.position;
            //targetPos.y = transform.position.y;
            //transform.LookAt(targetPos);
            GetComponent<Animation>().Play("attack01");
            //attackTimer = 0;
            //}
            //if (!GetComponent<Animation>().IsPlaying("attack01"))
            //{
            //    GetComponent<Animation>().Play("idle");
            //}
        }
        else
        {
            Move(); 
        }
        //GetComponent<Animation>().Play("takedamage");

    }

    void Move()
    {
        GetComponent<Animation>().Play("walk");
        Transform player = TranscriptManager.instance.player.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward * speed);
    }

    //0,收到多少伤害
    //1,后退距离
    //2.浮空高度
	void TakeDamage(string args)   //from playerattack
    {
        if (hp <= 0)
        {
            return;
        }
        //显示连击
        Combo.instance.ComboPlus();
        string[] proArray = args.Split(',');
        //减去伤害值
        int damage =int.Parse(proArray[0]);
        hp -= damage;
        //ui bar的更新
        hpBarSlider.value = (float)hp / hpTotal;
        hudText.Add("-" + damage, Color.black, 0.1f);
        if (hp <= 0)
        {
            //si le
            Dead();
        }
        //受到攻击
        //transform.GetComponent<Animation>().Play("takedamage");
        SoundManager.instance.Play("Hurt");
        //浮空和后退
        float backDistance = float.Parse(proArray[1]);
        float jumpDistance = float.Parse(proArray[2]);
        
        iTween.MoveBy(this.gameObject,
                      transform.InverseTransformDirection(TranscriptManager.instance.player.transform.forward) * backDistance + Vector3.up * jumpDistance,
                      0.3f);//把人物前方向转换为怪物局部方向
        //transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z-)
        //transform.position -= Vector3.up * jumpDistance*2;
        //出血特效
        GameObject.Instantiate(damageEffectPrefab, bloodPoint.transform.position, Quaternion.identity);

    }

    void Dead()
    {
        this.GetComponent<CharacterController>().enabled = false;
        int random = Random.Range(0, 10);
        if (random < 7)
        {
            //1 播放动画死亡
            GetComponent<Animation>().Play("die");
        }
        else
        {
            //2 破碎死亡
            this.GetComponentInChildren<MeshExploder>().Explode();
            this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            this.gameObject.SetActive(false);
            hpBarObj.SetActive(false);
            hudTextObj.SetActive(false);
        }

    }

    void CaleDistance()
    {
        Transform player = TranscriptManager.instance.player.transform;
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    /// <summary>
    /// 敌人攻击时调用方法
    /// </summary>
    void Attack()
    {
        Transform player = TranscriptManager.instance.player.transform;
        float distance = Vector3.Distance(transform.position,player.transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage",attackDamage,SendMessageOptions.DontRequireReceiver);
        }
    }
}

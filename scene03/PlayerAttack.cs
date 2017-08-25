using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {


    private Dictionary<string, PlayerEffecrt> effectDict = new Dictionary<string, PlayerEffecrt>();
    public PlayerEffecrt[] effectArray;

    private Animator ator;

    private int hp = 100;

    public enum AttackRange
    {
        Forward,
        Around,
        NearAround
    }
    public float distanceAttackForward = 2.0f;
    public float distanceAttackAround = 60.0f;
    public float distanceAttackNear = 10.0f;

    //伤害值
    public int[] damageArray = new int[4];
    //xue tiao
    private GameObject hudTextObj;
    private HUDText hudText;
    private Transform danmagePoint;

    private Transform hpPoint;
    private GameObject hudHpObj;
    private UISlider hudHpSlider;

    void Start()
    {
        PlayerEffecrt[] peAarray = this.GetComponentsInChildren<PlayerEffecrt>();
        foreach (PlayerEffecrt p in peAarray)    //存储攻击特效
        {
            effectDict.Add(p.gameObject.name, p);
        }
        foreach (PlayerEffecrt p in effectArray) //恶魔之手
        {
            effectDict.Add(p.gameObject.name, p);
        }
        ator = transform.GetComponent<Animator>();
        danmagePoint = transform.Find("DamagePoint");
        hudTextObj = HpBarManager.instance.GetHudText(danmagePoint.gameObject);
        hudText = hudTextObj.GetComponent<HUDText>();

        hpPoint = transform.Find("HpPoint");
        hudHpObj = HpBarManager.instance.GetHpBar(hpPoint.gameObject);
        hudHpSlider = hudHpObj.transform.Find("Bg").GetComponent<UISlider>();

    }

    //0 normal skill1 skill2 skill3
    //1 effect name
    //2 sound name
    //3 move forward    敌人和player都动
    //4 jump height
    void Attack(string args)
    {

        string[] proArray = args.Split(',');
        //0 wei zhi
        string posType = proArray[0];
        //1 show effect
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);
        //2 play sound
        string soundName = proArray[2];
        SoundManager.instance.Play(soundName);
        //3 move forward
        float moveFoward = float.Parse(proArray[3]);
        //4 jump height
        float jumpHeight = float.Parse(proArray[4]);
        if (moveFoward > 0.1f)
        {
            iTween.MoveBy(this.gameObject, Vector3.forward * moveFoward, 0.3f); //前移
            //this.gameObject.transform.position += transform.forward* moveFoward;
            //transform.forward 物体自身的z轴
            //Vector3.forward 0，0，1
        }
        if (posType == "normal")
        {
            ArrayList arrayList = GetEnemyFront(AttackRange.Forward);
            foreach (GameObject g in arrayList)
            {
                if (!g.activeInHierarchy) continue;
                g.SendMessage("TakeDamage", damageArray[0] + "," + proArray[3] + "," + proArray[4]);    //todo  to enemy
            }
        }


    }

    //0 normal skill1 skill2 skill3
    //1 move forward
    //2 jum height
    void SkillAttack(string args)
    {
        string[] proArray = args.Split(',');
        //0 wei zhi
        string posType = proArray[0];

        if (posType == "skill1")
        {
            ArrayList arrayList = GetEnemyFront(AttackRange.Forward);
            foreach (GameObject g in arrayList)
            {
                g.SendMessage("TakeDamage", damageArray[1] + "," + proArray[1] + "," + proArray[2]);    //todo  to enemy

            }
        }
        if (posType == "skill2")
        {
            ArrayList arrayList = GetEnemyFront(AttackRange.Around);
            foreach (GameObject g in arrayList)
            {
                if (g.activeInHierarchy == false) continue;
                g.SendMessage("TakeDamage", damageArray[2] + "," + proArray[1] + "," + proArray[2]);    //todo  to enemy

            }
        }
        if (posType == "skill3")
        {
            ArrayList arrayList = GetEnemyFront(AttackRange.NearAround);
            foreach (GameObject g in arrayList)
            {
                if (!g.activeInHierarchy) continue;
                g.SendMessage("TakeDamage", damageArray[3] + "," + proArray[1] + "," + proArray[2]);    //todo  to enemy
            }
        }
    }

    void ShowPlayerEffect(string effectName)
    {
        if (effectName == "") return;
        PlayerEffecrt pe;
        if (effectDict.TryGetValue(effectName, out pe))
            pe.Show();
    }
    /// <summary>
    /// 判断前面的敌人 得到arrayList
    /// </summary>
    /// <returns></returns>
    public ArrayList GetEnemyFront(AttackRange atcRange)
    {
        ArrayList arrayList = new ArrayList();
        if (atcRange == AttackRange.Forward)//是向前攻击
        {
            foreach (var g in TranscriptManager.instance.enemyList)
            {
                Vector3 pos = transform.InverseTransformPoint(g.transform.position);//把怪物的坐标转化为人物的局部坐标
                if (pos.z > -0.5f)  //正向前方
                {
                    float distance = Vector3.Distance(Vector3.zero, pos);   //敌人和人物的距离
                    if (distance < distanceAttackForward)
                    {
                        arrayList.Add(g);   //添加的是gamobjct
                    }
                }
            }
        }
        else if (atcRange == AttackRange.Around)
        {
            foreach (var g in TranscriptManager.instance.enemyList)
            {
                float distance = Vector3.Distance(transform.position, g.transform.position);   //敌人和人物的距离
                if (distance < distanceAttackAround)
                {
                    arrayList.Add(g);
                }
            }
        }
        else
        {
            foreach (var g in TranscriptManager.instance.enemyList)
            {
                float distance = Vector3.Distance(transform.position, g.transform.position);   //敌人和人物的距离
                if (distance < distanceAttackNear)
                {
                    arrayList.Add(g);
                }
            }
        }

        return arrayList;
    }
    //展示手的特效
    void ShowEffectHanf()
    {
        string effectName = "DevilHandMobile";
        PlayerEffecrt pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {
            ArrayList array = GetEnemyFront(AttackRange.Forward);
            foreach (GameObject go in array)
            {
                if (!go.activeInHierarchy) continue;
                //射线检测敌人地面的点
                RaycastHit hit;
                bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 2, LayerMask.GetMask("ground"));
                if (collider)
                {
                    GameObject.Instantiate(pe, hit.point, Quaternion.identity);
                }
            }
        }
    }
    //鸟的特效
    public void SelfToTarget(string effectName)
    {
        PlayerEffecrt pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {

            ArrayList array = GetEnemyFront(AttackRange.Around);
            foreach (GameObject go in array)
            {
                if (go.activeInHierarchy == false) continue;
                SoundManager.instance.Play("bird");
                PlayerEffecrt effectGO = Instantiate(pe) as PlayerEffecrt;
                effectGO.gameObject.transform.position = transform.position + Vector3.up;
                effectGO.gameObject.GetComponent<EffectSettings>().Target = go;
            }
        }
    }
    //转的特效
    public void ShowSkill3()
    {
        PlayerEffecrt pe;
        if (effectDict.TryGetValue("CrowstormEffect", out pe))
        {
            ArrayList array = GetEnemyFront(AttackRange.Around);
            PlayerEffecrt effectGO = Instantiate(pe) as PlayerEffecrt;
            effectGO.gameObject.transform.position = transform.position;
            SoundManager.instance.Play("ice_attack");
        }
    }
    //敌人身上的特效
    void ShowEffectToEnemy(string effectName)
    {
        PlayerEffecrt pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {
            ArrayList array = GetEnemyFront(AttackRange.Forward);
            foreach (GameObject go in array)
            {
                //射线检测敌人地面的点
                RaycastHit hit;
                bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 2, LayerMask.GetMask("ground"));
                if (collider)
                {
                    //PlayerEffecrt effectGO = Instantiate(pe) as PlayerEffecrt;
                    //effectGO.gameObject.transform.position = hit.point;
                    GameObject.Instantiate(pe, hit.point, Quaternion.identity);
                }

            }
        }
    }
    /// <summary>
    /// 受到敌人的攻击传过来的参数
    /// </summary>
    void TakeDamage(int damage)
    {
        if (this.hp <= 0)
            return;//si le
        this.hp -= damage;
        //播放受到攻击动画
        ator.SetTrigger("takedamage");
        //显示血量减少
        hudText.Add("-" + damage, Color.red, 0.5f);
        hudHpSlider.value = (float)(hp - damage) / hp;
        if (hudHpSlider.value <= 0)
        {
            Dead();
        }
        //屏幕上血红色特效
        BloodScreen.instance.Show();
    }

    void Dead()
    {
        ator.SetBool("dead", true);
        MessageManager.instance.ShowMessage("您已经死亡，即将退出", 3);
        StartCoroutine(BeginLoadScene());
        
    }
    IEnumerator BeginLoadScene()
    {
        yield return new WaitForSeconds(2.5f);
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        LoadSceneProBar.instance.Show(op);
    }

    void LockSkill()
    {
        //print("lock");
        TranscriptManager.instance.s2Btn.transform.GetComponent<SkillButton>().Disable();
        TranscriptManager.instance.s3Btn.transform.GetComponent<SkillButton>().Disable();
        //TranscriptManager.instance.AttackBtn.transform.GetComponent<SkillButton>().Disable();
        TranscriptManager.instance.s1Btn.transform.GetComponent<SkillButton>().Disable();
    }

    void LockAttackSkill()
    {
        //print("lock");
        TranscriptManager.instance.s2Btn.transform.GetComponent<SkillButton>().Disable();
        TranscriptManager.instance.s3Btn.transform.GetComponent<SkillButton>().Disable();
        TranscriptManager.instance.s1Btn.transform.GetComponent<SkillButton>().Disable();
    }

    void FreeSkill()
    {
        //print("free");
        TranscriptManager.instance.s2Btn.transform.GetComponent<SkillButton>().Enable(); 
        TranscriptManager.instance.s3Btn.transform.GetComponent<SkillButton>().Enable();
        //TranscriptManager.instance.AttackBtn.transform.GetComponent<SkillButton>().Enable();
        TranscriptManager.instance.s1Btn.transform.GetComponent<SkillButton>().Enable();
    }
}

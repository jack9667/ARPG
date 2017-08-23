using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public TextAsset skillAsset;
    public static SkillManager instance;

    private ArrayList skList=new ArrayList();   //存放了一组读到的skill信息

    void Awake()
    {
        instance = this;
        ReadTextAndInitSkill();
    }

    void ReadTextAndInitSkill()
    {
        string[] skArray = skillAsset.ToString().Split('\n');

        foreach(string s in skArray)
        {
            string[] proArray = s.Split(',');
            Skill skill = new Skill();
            skill.Id = int.Parse(proArray[0]);
            skill.Name = proArray[1];
            skill.Icon = proArray[2];
            switch (proArray[3])    //任务类型
            {
                case "Warrior":
                    skill.PlayerType = PlayerType.Warrior;
                    break;
                case "FemaleAssassin":
                    skill.PlayerType = PlayerType.FemaleAssassin;
                    break;
            }
            switch (proArray[4])    //技能类型
            {
                case "Basic":
                    skill.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    skill.SkillType = SkillType.Skill;
                    break;
            }
            switch (proArray[5])    //位置类型
            {
                case "Basic":
                    skill.PosType = PosType.Basic;
                    break;
                case "One":
                    skill.PosType = PosType.One;
                    break;
                case "Two":
                    skill.PosType = PosType.Two;
                    break;
                case "Three":
                    skill.PosType = PosType.Three;
                    break;
            }
            skill.ColdTime = int.Parse(proArray[6]);    //冷却时间
            skill.Damage = int.Parse(proArray[7]);      //基础伤害
            skill.Level = 1;

            skList.Add(skill);
        }
    }
    /// <summary>
    /// 更具位置更新
    /// </summary>
    public Skill GetSkillByPos(PosType posType)
    {
       PlayerInfo info = PlayerInfo._instance;
        foreach(Skill skill in skList)
        {
            if (skill.PlayerType == info.PlayerType && skill.PosType == posType)
            {
                return skill;
            }
        }

        return null;
    }
}

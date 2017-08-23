using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能类型
public enum SkillType   
{
    Basic,
    Skill
}
//位置
public enum PosType
{
    Basic, One = 1, Two = 2, Three = 3
}


public class Skill {
    private int id; //ctrl r ctrl e快捷生成
    private string name;
    private string icon;
    private PlayerType playerType;
    private SkillType skillType;    //技能类型
    private PosType posType;        //位置
    private int coldTime;   //冷却时间
    private int damage;     //基础攻击力
    private int level = 1;  //等级

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public PlayerType PlayerType
    {
        get { return playerType; }
        set { playerType = value; }
    }
    public SkillType SkillType
    {
        get { return skillType; }
        set { skillType = value; }
    }
    public PosType PosType
    {
        get { return posType; }
        set { posType = value; }
    }
    public int ColdTime
    {
        get { return coldTime; }
        set { coldTime = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }


    public void Upgrade()
    {
        Level++;
    }

}

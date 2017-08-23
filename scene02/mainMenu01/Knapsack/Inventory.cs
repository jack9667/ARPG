using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品的总类，都有什么
/// </summary>

public enum InventoryType
{
    Equip,  //装备
    Drug,    //药品
    Box//宝箱
}

public enum EquipType
{
    Helm,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing
}


//物品总类
public class Inventory {

    private int id;
    private string name;//名字
    private string icon;//在图集中的类型
    private InventoryType inventoryType;//物品类型
    private EquipType equipType;//装备类型
    private int price = 0;
    private int starLevel = 1;//星级
    private int quality = 1;
    private int damage = 0;
    private int hp = 0;
    private int power = 0;//战斗力
    private PlayerInfo.InfoType infoType;//作用在什么属性上
    private int applyValue;//作用值
    private string des;    //描述

    //会变化，先去掉了
    //private int level = 1;//装备等级
    //private int count = 1;//物品个数

    #region get set
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string ICON
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }
    public InventoryType InventoryTYPE
    {
        get
        {
            return inventoryType;
        }
        set
        {
            inventoryType = value;
        }
    }
    public EquipType EquipTYPE
    {
        get
        {
            return equipType;
        }
        set
        {
            equipType = value;
        }
    }
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
        }
    }
    public int StarLevel
    {
        get
        {
            return starLevel;
        }
        set
        {
            starLevel = value;
        }
    }
    public int Quality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = value;
        }
    }
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }
    public PlayerInfo.InfoType InfoTYPE
    {
        get
        {
            return infoType;
        }
        set
        {
            infoType = value;
        }
    }
    public int ApplyValue
    {
        get
        {
            return applyValue;
        }
        set
        {
            applyValue = value;
        }
    }
    public string Des
    {
        get
        {
            return des;
        }
        set
        {
            des = value;
        }
    } 
    /*public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }*/

    #endregion
}

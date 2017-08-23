using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色含有的物品的根类，它包含了inventory这个属性
/// </summary>
public class InventoryItem {

    public Inventory inventory;
    private int level;//等级
    private int count;//个数
    private bool isDressed = false; //装备是否被穿上
    private int posInUi;

    public int Count
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
    }
    public bool IsDressed
    {
        get
        {
            return isDressed;
        }
        set
        {
            isDressed = value;
        }
    }
    
}

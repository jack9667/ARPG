using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品管理初始化类，被放在Game Controller下了
/// </summary>
public class InventoryManager : MonoBehaviour {

    public TextAsset listinfo;
    public Dictionary<int, Inventory> dict=new Dictionary<int, Inventory>();   //物品总类字典
    //public Dictionary<int, InventoryItem> dictItem = new Dictionary<int, InventoryItem>(); //角色拥有字典
    public List<InventoryItem> listItem = new List<InventoryItem>();    //角色拥有的物品list
    public static InventoryManager _instance;


    //inventory更新事件
    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChanged;

    void Awake()
    {
        _instance = this;
        ReadInventoryInfo();
        OnInventoryChanged += OnInventoryChanged1;
    }

    void OnInventoryChanged1()
    {

    }
    void Start()
    {
        ReadInventoryItemInfo();
    }

    //读取物品信息文件，存到每一个inventory中，再把每个inventory放到一个字典索引中
    //也就是我都定义了什么物品，每个物品具有什么属性
    void ReadInventoryInfo()
    {
        string str = listinfo.ToString();
        string[] itemStrArray = str.Split('\n');
        foreach(string s in itemStrArray)
        {
            //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            string[] preArray = s.Split('|');
            Inventory inventory = new Inventory();
            inventory.ID = int.Parse(preArray[0]);
            inventory.Name = preArray[1];
            inventory.ICON = preArray[2];
            switch (preArray[3])
            {
               case "Equip":
                    inventory.InventoryTYPE = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryTYPE = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryTYPE = InventoryType.Box;
                    break;
            }
            if (inventory.InventoryTYPE == InventoryType.Equip)
            {
                switch (preArray[4])
                {
                    case "Helm":
                        inventory.EquipTYPE = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipTYPE = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipTYPE = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipTYPE = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipTYPE = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipTYPE = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipTYPE = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipTYPE = EquipType.Wing;
                        break;
                }
                inventory.Price = int.Parse(preArray[5]);
            }
            //print(s);
            
            if (inventory.InventoryTYPE == InventoryType.Equip)
            {
                inventory.StarLevel = int.Parse(preArray[6]);
                inventory.Quality = int.Parse(preArray[7]);
                inventory.Damage = int.Parse(preArray[8]);
                inventory.HP = int.Parse(preArray[9]);
                inventory.Power = int.Parse(preArray[10]);
            }
            if (inventory.InventoryTYPE == InventoryType.Drug)
            {
                inventory.ApplyValue = int.Parse(preArray[12]);
            }
            inventory.Des = preArray[13];
            
            dict.Add(inventory.ID, inventory);
            
        }
        
    }
    //完成角色信息初始化，获得应有的物品，放到listItem中
    void ReadInventoryItemInfo()
    {
        //需要连接服务器取得当前角色拥有的物品信息
        //先随机搞了
        for(int i = 0; i < 20; i++)
        {
            int id = Random.Range(1001, 1020);
            
            Inventory inv = null;
            dict.TryGetValue(id, out inv);
            //判断是药品还是装备，分装不同的格子里
            if (inv.InventoryTYPE == InventoryType.Equip)
            {
                InventoryItem it = new InventoryItem();
                it.inventory = inv;
                it.Level = Random.Range(0, 20);
                it.Count = 1;
                listItem.Add(it);
            }
            else
            {
                //判断是否已经存在
                InventoryItem it = null;
                bool isExit = false;
                foreach(var p in listItem)
                {
                    if (p.inventory.ID == id)
                    {
                        isExit = true;
                        it = p;
                        break;
                    }
                }
                if (isExit)
                {
                    it.Count++;
                }
                else
                {
                    it = new InventoryItem();
                    it.Count = 1;
                    it.inventory = inv;
                    listItem.Add(it);
                }

                //if(dict.TryGetValue(id,out inv))
                //{
                //    it.Count++;
                //}
                //else
                //{
                //    it = new InventoryItem();
                //    it.Count = 1;
                //    it.inventory = inv;
                //    listItem.Add(it);
                //}

            }
            
        }
        //初始化完所有角色背包之后
        //调用事件，其在inventorUI中注册的
        //交给inventorUI更新
        OnInventoryChanged();   
    }

    public void RemoveInventoryItem(InventoryItem it)
    {
        this.listItem.Remove(it);
    }
}


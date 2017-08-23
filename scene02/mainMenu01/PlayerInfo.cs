using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Warrior,    //展示
    FemaleAssassin  //女刺客
}

/// <summary>
/// 人物的总体信息，其中就包含任务有的装备，姓名经验等
/// 是EqwuipRole中的装备记录在这里
/// 人物拥有的物品存在InventoryManager中的listItem
/// </summary>
public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo _instance;

    #region property
    private string _name; //姓名
    private string _headPortrait;//头像
    private int _levelNums = 1;//等级
    private int _powerNum = 1;//战斗力
    private int _expNum = 0;//经验数
    private int _diamondNum;//钻石数
    private int _coinNum;//金币数
    private int _energyNum;//体力数
    private int _toughenNum;//历练数
    private PlayerType playerType;  //任务角色

    //人物的装备等各个属性
    private int _hp;
    private int _damage;    //？干啥？sb siki
    //private int _helmID =0 ;
    //private int _clothID =0 ;
    //private int _weaponID =0 ;
    //private int _shoesID=0;
    //private int _necklaceID=0;
    //private int _braceletID=0;
    //private int _ringID=0;
    //private int _wingID=0;

    //已经穿上的装备
    public InventoryItem helmInventoryItem;
    public InventoryItem clothInventoryItem;
    public InventoryItem weaponInventoryItem;
    public InventoryItem shoesInventoryItem;
    public InventoryItem necklaceInventoryItem;
    public InventoryItem braceletInventoryItem;
    public InventoryItem ringInventoryItem;
    public InventoryItem wingInventoryItem;

    #endregion
    #region get set methd
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public string HeadPortrait
    {
        get
        {
            return _headPortrait;
        }
        set
        {
            _headPortrait = value;
        }
    }
    public int LevelNums
    {
        get
        {
            return _levelNums;
        }
        set
        {
            _levelNums = value;
        }
    }
    public int PowerNum
    {
        get
        {
            return _powerNum;
        }
        set
        {
            _powerNum = value;
        }
    }
    public int ExpNum
    {
        get
        {
            return _expNum;
        }
        set
        {
            _expNum = value;
        }
    }
    public int DiamondNum
    {
        get
        {
            return _diamondNum;
        }
        set
        {
            _diamondNum = value;
        }
    }
    public int CoinNum
    {
        get
        {
            return _coinNum;
        }
        set
        {
            _coinNum = value;
        }
    }
    public int EnergyNum
    {
        get
        {
            return _energyNum;
        }
        set
        {
            _energyNum = value;
        }
    }
    public int ToughenNum
    {
        get
        {
            return _toughenNum;
        }
        set
        {
            _toughenNum = value;
        }
    }
    public PlayerType PlayerType
    {
        get
        {
            return playerType;
        }
        set
        {
            playerType = value;
        }
    }

    private int pbuf;

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    //public int HelmID
    //{
    //    get { return _helmID; }
    //    set { _helmID = value; }
    //}
    //public int ClothID
    //{
    //    get { return _clothID; }
    //    set { _clothID = value; }
    //}
    //public int WeaponID
    //{
    //    get
    //    {
    //        return _weaponID;
    //    }
    //    set
    //    {
    //        _weaponID = value;
    //    }
    //}
    //public int ShoesID
    //{
    //    get
    //    {
    //        return _shoesID;
    //    }
    //    set
    //    {
    //        _shoesID = value;
    //    }
    //}
    //public int NecklaceID
    //{
    //    get { return _necklaceID; }
    //    set { _necklaceID = value; }
    //}
    //public int BraceletID
    //{
    //    get { return _braceletID; }
    //    set { _braceletID = value; }
    //}
    //public int RingID
    //{
    //    get { return _ringID; }
    //    set { _ringID = value; }
    //}
    //public int WingID
    //{
    //    get
    //    {
    //        return _wingID;
    //    }
    //    set
    //    {
    //        _wingID = value;
    //    }
    //}
    #endregion

    //两个计时器
    public float energyTimer = 0;
    public float toughenTimer = 0;

    #region  unity event
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Init();
    }
    void FixedUpdate()
    {
        //体力和历练自动增长
        if (this.EnergyNum < 100)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer > 60)
            {
                this.EnergyNum++;
                energyTimer = 0;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        }
        else
        {
            this.energyTimer = 0;
        }

        if (this.ToughenNum < 50)
        {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60)
            {
                this.ToughenNum++;
                toughenTimer = 0;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        }
        else
        {
            this.toughenTimer = 0;
        }
    }
    #endregion

    //事件方法
    //被修改的类型
    public enum InfoType
    {
        Name,
        HeadPortrait,
        Level,
        Power,
        Exp,
        Diamond,
        Coin,
        Energy,
        Toughen,
        Hp,
        Damage,
        Equip,
        All
    }
    //委托
    public delegate void OnPlayerInfoChangeEvent(InfoType type);
    //事件
    public event OnPlayerInfoChangeEvent OnPlayerInfoChanged;
    //初始化，从服务器取得信息
    void Init()
    {
        this.CoinNum = 4321;
        this.DiamondNum = 1234;
        this.EnergyNum = 88;
        this.ToughenNum = 30;
        this.ExpNum = 123;
        this.HeadPortrait = "1";
        this.LevelNums = 9999;
        this.Name = "战神阿凯";
        this.PowerNum = 1;
        pbuf = this.PowerNum;//role面板更新用
        this.playerType = PlayerType.Warrior;
        
        //随机穿上几个装备
        //this.BraceletID = 1001;
        //this.WingID = 1002;
        //this.RingID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;
        InitHpDamPow();

        OnPlayerInfoChanged(InfoType.All);
    }

    public void ChangeName(string name)
    {
        if (name != "")
        {
            this.Name = name;
            OnPlayerInfoChanged(InfoType.Name);
        }
        
    }

    //穿上装备
    void PutOnEquip(int id)
    {
        if (id == 0) return;
        Inventory invty = null;
        InventoryManager._instance.dict.TryGetValue(id, out invty);
        this.HP += invty.HP;
        this.Damage += invty.Damage;
        this.PowerNum += invty.Power;
    }
    //脱下装备
    void PutOffEquip(int id)
    {
        if (id == 0) return;
        Inventory invty = null;
        InventoryManager._instance.dict.TryGetValue(id, out invty);
        this.HP -= invty.HP;
        this.Damage -= invty.Damage;
        this.PowerNum -= invty.Power;
    }

    void InitHpDamPow()
    {
        this.HP = this.LevelNums * 100;
        this.Damage = this.Damage * 50;
        this.PowerNum = this.PowerNum;
        //PutOnEquip(BraceletID);
        //PutOnEquip(WingID);
        //PutOnEquip(RingID);
        //PutOnEquip(ClothID);
        //PutOnEquip(HelmID);
        //PutOnEquip(WeaponID);
        //PutOnEquip(NecklaceID);
        //PutOnEquip(ShoesID);
    }
    /// <summary>
    /// 穿上了一个装备,popup界面穿上装备来这里调用
    /// 给用户设置都穿了什么
    /// </summary>
    public void DressOn(InventoryItem it)
    {
        //当前装备被穿上了
        it.IsDressed = true;
        InventoryItem inventoryItemDressed = null;  //保存要脱下的
        //有穿上的就替换，原来的保存在inventoryItemDressed中
        //没有就世界替换
        switch (it.inventory.EquipTYPE)
        {
            case EquipType.Bracelet:
                if (braceletInventoryItem != null)  //当前已经有装备了，原来的保存在inventoryItemDressed中
                {
                    inventoryItemDressed = braceletInventoryItem;
                }
                braceletInventoryItem = it; //把popup里的装备给playerinfo，即更新playerinfo中的装备
                break;
            case EquipType.Cloth:
                if (clothInventoryItem != null)
                {
                    inventoryItemDressed = clothInventoryItem;
                }
                clothInventoryItem = it;
                break;
            case EquipType.Helm:
                if (helmInventoryItem != null)
                {
                    inventoryItemDressed = helmInventoryItem;
                }
                helmInventoryItem = it;
                break;
            case EquipType.Necklace:
                if (necklaceInventoryItem != null)
                {
                    inventoryItemDressed = necklaceInventoryItem;
                }
                necklaceInventoryItem = it;
                break;
            case EquipType.Ring:
                if (ringInventoryItem != null)
                {
                    inventoryItemDressed = ringInventoryItem;
                }
                ringInventoryItem = it;
                break;
            case EquipType.Shoes:
                if (shoesInventoryItem != null)
                {
                    inventoryItemDressed = shoesInventoryItem;
                }
                shoesInventoryItem = it;
                break;
            case EquipType.Weapon:
                if (weaponInventoryItem != null)
                {
                    inventoryItemDressed = weaponInventoryItem;
                }
                weaponInventoryItem = it;
                break;
            case EquipType.Wing:
                if (wingInventoryItem != null)
                {
                    inventoryItemDressed = wingInventoryItem;
                }
                wingInventoryItem = it;
                break;
        }
        ///这就是把脱下的放进背包中
        if (inventoryItemDressed != null)
        {
            inventoryItemDressed.IsDressed = false; //把脱下的至为没被穿上
            InventoryUI._instance.AddInventoryItem(inventoryItemDressed);//把没被穿的更新到背包里
        }

        OnPlayerInfoChanged(InfoType.Equip);    //更新info里的装备
        PowerNum = GetAllPower();
        OnPlayerInfoChanged(InfoType.All);

    }
    /// <summary>
    /// knapsackRole的装备被脱下
    /// </summary>
    /// <param name="it"></param>
    public void DressOff(InventoryItem it)
    {

        switch (it.inventory.EquipTYPE)
        {
            case EquipType.Bracelet:
                if (braceletInventoryItem != null)  //穿在当前位置
                {
                    braceletInventoryItem = null;
                }
                break;
            case EquipType.Cloth:
                if (clothInventoryItem != null)
                {
                    clothInventoryItem = null;
                }
                break;
            case EquipType.Helm:
                if (helmInventoryItem != null)
                {
                    helmInventoryItem = null;
                }
                break;
            case EquipType.Necklace:
                if (necklaceInventoryItem != null)
                {
                    necklaceInventoryItem = null;
                }
                break;
            case EquipType.Ring:
                if (ringInventoryItem != null)
                {
                    ringInventoryItem = null;
                }
                break;
            case EquipType.Shoes:
                if (shoesInventoryItem != null)
                {
                    shoesInventoryItem = null;
                }
                break;
            case EquipType.Weapon:
                if (weaponInventoryItem != null)
                {
                    weaponInventoryItem = null;
                }
                break;
            case EquipType.Wing:
                if (wingInventoryItem != null)
                {
                    wingInventoryItem = null;
                }
                break;
        }
        it.IsDressed = false;
        InventoryUI._instance.AddInventoryItem(it);

        OnPlayerInfoChanged(InfoType.Equip);
        PowerNum = GetAllPower();
        OnPlayerInfoChanged(InfoType.All);
    }
    /// <summary>
    /// 需要个数的金币数
    /// </summary>
    /// <param name="count"></param>
    public bool GetCoin(int count)
    {
        if (CoinNum >= count)
        {
            CoinNum -= count;
            OnPlayerInfoChanged(InfoType.Coin);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 调用InventoryManager存储的listItem去改变人物拥有的物品
    /// </summary>
    /// <param name="it"></param>
    /// <param name="count"></param>
    public void UseInventory(InventoryItem it,int count)
    {
        //使用效果

        //处理物品使用后的个数
        it.Count -= count;
        if (it.Count >= 0)
        {
            //还有

        }
        else
        {
            //用完了,在列表中移除该物品
            InventoryManager._instance.listItem.Remove(it);
        }
    }
    /// <summary>
    /// 得到全部的战斗力
    /// </summary>
    public int GetAllPower()
    {
        float power = pbuf;
        if (helmInventoryItem != null)
        {
            power += helmInventoryItem.inventory.Power * (1 + (helmInventoryItem.Level) / 10f); //基础+升级加成
        }
        if (clothInventoryItem != null)
        {
            power += clothInventoryItem.inventory.Power * (1 + (clothInventoryItem.Level ) / 10f); //基础+升级加成
        }
        if (weaponInventoryItem != null)
        {
            power += weaponInventoryItem.inventory.Power * (1 + (weaponInventoryItem.Level ) / 10f); //基础+升级加成
        }
        if (shoesInventoryItem != null)
        {
            power += shoesInventoryItem.inventory.Power * (1 + (shoesInventoryItem.Level ) / 10f); //基础+升级加成
        }
        if (necklaceInventoryItem != null)
        {
            power += necklaceInventoryItem.inventory.Power * (1 + (necklaceInventoryItem.Level) / 10f); //基础+升级加成
        }
        if (braceletInventoryItem != null)
        {
            power += braceletInventoryItem.inventory.Power * (1 + (braceletInventoryItem.Level ) / 10f); //基础+升级加成
        }
        if (ringInventoryItem != null)
        {
            power += ringInventoryItem.inventory.Power * (1 + (ringInventoryItem.Level ) / 10f); //基础+升级加成
        }
        if (wingInventoryItem != null)
        {
            power += wingInventoryItem.inventory.Power * (1 + (wingInventoryItem.Level ) / 10f); //基础+升级加成
        }
        return (int)power;
    }
    //
    public void  AddCoin(int count)
    {
        this.CoinNum += count;
        OnPlayerInfoChanged(InfoType.Coin);
    }

}

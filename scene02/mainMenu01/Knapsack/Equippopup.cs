using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 点击就出，物品显示界面脚本
/// </summary>
public class Equippopup : MonoBehaviour {

    private InventoryItem item;
    private InventoryItemUI itUI;
    private KnapsackRoleEquip itRole;

    private UISprite equipSprite;
    private UILabel nameLabel;
    private UILabel qualityLabel;
    private UILabel damageLabel;
    private UILabel hpLabel;
    private UILabel powerLabel;
    private UILabel desLabel;
    private UILabel levelLabel;
    private UILabel btnLabel;//装备和卸载

    private UIButton closeButton;
    private UIButton equipButton;//装备和卸载
    private UIButton upgradeButton;//升级

    private bool isLeft = true;

    void Awake()
    {
        equipSprite = transform.Find("EquipBg/Sprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        qualityLabel = transform.Find("PzLabel/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
        hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel/NumLabel").GetComponent<UILabel>();
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("btn-Equip/Label").GetComponent<UILabel>();
        closeButton = transform.Find("btn-close").GetComponent<UIButton>();
        equipButton = transform.Find("btn-Equip").GetComponent<UIButton>();
        upgradeButton = transform.Find("btn-Upgrade").GetComponent<UIButton>();

        ///注册点击事件
        ///ngui自带eventdelegate和onClick.Add用于添加点击事件
        EventDelegate ed1 = new EventDelegate(this, "OnCloseClick");
        closeButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnEquipClick");
        equipButton.onClick.Add(ed2);
        EventDelegate ed3 = new EventDelegate(this, "OnUpgradeClick");
        upgradeButton.onClick.Add(ed3);
    }

    public void Show(InventoryItem it,InventoryItemUI itUi,KnapsackRoleEquip itRole,bool isLeft=true) //show将点击的inventoryUI传进来
    {
        gameObject.SetActive(true);
        this.item = it;     //被点击的itui传给界面自己的item
        this.itUI = itUi;
        this.isLeft = isLeft;
        this.itRole = itRole;
        Vector3 pos = transform.localPosition;
        if (isLeft)
        {
            transform.localPosition = new Vector3(-355.0f, pos.y, pos.z);
            btnLabel.text="装备";
        }
        else
        {
            transform.localPosition = new Vector3(3.0f, pos.y, pos.z);
            btnLabel.text = "卸下";
        }
        equipSprite.spriteName = item.inventory.ICON;
        nameLabel.text = item.inventory.Name;
        qualityLabel.text = item.inventory.Quality.ToString();
        damageLabel.text = item.inventory.Damage.ToString();
        hpLabel.text = item.inventory.HP.ToString();
        powerLabel.text = item.inventory.Power.ToString();
        desLabel.text = item.inventory.Des;
        levelLabel.text = item.Level.ToString();
    }
    ///注册按钮点击事件
    public void OnCloseClick()
    {
        this.item = null;
        gameObject.SetActive(false);
        transform.parent.SendMessage("DisBtn");
    }
    /// <summary>
    /// 点击卸下按钮和装备按钮是触发
    /// </summary>
    public void OnEquipClick()
    {
        int startValue = PlayerInfo._instance.GetAllPower();
        //左边v表示从背包里面点击 ---装备
        if (this.isLeft)
        {
            //清空背包ui的item
            this.itUI.Clear();
            PlayerInfo._instance.DressOn(this.item); 
        }//右边表示从role中点击装备 ---卸下
        else
        {
            this.itRole.Clear();
            PlayerInfo._instance.DressOff(this.item);
            
        }
        this.item = null;
        this.itUI = null;
        gameObject.SetActive(false);
        int endValue = PlayerInfo._instance.GetAllPower();
        //PlayerInfo._instance.PowerNum = endValue;
        PowerShow.instance.ShowPowerChange(startValue, endValue);

        InventoryUI._instance.SendMessage("UpdateCount");// to inventoryUi  更新格子个数-1
        transform.parent.SendMessage("DisBtn");

    }
    /// <summary>
    /// 升级装备
    /// </summary>
    public void OnUpgradeClick()
    {
        int startValue = PlayerInfo._instance.GetAllPower();
        ///需要的金币数
        int cionNdeed = (this.item.Level + 1) * item.inventory.Price;
        bool isSuccess = PlayerInfo._instance.GetCoin(cionNdeed);
        if (isSuccess)
        {
            item.Level += 1;
            levelLabel.text = item.Level.ToString();
        }
        else
        {
            //金币不足
            MessageManager.instance.ShowMessage("需要金币："+cionNdeed, 0.5f);
        }
        int endValue = PlayerInfo._instance.GetAllPower();
        //PlayerInfo._instance.PowerNum = endValue;
        PowerShow.instance.ShowPowerChange(startValue, endValue);
    }
    void ClearObj()
    {
        this.item = null;
        this.itUI = null;
        gameObject.SetActive(false);
    }
    //关掉自身
    public void Close()
    {
       
        ClearObj();
    }
}

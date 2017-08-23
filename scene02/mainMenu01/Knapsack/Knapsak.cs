using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//整个物品界面管理脚本
//

public class Knapsak : MonoBehaviour {

    public static Knapsak instance;

    private Equippopup equipPopup;
    private InventoryPopup inventoryPopup;

    private UIButton saleButton;
    private UILabel priceLabel;
    private UIButton closeButton;
    private UIButton upgradeButton;

    private InventoryItemUI itUI;

    void Awake()
    {
        instance = this;
        equipPopup = transform.Find("EquipPopup").GetComponent<Equippopup>();
        inventoryPopup = transform.Find("InventoryPopup").GetComponent<InventoryPopup>();
        saleButton = transform.Find("Inventory/btn-Sale").GetComponent<UIButton>();
        priceLabel = transform.Find("Inventory/PriceBg/Label").GetComponent<UILabel>();
        closeButton = transform.Find("Inventory/btn-close").GetComponent<UIButton>();
        upgradeButton= transform.Find("EquipPopup/btn-Upgrade").GetComponent<UIButton>();
        DisBtn();
        priceLabel.text = "售价";
        EventDelegate ed1 = new EventDelegate(this, "OnSaleClick");
        saleButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "HideKnapsak");
        closeButton.onClick.Add(ed2);

    }
    /// <summary>
    /// 物品点击事件。被点击的物品上传到这里,KnapsackRoleEquip和InventoryItemUI里面的OnPress（sendmessage过来的）
    /// </summary>
    /// <param name="objArray"></param>
	public void OnInventoryClick(object[] objArray)
    {
        
        InventoryItem it = objArray[0] as InventoryItem;    //被电击的物品
        
        bool isLeft = (bool)objArray[1];
        if (it.inventory.InventoryTYPE == InventoryType.Equip)
        {
            InventoryItemUI itUi = null;
            KnapsackRoleEquip itRole = null;
            if (isLeft == true) //背包中装备
            {
                itUi = objArray[2] as InventoryItemUI;
            }else {     //role中装备
                itRole = objArray[2] as KnapsackRoleEquip;
            }
            equipPopup.Show(it, itUi,itRole,isLeft);    //show将点击的inventoryUI传进来
            inventoryPopup.Close();
        }
        else
        {
            InventoryItemUI itUI = objArray[2] as InventoryItemUI;
            inventoryPopup.Show(it,itUI);
            equipPopup.Close();
        }

        if (isLeft)    //不是role中点击的
        {
            this.itUI = objArray[2] as InventoryItemUI;
            EnableBUtn();
            priceLabel.text = "售价"+this.itUI.it.inventory.Price*this.itUI.it.Count;
        }
        else
        {
            upgradeButton.isEnabled = false;
        }
    }
    //禁用出售功能
    void DisBtn()
    {
        saleButton.SetState(UIButtonColor.State.Disabled, true);
        saleButton.isEnabled = false;
        priceLabel.text = "售价";
    }
    //启用
    void EnableBUtn()
    {
        saleButton.isEnabled = true;
        upgradeButton.isEnabled = true;
    }
    //出售
    void OnSaleClick()
    {
        int price = int.Parse(priceLabel.text.Substring(2));
        PlayerInfo._instance.AddCoin(price);
        InventoryManager._instance.RemoveInventoryItem(itUI.it);
        itUI.Clear();
        equipPopup.Close();
        inventoryPopup.Close();
        DisBtn();
        InventoryUI._instance.UpdateCount();
    }
    //
    public void ShowKnapsak()
    {
        gameObject.SetActive(true);
    }
    public void HideKnapsak()
    {
        gameObject.SetActive(false);
    }
}

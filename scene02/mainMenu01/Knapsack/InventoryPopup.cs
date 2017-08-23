using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 药品弹出细节框框
/// </summary>
public class InventoryPopup : MonoBehaviour {

    private UILabel nameLabel;
    private UISprite inventorySprite;
    private UILabel desLabel;
    private UILabel btnLabel;
    private InventoryItem it;

    private UIButton closeButton;
    private UIButton useButton;//使用
    private UIButton useBatchingButton;//批量使用

    private InventoryItemUI itUI;
    

    void Awake()
    {
        nameLabel = transform.Find("InventoryBg/Name-Label").GetComponent<UILabel>();
        inventorySprite = transform.Find("InventoryBg/InventSprite/Sprite").GetComponent<UISprite>();
        desLabel = transform.Find("InventoryBg/InventSprite/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("InventoryBg/btn-BatchingUse/Label").GetComponent<UILabel>();
        closeButton = transform.Find("InventoryBg/btn-close").GetComponent<UIButton>();
        useButton = transform.Find("InventoryBg/btn-Use").GetComponent<UIButton>();
        useBatchingButton = transform.Find("InventoryBg/btn-BatchingUse").GetComponent<UIButton>();

        ///注册点击事件
        EventDelegate ed1 = new EventDelegate(this,"OnCloseClick");
        closeButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnUseClick");
        useButton.onClick.Add(ed2);
        EventDelegate ed3 = new EventDelegate(this, "OnBatchingClick");
        useBatchingButton.onClick.Add(ed3);

    }
    /// <summary>
    /// 展示函数
    /// </summary>
    /// <param name="it"></param>
    public void Show(InventoryItem it,InventoryItemUI itUi)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUi;
        nameLabel.text = it.inventory.Name;
        inventorySprite.spriteName = it.inventory.ICON;
        desLabel.text = it.inventory.Des;
        btnLabel.text = "批量使用(" + it.Count + ")";
    }

    public void OnCloseClick()
    {
        Clear();
        gameObject.SetActive(false);
        transform.parent.SendMessage("DisBtn");
    }
    /// <summary>
    /// 使用一次 按钮
    /// </summary>
    public void OnUseClick()
    {
        itUI.ChangerCount(1);   //改变个数
        PlayerInfo._instance.UseInventory(it, 1);   //更新人物中含有的数量
        it.IsDressed = true;    //背包遍历条件设为否定，被穿上，不被便利
        InventoryUI._instance.SendMessage("UpdateCount");   //to inventoryUI 更新个数
        OnCloseClick();
    }
    /// <summary>
    /// 批量使用按钮
    /// </summary>
    public void OnBatchingClick()
    {
        itUI.ChangerCount(it.Count);
        PlayerInfo._instance.UseInventory(it, it.Count);
        it.IsDressed = true;
        InventoryUI._instance.SendMessage("UpdateCount");
        OnCloseClick();
    }
    void Clear()
    {
        
        this.it = null;
        this.itUI = null;
        
    }
    void ClearObj()
    {
        this.it = null;
        this.itUI = null;
        gameObject.SetActive(false);
    }
    //关掉自身
    public void Close()
    {
        ClearObj();
    }
}

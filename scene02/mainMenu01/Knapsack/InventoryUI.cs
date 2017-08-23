using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物总的背包物品ui,真个sroll vie
/// </summary>

public class InventoryUI : MonoBehaviour {

    private UIButton clearButton;
    private UILabel inventoryLabel;// 2/32个各自
    private int count;//多少个格子

    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();  //所有物品的格子

    public static InventoryUI _instance;

    void Awake()
    {
        _instance = this;
        clearButton = transform.Find("btn-Clearup").GetComponent<UIButton>();
        inventoryLabel = transform.Find("InventoryLabel").GetComponent<UILabel>();
        //注册物品更新事件
        ///在inventory manager里面调用更新
        InventoryManager._instance.OnInventoryChanged += OnInventoryChanged;
        EventDelegate ed1 = new EventDelegate(this, "OnClearUp");
        clearButton.onClick.Add(ed1);
        UpdateShow();
    }

    void OnDestory()
    {
        InventoryManager._instance.OnInventoryChanged -= OnInventoryChanged;
    }

    void OnInventoryChanged()
    {
        UpdateShow();
    }
    //更新背包里的物品
    void UpdateShow()
    {
        int temp = 0;
        //更新背包的物品
        for(int i = 0; i < InventoryManager._instance.listItem.Count; i++)
        {
            InventoryItem it = InventoryManager._instance.listItem[i];
            if (it.IsDressed == false)
            {
                itemUIList[temp].SetInventoryItem(it);
                temp++;
            } 
        }
        count = temp;
        //清除空的
        for(int i= temp; i < itemUIList.Count; i++)
        {
            itemUIList[i].Clear();
        }
        inventoryLabel.text = count + "/32";
    }
    /// <summary>
    /// 放回到背包中
    /// </summary>
    /// <param name="it"></param>
    public void AddInventoryItem(InventoryItem it)
    {
        
        foreach (InventoryItemUI itUI in itemUIList)
        {
            if (itUI.it == null)
            {
                itUI.SetInventoryItem(it);
                count++;
                break;
            }
        }
        inventoryLabel.text = count + "/32";
    }
    //整理
    void OnClearUp()
    {
        UpdateShow();
    }
    //背包里面数量更新---
    public void UpdateCount()
    {
        count = 0;
        foreach (InventoryItemUI itUI in itemUIList)
        {
            if (itUI.it != null)
            {
                count++;
                continue;
            }
        }
        inventoryLabel.text = count + "/32";
    }
}

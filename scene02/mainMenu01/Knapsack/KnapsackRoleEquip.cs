using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//左边，角色信息界面的装备的属性
//
public class KnapsackRoleEquip : MonoBehaviour {

    private UISprite _sprite;
    private UISprite Sprite
    {
        get
        {
            if (_sprite == null)
                _sprite = this.GetComponent<UISprite>();
            return _sprite;
        }
    }
    //void Awake()
    //{
    //    Sprite = this.GetComponent<UISprite>();
    //}
    private InventoryItem it;
    //暂时不用
	public void SetId(int id)
    {
        Inventory inventory = null;
        if(InventoryManager._instance.dict.TryGetValue(id, out inventory))
        {
            Sprite.spriteName = inventory.ICON;
        }
    }
    /// <summary>
    /// 将item显示到role面板中
    /// </summary>
    /// <param name="it"></param>
    public void SetInventoryItem(InventoryItem it)
    {
        if (it == null) return;
        this.it = it;
        Sprite.spriteName = it.inventory.ICON;
    }

    //被按下传递给knapsack信息
    //更新点击出的物品面板
    public void OnPress(bool isPress)
    {
        if (isPress&&it!=null)
        {
            object[] objArray = new object[3];
            objArray[0] = this.it;
            objArray[1] = false;//在右边
            objArray[2] = this;
            transform.parent.parent.SendMessage("OnInventoryClick", objArray);  //to knapsak to show popup
        }
    }

    public void Clear()
    {
        Sprite.spriteName = "bg_道具";
        this.it = null;
    }
}

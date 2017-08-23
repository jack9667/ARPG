using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//背包界面中每个item的脚本 inventoryItemUI->包含一个inventoryItem
//
public class InventoryItemUI : MonoBehaviour {

    private UISprite sprite;
    private UILabel label;
    //当前格子的的item物品
    public InventoryItem it;

   

    public UISprite Sprite
    {
        get
        {
            if (sprite == null)
                sprite = transform.Find("Sprite").GetComponent<UISprite>();
            return sprite;
        }
    }

    public UILabel Label
    {
        get
        {
            if (label == null)
                label = transform.Find("Label").GetComponent<UILabel>();
            return label;
        }
    }
 
    //显示item
    public void SetInventoryItem(InventoryItem it)
    {
        this.it = it;
        Sprite.spriteName = it.inventory.ICON;
        if (it.Count == 1)
        {
            Label.text = "";
        }
        else
        {
            Label.text = it.Count.ToString();
        }
        
    }

    public void Clear()
    {
        Label.text = "";
        Sprite.spriteName = "bg_道具";
        this.it = null;
        
    }
    //被按下传递给knapsack信息
    public void OnClick()
    {
        if (it!=null)
        {
            object[] objArray = new object[3];
            objArray[0] = this.it;
            objArray[1] = true; //是否是在左边 在背包里面点应该显示在右边，所以是false
            objArray[2] = this; //放的是当前invertoryitemUI，里面包含了it，siki这个sb
            transform.parent.parent.parent.SendMessage("OnInventoryClick", objArray);   //to Knapsk，调用显示界面
        }
    }
    
    public void ChangerCount(int count)
    {
        if (it.Count - count <= 0)
        {
            Clear();
        }else if(it.Count - count == 1)
        {
            label.text="";
        }
        else
        {
            label.text = (it.Count - count).ToString();
        }
    }
}

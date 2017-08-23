using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemUI : MonoBehaviour {

    public static SkillItemUI instance;
    public PosType posType;

    public Skill skill;

    private UISprite sprite;
    private UILabel labelName;
    private UIButton btn;

    public UISprite Sprite
    {
        get
        {
            if (sprite == null)
                sprite = this.GetComponent<UISprite>();
            return sprite;
        }
    }
    public UIButton Btn
    {
        get
        {
            if (btn == null)
                btn = this.GetComponent<UIButton>();
            return btn;
        }

    }
    public UILabel LabelName
    {
        get
        {
            return labelName;
        }

        set
        {
            labelName = value;
        }
    }

    object[] obj=new object[2];

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LabelName = transform.Find("Label").GetComponent<UILabel>();
        UpdateShow();
    }

    //更新技能显示
    void UpdateShow()
    {
        skill = SkillManager.instance.GetSkillByPos(posType);   //更新图标
        Sprite.spriteName = skill.Icon;
        LabelName.text = skill.Name;
        Btn.normalSprite = skill.Icon;
        obj[0] = skill;
        obj[1] = LabelName;
    }
    /// <summary>
    /// ngui 自己的
    /// </summary>
    /// <param name="isPress"></param>
    void OnClick()  
    {
        transform.parent.SendMessage("OnSkillClick", obj);    // to skillui.cs
    }
    public void UpdateUiShow()
    {
        transform.parent.SendMessage("OnSkillClick", obj);    // to skillui.cs
    }

   
}

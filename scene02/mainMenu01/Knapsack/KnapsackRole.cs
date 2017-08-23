using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色属性面板
/// </summary>
public class KnapsackRole : MonoBehaviour {

    private KnapsackRoleEquip helmEquip;
    private KnapsackRoleEquip clothEquip;
    private KnapsackRoleEquip weaponEquip;
    private KnapsackRoleEquip shoesEquip;
    private KnapsackRoleEquip necklackEquip;
    private KnapsackRoleEquip braceletEquip;
    private KnapsackRoleEquip ringEquip;
    private KnapsackRoleEquip wingEquip;

    private UILabel hpLabel;
    private UILabel damageLabel;
    private UILabel expLabel;
    private UISlider expSlider;

    void Awake()
    {
        helmEquip = transform.Find("HelmSprite").GetComponent<KnapsackRoleEquip>();
        clothEquip = transform.Find("ClothSprite").GetComponent<KnapsackRoleEquip>();
        weaponEquip = transform.Find("WeaponSprite").GetComponent<KnapsackRoleEquip>();
        shoesEquip = transform.Find("ShoesSprite").GetComponent<KnapsackRoleEquip>();
        necklackEquip = transform.Find("NecklaceSprite").GetComponent<KnapsackRoleEquip>();
        braceletEquip = transform.Find("BraceletSprite").GetComponent<KnapsackRoleEquip>();
        ringEquip = transform.Find("RingSprite").GetComponent<KnapsackRoleEquip>();
        wingEquip = transform.Find("WingSprite").GetComponent<KnapsackRoleEquip>();

        hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
        expLabel = transform.Find("ExpSlider/Label").GetComponent<UILabel>();
        expSlider = transform.Find("ExpSlider/Slider").GetComponent<UISlider>();
        //注册事件
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

        
    }

    void OnDestory()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    /// <summary>
    /// 更新来自playinfo里的更新
    /// </summary>
    /// <param name="Type"></param>
    void OnPlayerInfoChanged(PlayerInfo.InfoType Type)
    {
        if (Type == PlayerInfo.InfoType.All || Type==PlayerInfo.InfoType.Hp|| Type == PlayerInfo.InfoType.Damage || Type == PlayerInfo.InfoType.Exp||Type==PlayerInfo.InfoType.Equip)
        {
            UpdateShow();
        }
    }
    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;

        //helmEquip.SetId(info.HelmID);
        //clothEquip.SetId(info.ClothID);
        //weaponEquip.SetId(info.WeaponID);
        //shoesEquip.SetId(info.ShoesID);
        //necklackEquip.SetId(info.NecklaceID);
        //braceletEquip.SetId(info.BraceletID);
        //ringEquip.SetId(info.RingID);
        //wingEquip.SetId(info.WingID);

        ///更新装备位置显示，调用knapsckRoleEquip的函数
        helmEquip.SetInventoryItem(info.helmInventoryItem);
        clothEquip.SetInventoryItem(info.clothInventoryItem);
        weaponEquip.SetInventoryItem(info.weaponInventoryItem);
        shoesEquip.SetInventoryItem(info.shoesInventoryItem);
        necklackEquip.SetInventoryItem(info.necklaceInventoryItem);
        braceletEquip.SetInventoryItem(info.braceletInventoryItem);
        ringEquip.SetInventoryItem(info.ringInventoryItem);
        wingEquip.SetInventoryItem(info.wingInventoryItem);

        hpLabel.text = info.HP.ToString();
        damageLabel.text = info.Damage.ToString();
        expSlider.value = (float)info.ExpNum / GameController.GetRequireExpByLevel(info.LevelNums + 1);
        expLabel.text = info.ExpNum + "/" + GameController.GetRequireExpByLevel(info.LevelNums + 1);
    }

}

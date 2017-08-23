using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar : MonoBehaviour {

    private UILabel coinLabel;
    private UILabel diamonLabel;
    private UIButton coinPlusButton;
    private UIButton diamonPlusButton;
    
    void Awake()
    {
        
        coinLabel = transform.Find("CoinSprite/Label").GetComponent<UILabel>();
        diamonLabel = transform.Find("DiamonSprite/Label").GetComponent<UILabel>();
        coinPlusButton = transform.Find("CoinSprite/btn-CoinPluss").GetComponent<UIButton>();
        diamonPlusButton = transform.Find("DiamonSprite/btn-DiamonPluss").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    void Start()
    {
        
    }

    void OnDestory()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(PlayerInfo.InfoType type)
    {
        if (type == PlayerInfo.InfoType.All || type == PlayerInfo.InfoType.Coin || type == PlayerInfo.InfoType.Diamond)
            UpdateShow();
        
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        coinLabel.text = info.CoinNum.ToString();
        diamonLabel.text = info.DiamondNum.ToString();
    }
}

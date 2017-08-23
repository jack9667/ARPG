using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShow : MonoBehaviour {

    private float startValue = 0;
    private int endValue = 0;
    private bool isStart = false;
    private bool isUp = true;//是否增加
    private UILabel numLabel;
    private int speed = 100;

    public static PowerShow instance;

    void Awake()
    {
        instance = this;
        numLabel=transform.Find("Label").GetComponent<UILabel>();
        
    }

    void Start()
    {
        numLabel.text = "战力"+ PlayerInfo._instance.PowerNum;//更新战力
    }
    void Update() {
        if (isStart)
        {
            if (isUp)
            {
                startValue += speed;// * Time.deltaTime;
                if (startValue >= endValue) //到达终点
                {
                    isStart = false;
                    startValue = endValue;
                }
            }
            else
            {
                startValue -= speed;// * Time.deltaTime;
                if (startValue <= endValue) //到达终点
                {
                    isStart = false;
                    startValue = endValue;
                }
            }
            numLabel.text = "战力"+(int)startValue;
            //PlayerInfo._instance.PowerNum = int.Parse(numLabel.text.Substring(2));
            //PlayerInfo._instance.OnPlayerInfoChanged(PlayerInfo.InfoType.All);
        }
       
    }

    public void ShowPowerChange(int s,int e )
    {
        this.startValue = s;
        this.endValue = e;
        if (e > s)
            isUp = true;
        else
            isUp = false;
        isStart = true;
    }
}

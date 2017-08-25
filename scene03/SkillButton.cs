using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour {

    public PosType posType = PosType.Basic;
    public float coldTime = 4;
    private float remainTime = 0;    //end time

    private UISprite maskSp;
    private UIButton btn;
    private PlayerAnimation playerAnimation;

    private bool isOtherOn = false;
    private bool isTimeing = false;


    void Start()
    {
        playerAnimation = TranscriptManager.instance.player.GetComponent<PlayerAnimation>();
        btn = this.GetComponent<UIButton>();
        if(posType!=PosType.Basic)
        maskSp = transform.Find("Mask").GetComponent<UISprite>();
    }

	// Update is called once per frame
	void Update () {
        if (isOtherOn&&!isTimeing)
            return;

        if (maskSp == null||posType==PosType.Basic)
            return;

        if (remainTime > 0)
        {
            //this.GetComponent<Collider>().enabled = false;
            //btn.SetState(UIButtonColor.State.Normal, true);
            isTimeing = true;
            remainTime -= Time.deltaTime;
            maskSp.fillAmount = remainTime / coldTime;  //滑动比例
        }
        else
        {
            //this.GetComponent<Collider>().enabled = true;
            maskSp.fillAmount = 0;
            isTimeing = false;
        }

        if (isTimeing)
        {
            this.GetComponent<Collider>().enabled = false;
            btn.SetState(UIButtonColor.State.Normal, true);
        }
        else
        {
            this.GetComponent<Collider>().enabled = true;
        }




    }

    //把时间发送给playeranimtion
    void OnPress(bool isPress)
    {

        playerAnimation.OnAttackButtonClick(isPress, posType);  //to playeranimtion
        if (isPress)
        {
            remainTime = coldTime;
        }
    }

    public void Disable()
    {
        isOtherOn = true;
        this.GetComponent<Collider>().enabled = false;
        btn.SetState(UIButtonColor.State.Normal, true);
    }
    public void Enable()
    {
        isOtherOn = false;
        this.GetComponent<Collider>().enabled = true;
    }

}

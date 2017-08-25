using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator ainm;



	// Use this for initialization
	void Start () {
        ainm = this.GetComponent<Animator>();
	}
	
	// 播放别的动画时候不动
	void Update () {
    }

    public void OnAttackButtonClick(bool isPress,PosType posType)   //from skillbutton
    {
        
        if (posType == PosType.Basic)
        {
            if (isPress)//鼠标按下 true
            {
                ainm.SetTrigger("attack");
            }
        }
        else
        {
            ainm.SetBool("skill" + (int)posType, isPress);
        }

    }
}

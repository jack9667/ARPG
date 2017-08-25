using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    public static Combo instance;
    private float comboTime = 2;    //连击时间之内攻击 数字增加
    private float timer;
    private int comboCount; //连击数

    private UILabel numLabel;

    void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        numLabel = transform.Find("Label").GetComponent<UILabel>();
    }

    public void ComboPlus()
    {
        this.gameObject.SetActive(true);
        timer = comboTime;
        comboCount++;
        numLabel.text = comboCount.ToString();
        transform.localScale = Vector3.zero;
        iTween.ScaleTo(this.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.2f);
        iTween.ShakePosition(this.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
            comboCount = 0;
        }
    }
}

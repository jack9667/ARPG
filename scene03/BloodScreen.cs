using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScreen : MonoBehaviour {

    public static BloodScreen instance;
    private UISprite sp;
    private TweenAlpha ta;

    void Awake()
    {
        instance = this;
        sp = GetComponent<UISprite>();
        ta = GetComponent<TweenAlpha>();
    }

    public void Show()
    {
        sp.alpha = 0.5f;
        ta.ResetToBeginning();
        ta.PlayForward();   
    }
}

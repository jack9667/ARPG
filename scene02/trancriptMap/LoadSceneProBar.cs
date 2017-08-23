using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneProBar : MonoBehaviour {

    public static LoadSceneProBar instance;

    private UISprite bgSprite;

    private UISlider proBar;
    private bool isAsync = false;
    private AsyncOperation ao = null;

    void Awake()
    {
        instance = this;
        proBar = transform.Find("BgSprite/proBar").GetComponent<UISlider>();
        bgSprite = transform.Find("BgSprite").GetComponent<UISprite>();
        bgSprite.gameObject.SetActive(false);
        
        //Application.LoadLevelAsync();
    }

    void Update()
    {
        if (isAsync)
        {
            proBar.value = ao.progress;
        }
    }
    public void Show(AsyncOperation ao)
    {
        bgSprite.gameObject.SetActive(true);
        isAsync = true;
        this.ao = ao;
    }

    
}

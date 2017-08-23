using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginUI : MonoBehaviour {

    public GameObject start;

	public void CloseThis()
    {
        this.gameObject.SetActive(false);
        start.gameObject.SetActive(true);
    }

    public void LoadTourist()
    {
        this.gameObject.SetActive(false);
        StartmenuController.Instance.LoginInGameSelect();
    }


}

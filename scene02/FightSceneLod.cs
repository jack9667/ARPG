using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightSceneLod : MonoBehaviour {

    public static FightSceneLod instance;
    public UITexture mapSpr;

    void Awake()
    {
        instance = this;
    }

    public void LoadFitScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(2);
        LoadSceneProBar.instance.Show(op);
    }

    public void LoadMap()
    {
        mapSpr.gameObject.SetActive(true);
    }
    public void OnFightClick()
    {
        PlayerAutoMove.instance.SetTranscriptDes(NPCManager.instance.transpritGo.transform.position);
    }
}

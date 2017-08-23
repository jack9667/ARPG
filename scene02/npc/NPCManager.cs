using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    public GameObject[] npcArray;
    public static NPCManager instance;

    public GameObject transpritGo;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject GetnpcById()
    {
        return npcArray[0];
    }
}

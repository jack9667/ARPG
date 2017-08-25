using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptManager : MonoBehaviour {

    public static TranscriptManager instance;
    public GameObject player;
    public List<GameObject> enemyList = new List<GameObject>();

    public GameObject s1Btn;
    public GameObject s2Btn;
    public GameObject s3Btn;
    public GameObject AttackBtn;

    private int ilook;

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
	// Use this for initialization
	void Start () {

    }
	

	
}

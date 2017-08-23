using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int GetRequireExpByLevel(int lv)
    {
        return lv * 1000;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPosArray;
    public float time = 0;  //多少秒之后生成
    public float repeateRate = 0;   //循环生成秒数

    private bool isEnter= false;
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(time);

        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject.Instantiate(enemyPrefabs[i], spawnPosArray[i].position, Quaternion.identity);
            yield return new WaitForSeconds(repeateRate);

        }
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.tag == "Player"&&!isEnter)
            StartCoroutine(SpawnEnemy());
        isEnter = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public Transform spawnPoint;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int spawnNumber;

    void Start()
    {
        StartCoroutine(EnemyDrop());

    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < spawnNumber)
        {
            xPos = Random.Range(-10,10);
            zPos = Random.Range(-10,10);
            Instantiate(theEnemy, new Vector3(spawnPoint.position.x+xPos,0, spawnPoint.position.z+zPos),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;

        }
    }
}

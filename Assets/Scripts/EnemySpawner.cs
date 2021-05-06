using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int enemyCount = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 0.2f;
    private Coroutine coroutine;

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, new Vector3(start.position.x, 1, start.position.z), Quaternion.identity);
                enemyCount++;
                yield return new WaitForSeconds(wave.rate);
            }
            while (enemyCount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (enemyCount > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

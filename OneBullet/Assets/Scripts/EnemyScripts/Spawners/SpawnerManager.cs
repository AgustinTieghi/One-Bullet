using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerManager : MonoBehaviour
{
    public List<EnemySpawner> spawners;
    public float countdown;
    public int index;
    public float initialTime;
    public float currentTime;
    public string WonScene = "WonScene";
    private void Start()
    {
        initialTime = countdown;
        StartCoroutine(ActivateSpawners());
        print("Wave 1 Started!");
    }
    void Update()
    {
        currentTime = countdown -= Time.deltaTime;
        if (/*currentTime < initialTime - initialTime * 0.25 && */index == 0)
        { 
            index++;
            StartCoroutine(ActivateSpawners());
            print("Wave 2 Started!");
        }

        if (currentTime < initialTime - initialTime * 0.35 && index == 1)
        {
            index++;
            StartCoroutine(ActivateSpawners());
            print("Wave 3 Started!");
        } 
        
        if (currentTime < initialTime - initialTime * 0.50 && index == 2)
        {          
            foreach (var spawner in spawners)
            {
                spawner.GetComponent<EnemySpawner>().spawnRate = 1;
                spawner.GetComponent<EnemySpawner>().zombieSpeed = 2;
            }
            index++;
            print("Wave 4 Started!");
            StartCoroutine(ActivateSpawners());
        }

        if (currentTime < 0)
        {
            SceneManager.LoadScene(WonScene);
        }
    }


    IEnumerator ActivateSpawners()
    {
        spawners[index].gameObject.SetActive(true);
        yield return null;
    }
    IEnumerator NextWave()
    {
        yield return null;
    }
}

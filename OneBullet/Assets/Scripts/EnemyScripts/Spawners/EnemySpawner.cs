using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public float timer;
    public GameObject zombie;
    public float spawnRate;
    public Transform target;
    public List<GameObject> zombies;
    public float randomIndex;
    public RuntimeAnimatorController zombieController1;
    public RuntimeAnimatorController zombieController2;
    public int maxEnemies;
    public float zombieSpeed;

    private void Start()
    {
        EnemyBrain.OnDeath += RemoveZombiesFromList;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate && zombies.Count <= maxEnemies - 1)
        {
            GameObject spawnedZombie = Instantiate(zombie, this.transform); 
            randomIndex = Random.Range(0, 2);
            if (randomIndex == 0)
            {
                spawnedZombie.GetComponent<Animator>().runtimeAnimatorController = zombieController1 as RuntimeAnimatorController;
            }
            else
            {
                spawnedZombie.GetComponent<Animator>().runtimeAnimatorController = zombieController2 as RuntimeAnimatorController;
            }
            zombies.Add(spawnedZombie);
            spawnedZombie.GetComponent<EnemyBrain>().target = this.target;
            spawnedZombie.GetComponent<NavMeshAgent>().speed = zombieSpeed;
            timer = 0;
        }    
        
    }

    public void RemoveZombiesFromList()
    {
        foreach (GameObject zombie in zombies.ToList())
        {          
            if (zombie == null || zombie.GetComponent<EnemyBrain>().enemyHP <= 0 )
            {
                zombies.Remove(zombie);
            }
        }
    }
}

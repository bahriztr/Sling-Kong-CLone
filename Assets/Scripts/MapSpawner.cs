using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;
    public int count = 0;
    private Transform player;
    private GameObject last;
    private bool mapCreated = false;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        //MapSpawn();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position , player.transform.position) < 5)
        {
            if (!mapCreated)
            {
                SpawnTemp(transform.position);
                mapCreated = true;
            }
        }

        // if (player.transform.position.y > transform.position.y + 10)
        // {
        //     if (gameObject.tag =="obstacle")
        //     {
        //         objectPooler.SetObject(gameObject, false);
        //     }
        //     else if (gameObject.tag == "question")
        //     {
        //         objectPooler.SetObject(gameObject, true);
        //     }
        // }
    }


    public void MapSpawn()
    {
        
        if (count == 0)
        {
            for (int i = 1; i <= objectPooler.questMapSize; i++)
            {
                GameObject go = objectPooler.GetObject(true);
                go.transform.position = new Vector3(transform.position.x, transform.position.y + 15.8f * i, transform.position.z);
            }

            count = 1;
        }
        else if (count == 1)
        {
            for (int i = 1; i <= objectPooler.obstacleMapSize; i++)
            {
                GameObject go = objectPooler.GetObject(false);
                go.transform.position = new Vector3(transform.position.x, transform.position.y + 15.8f * i, transform.position.z);
            }

            count = 0;
        }
    }

    public void SpawnTemp(Vector3 pos)
    {
        
<<<<<<< HEAD
        int rnd = Random.Range(0, 6);
        if (rnd == 5)
=======
        int rnd = Random.Range(0, 10);
        if (rnd >= 4)
>>>>>>> main
        {
            
            last = objectPooler.GetObject(true);
            last.transform.position = new Vector3(transform.position.x, pos.y + 15.8f, transform.position.z);
        }
        else
        {
            last = objectPooler.GetObject(false);
            last.transform.position = new Vector3(transform.position.x, pos.y + 15.8f, transform.position.z);
            
        }
    }
}

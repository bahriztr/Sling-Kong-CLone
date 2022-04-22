using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [SerializeField] private GameObject[] questMapPrefab;
    [SerializeField] private GameObject[] obstacleMapPrefab;
    public List<GameObject> questMaps = new List<GameObject>();
    public List<GameObject> obstacleMaps = new List<GameObject>();
    private GameObject go;
    private Vector3 mapVector;
    public int questMapSize;
    public int obstacleMapSize;
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < questMapPrefab.Length; i++)
        {
            GameObject obj = Instantiate(questMapPrefab[i]);
            obj.SetActive(false);
            questMaps.Add(obj);
        }
        for (int i = 0; i < obstacleMapPrefab.Length; i++)
        {
            GameObject obj = Instantiate(obstacleMapPrefab[i]);
            obj.SetActive(false);
            obstacleMaps.Add(obj);
        }
        questMapSize = questMaps.Count;
        obstacleMapSize = obstacleMaps.Count;
    }
    
    public GameObject GetObject(bool isQuestion)
    {
        if (isQuestion)
        {
            if (questMaps.Count != 0)
            {
                // go = questMaps[Random.Range(0, questMaps.Count)];
                // go.SetActive(true);
                // questMaps.Remove(go);
                go = Instantiate(questMapPrefab[Random.Range(0, questMapPrefab.Length)]);

                return go;
            }else
                return null;
            
        }
        else
        {
            if (obstacleMaps.Count != 0)
            {
                // go = obstacleMaps[Random.Range(0, obstacleMaps.Count)];
                // go.SetActive(true);
                // obstacleMaps.Remove(go);
                go = Instantiate(obstacleMapPrefab[Random.Range(0, obstacleMapPrefab.Length)]);
                return go;
            }else
                return null;
            
        }
    }

    public void SetObject(GameObject go, bool isQuestion)
    {
        if (isQuestion)
        {
            questMaps.Add(go);
        }
        else
        {
            obstacleMaps.Add(go);
        }
        go.SetActive(false);
        //objectToSpawn.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
    }
}

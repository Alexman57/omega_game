using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    static public RoadGenerator instance;
    Animator animator;

    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    public float speed = 0;
    public int maxRoadCount = 10;
    bool keyPress = false;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        speed = maxSpeed;
        ResetLevel();
        //StartLevel();
    }

    void Update()
    {
        if (speed == 0) return;
        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 10);
        }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartLevel()
    {
        speed = maxSpeed;
        // GameManager.instance.StartGame();
    }

    public void ResetLevel()
    {
        //speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        GameManager.instance.StartGame();
        MapGenerator.instance.ResetMaps();

    }

    public void PauseLevel()
    {
        if (true)
        { 
        
            if (keyPress == false)
             {
                keyPress = true;
                speed = 0;
                Time.timeScale = 0f;
                Debug.Log("pause");
            }
            else if (keyPress == true)
            {
                Debug.Log("NOT pause");
                speed = maxSpeed;
                Time.timeScale = 1f;
                keyPress = false;
            }
        } 
    }
        

}

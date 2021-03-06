using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    float rotationSpeed = 100;


    void Start()
    {
        rotationSpeed += Random.Range(0, rotationSpeed / 4.0f);
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

}

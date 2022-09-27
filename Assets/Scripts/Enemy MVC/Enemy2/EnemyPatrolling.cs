using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public Transform[] spawnPoints;
    int currentPosition;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != spawnPoints[currentPosition].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPoints[currentPosition].position, movementSpeed * Time.deltaTime);
        }
        else
        {
            currentPosition = (currentPosition + 1) % spawnPoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Died");
        }
    }
}

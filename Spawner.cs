using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle;

    private float speed_argument = 4;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        GameObject Obstacle_initiated = Instantiate(obstacle, transform.position, Quaternion.identity);

        StartCoroutine(Spawn());

    }
}

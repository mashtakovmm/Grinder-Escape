using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject[] beltObjects;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Spawn()
    {
        int i = Random.Range(0, beltObjects.Length - 1);
        Vector2 spawnPos = new Vector2(beltObjects[i].transform.position.x, transform.position.y) ;
        Instantiate(prefabs[0], spawnPos, Quaternion.identity, transform);
        yield return new WaitForSeconds(Random.Range(minTime,maxTime));
        StartCoroutine(Spawn());
    }
}

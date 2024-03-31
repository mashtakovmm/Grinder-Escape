using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float diffScale;
    public float DiffScale => diffScale;

    void Start()
    {
        StartCoroutine(IncrementScore());
    }


    IEnumerator IncrementScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            diffScale += 0.01f;
        }
    }
}

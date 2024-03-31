using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float score;
    public float Score => score;

    void Start()
    {
        StartCoroutine(IncrementScore());
    }


    IEnumerator IncrementScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            score += 100; 
        }
    }

}

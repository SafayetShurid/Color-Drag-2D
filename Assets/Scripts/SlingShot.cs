using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{

    public GameObject ball;
    public bool ballReady;
    public Transform hook;

    void Start()
    {
        SpawnNewBall();
    }


    public void SpawnNewBall()
    {
        StartCoroutine(SpawnNewBallRoutine());
    }

    IEnumerator SpawnNewBallRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject go = Instantiate(ball,hook.position,Quaternion.identity);
        
        go.transform.SetParent(this.transform);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField]
    private Balloon balloonPrefab;
    [SerializeField]
    private int numBalloonClusters;
    [SerializeField]
    private List<Balloon> balloonClusters;


    //Calling in awake b/c need to be initialized before other objects. Eg: GameProgress
    void Awake()
    {
        balloonClusters = new List<Balloon>();
        CreateBalloonClusters();
    }

    void CreateBalloonClusters()
    {

        Vector3 boxSize = GameObject.FindGameObjectWithTag("MapBoundary").transform.localScale;
        for (int i = 0; i < numBalloonClusters; i++)
        {

            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-boxSize.x / 2, boxSize.x / 2), 
                UnityEngine.Random.Range(-boxSize.y / 2, boxSize.y / 2),
                UnityEngine.Random.Range(-boxSize.z / 2, boxSize.z / 2)
                );
            Balloon balloonCluster = (Balloon)Instantiate(balloonPrefab, randomPosition, Quaternion.identity);
            balloonCluster.name = "BalloonCluster";
            LinearFlight flight = balloonCluster.GetComponent<LinearFlight>();
            flight.rotateRamdonly(360);

            balloonClusters.Add(balloonCluster);
        }
    }

    public List<Balloon> getBalloonClusters()
    {
        return balloonClusters;
    }
}

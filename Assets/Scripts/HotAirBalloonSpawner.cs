using UnityEngine;
using System.Collections;

public class HotAirBalloonSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject hotAirBalloonPrefab;
    private readonly float SPEED_UP_FACTOR = 1.3f;
    private Vector3 boxSize;

    void Start()
    {
        boxSize = GameObject.FindGameObjectWithTag("MapBoundary").transform.localScale;
    }

    public void DeployHotAirBalloon(bool isSpawnFromRight, bool isSpeedUpEnabled)
    {
        Debug.Log("DeployHotAirBalloon!!!!");
        float spawnX = isSpawnFromRight ? -boxSize.x / 2 : boxSize.x / 2;
        float spawnY = UnityEngine.Random.Range(-boxSize.y / 2, boxSize.y / 2);
        float spawnZ = UnityEngine.Random.Range(-boxSize.z / 2, boxSize.z / 2);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
        Debug.Log("spawnPosition: " + spawnPosition);

        GameObject hotAirBalloon = Instantiate(hotAirBalloonPrefab, spawnPosition, Quaternion.identity) as GameObject;
        hotAirBalloon.name = "hotAirBalloon";
        
        LinearFlight linearFlight = hotAirBalloon.GetComponent<LinearFlight>();
        if (isSpawnFromRight)
        {
            hotAirBalloon.transform.forward = new Vector3(1, 0, 0);
        }
        else
        {
            hotAirBalloon.transform.forward = new Vector3(-1, 0, 0);
        }
        if (isSpeedUpEnabled)
        {
            linearFlight.AdjustSpeedByMultiplicativeFactor(SPEED_UP_FACTOR);
        }
    }
}

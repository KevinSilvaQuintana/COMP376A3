using UnityEngine;
using System.Collections;

public class HotAirBalloon : MonoBehaviour
{

    public float screenOffset;

    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private float balloonOffset;

    private Player player;
    private float shootingCooldown;
    private Score score;


    // Use this for initialization
    void Start()
    {
        shootingCooldown = shootingDelay;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        shootingCooldown += Time.deltaTime;

        if (isOutOfScreen())
        {
            Destroy(gameObject);
        }

        if (shootingCooldown > shootingDelay)
        {
            ShootWaterBalloon();
            shootingCooldown = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            score.AddScoreHotAirBalloon();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            player.Kill();
            Destroy(gameObject);
        }
    }

    private bool isOutOfScreen()
    {
        Vector3 boxSize = GameObject.FindGameObjectWithTag("MapBoundary").transform.localScale;
        if (transform.position.x > boxSize.x + screenOffset || transform.position.x < -boxSize.x - screenOffset)
        {
            return true;
        }
        if (transform.position.y > boxSize.y + screenOffset || transform.position.y < -boxSize.y - screenOffset)
        {
            return true;
        }
        if (transform.position.z > boxSize.z + screenOffset || transform.position.z < -boxSize.z - screenOffset)
        {
            return true;
        }
        return false;
    }

    private void ShootWaterBalloon()
    {
        Vector3 playerDirection = player.transform.position - gameObject.transform.position;

        Quaternion q = Quaternion.FromToRotation(Vector3.up, playerDirection);
        GameObject waterBalloon = (GameObject)Instantiate(waterBalloonPrefab, transform.position, q);
        waterBalloon.name = "WaterBallooon";
        waterBalloon.transform.forward = playerDirection;        
    }
}

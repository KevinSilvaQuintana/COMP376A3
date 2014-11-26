using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour
{
    public Balloon balloonPrefab;
    public float sizeDecrement;
    public bool isAlive;
    public int balloonsInCluster;

    private Player player;
    private Score score;
    private GameProgress progress;

    // Use this for initialization
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        progress = GameObject.FindGameObjectWithTag("GameProgress").GetComponent<GameProgress>();
        Color newColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        GameObject collidingObject = col.gameObject;
        if (collidingObject.tag == "Missile")
        {
            // Destroy the missile
            Destroy(collidingObject);
            Pop();
        }
        else if (collidingObject.tag == "Player")
        {
            player.Kill();
        }
    }

    void Pop()
    {
        if (balloonsInCluster == 1)
        {
            Die();
            score.AddScoreLastBalloon();
        }
        else
        {
            CreateNewBalloon();
            CreateNewBalloon();
            Die();
            score.AddScoreSplitBalloon();
        }
    }

    void Die()
    {
        progress.DecrementBalloons();
        isAlive = false;
        Destroy(gameObject);
    }

    private void CreateNewBalloon()
    {
        Balloon newBalloon = (Balloon)Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        newBalloon.name = "BalloonPrefab";
        newBalloon.transform.localScale *= sizeDecrement;
        newBalloon.balloonsInCluster = this.balloonsInCluster / 2;
        LinearFlight flight = newBalloon.GetComponent<LinearFlight>();
        flight.IncrementSpeed();
        flight.rotateRamdonly(180f);
    }
}

using UnityEngine;
using System.Collections;

public class WaterBalloon : MonoBehaviour {

    private Player player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    void OnCollisionEnter(Collision col)
    {
        GameObject collidingObject = col.gameObject;
        if (collidingObject.tag == "Missile")
        {
            // Destroy the missile
            Destroy(collidingObject);
            Destroy(gameObject);
        }
        else if (collidingObject.tag == "Player")
        {
            player.Kill();
            Destroy(gameObject);
        }
    }
}

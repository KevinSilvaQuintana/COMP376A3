using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    [SerializeField]
    private float deathCooldown;
    [SerializeField]
    private float reviveCooldown;

    private Player player;
    private Lives lives;
    public bool isDying = false;
    public bool isReviving = true;

    private float currentTimer;
    private float revivingTimer = 0f;
    private float dyingTimer = 0f;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
        lives = GameObject.FindGameObjectWithTag("Lives").GetComponent<Lives>();
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        if (isDying)
        {
            dyingTimer += Time.deltaTime;
        }
        if (isReviving)
        {
            revivingTimer += Time.deltaTime;
        }
        if (dyingTimer > deathCooldown)
        {
            dyingTimer = 0f;
            isReviving = true;
            isDying = false;
            Respawn();
        }
        if (revivingTimer > reviveCooldown)
        {
            revivingTimer = 0f;
            isReviving = false;
            isDying = false;
            player.RemoveDeadMaterial();
            
        }

    }

    public void Die()
    {
        if (!isReviving)
        {
            isDying = true;
            lives.RemoveLife();
            player.DisableControls();
            player.DisableWrapAround();
            player.DisableCollisions();
            
            if (lives.IsGameOver())
            {
                Application.LoadLevel("GameOver");
            }
        }
        else
        {
            Debug.Log("Invincible frames!");
        }
    }

    private void Respawn()
    {
        player.transform.position = new Vector3(0, 0, 0);
        player.EnableControls();
        player.EnableWrapAround();
        player.EnableCollisions();
        player.ApplyDeadMaterial();
    }
}

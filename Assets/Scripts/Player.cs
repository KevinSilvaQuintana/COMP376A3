using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float maxRangeMouseY = 45f;
	public float turnSpeed = 5f;
	public float moveSpeed = 5f;

	Transform head;

	float rotationX = 0;
	float rotationY = 180f;

    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private float characterOffset;
    [SerializeField]
    private Material redMaterial;

    private float shootingCooldown;
    private bool isControlsEnabled = true;
    private Death playerDeath;
    private Material originalMaterial;
    

	void Awake()
	{
		head = transform.Find("Head").transform;

        //Small fix to allow player to shoot without waiting delay
        shootingCooldown = shootingDelay;
        playerDeath = gameObject.GetComponent<Death>();
        originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
	}

	void Update()
	{
		Screen.lockCursor = true;
		UpdateRotation();
		UpdateMovement();

        shootingCooldown += Time.deltaTime;
        //RegulateVelocity();
        if (isControlsEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("FIRE");
                FireMissile();
            }
        }
	}
	
	void UpdateRotation()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		rotationY += mouseX * turnSpeed * Time.deltaTime;
		rotationX -= mouseY * turnSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, -maxRangeMouseY, maxRangeMouseY);
		transform.rotation = Quaternion.Euler(new Vector3(0 , rotationY, 0));
		head.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, 0));
	}
	
	void UpdateMovement()
	{
		float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("UpDown");
        float axisZ = Input.GetAxis("FrontBack");

        Vector3 playerMove = new Vector3(axisX, axisY, axisZ);
        gameObject.transform.Translate(playerMove * moveSpeed * Time.deltaTime);
	}

    public void Kill()
    {
        playerDeath.Die();
    }

    public void DisableControls()
    {
        isControlsEnabled = false;
    }

    public void EnableControls()
    {
        isControlsEnabled = true;
    }

    public void DisableWrapAround()
    {
        gameObject.GetComponent<WrapAround>().enabled = false;
    }

    public void EnableWrapAround()
    {
        gameObject.GetComponent<WrapAround>().enabled = true;
    }

    public void DisableCollisions()
    {
        //gameObject.GetComponent<IgnoreCollision>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableCollisions()
    {
        //gameObject.GetComponent<IgnoreCollision>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ApplyDeadMaterial()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = redMaterial;
    }

    public void RemoveDeadMaterial()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = originalMaterial;
    }

    private void FireMissile()
    {
        if (shootingCooldown > shootingDelay)
        {
            Vector3 missileDirection = head.position + head.forward * characterOffset;
            GameObject newMissile = (GameObject)Instantiate(missilePrefab, missileDirection, head.rotation);
            shootingCooldown = 0;
        }
    }
}

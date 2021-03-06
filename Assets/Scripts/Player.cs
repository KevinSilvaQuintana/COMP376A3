﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float maxRangeMouseY = 45f;
	public float turnSpeed = 5f;
	public float moveSpeed = 5f;
    public float trajectoryOffset;

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
    private Material deadMaterial;

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

        shootingCooldown += Time.deltaTime;
        if (isControlsEnabled)
        {
            UpdateRotation();
            UpdateMovement();
            if (Input.GetButtonDown("Fire1"))
            {
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
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnableCollisions()
    {
        //gameObject.GetComponent<IgnoreCollision>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void ApplyDeadMaterial()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in meshRenderers)
        {
            m.material = deadMaterial;
        }
    }

    public void RemoveDeadMaterial()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in meshRenderers)
        {
            m.material = originalMaterial;
        }
    }

    private void FireMissile()
    {
        if (shootingCooldown > shootingDelay)
        {
            Vector3 missileDirection = head.position + head.forward * characterOffset;
            GameObject missile = (GameObject)Instantiate(missilePrefab, missileDirection, head.rotation);

            Quaternion upTrajectory = Quaternion.AngleAxis(30, new Vector3(-1, 0, 0));
            missile.transform.rotation = missile.transform.rotation * upTrajectory;
            shootingCooldown = 0;
        }
    }
}

using UnityEngine;
using System.Collections;

public class LinearFlight : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float speedIncrement;
    [SerializeField]
    public Vector3 flightDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public void IncrementSpeed()
    {
        speed += speedIncrement;
    }

    public void AdjustSpeedByMultiplicativeFactor(float factor)
    {
        speed *= factor;
    }

    public void rotateRamdonly(float maxDegree)
    {
        Quaternion q = CreateRotationWithMaxDegree(maxDegree);
        transform.forward = q * transform.forward;
    }

    private Quaternion CreateRotationWithMaxDegree(float maxDegree)
    {
        Vector3 rotationAxis = new Vector3(Random.value, Random.value, Random.value).normalized;
        float degree = Random.Range(0f, maxDegree);
        return Quaternion.AngleAxis(degree, rotationAxis);
    }
}

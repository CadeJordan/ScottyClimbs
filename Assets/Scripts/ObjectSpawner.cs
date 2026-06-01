using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 3f;
    public float spawnRadiusX = 3f;
    public float spawnRadiusZ = 3f;
    public float spawnHeight = 0f;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 6f;
    public float obstacleLifetime = 10f;
    public float x = 0f;
    public float z = 0f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        Vector3 spawnPos = transform.position + GetEdge();
        GameObject obs = Instantiate(obstaclePrefab, spawnPos, Random.rotation);

        Rigidbody rb = obs.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.down * Random.Range(minFallSpeed, maxFallSpeed);

        Destroy(obs, obstacleLifetime);
    }

    Vector3 GetEdge()
    {
        int edge = Random.Range(0,4);

        switch (edge)
        {
            case 0:
                x = Random.Range(-spawnRadiusX, spawnRadiusX);
                z = spawnRadiusZ;
                break;
            case 1:
                x = Random.Range(-spawnRadiusX, spawnRadiusX);
                z = -spawnRadiusZ;
                break;
            case 2:
                x = -spawnRadiusX;
                z = Random.Range(-spawnRadiusZ, spawnRadiusZ);
                break;
            case 3:
                x = spawnRadiusX;
                z = Random.Range(-spawnRadiusZ, spawnRadiusZ);
                break;
        }
        return new Vector3(x, 0f, z);
    }
}
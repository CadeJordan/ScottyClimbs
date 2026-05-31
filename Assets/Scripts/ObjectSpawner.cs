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
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-spawnRadiusX, spawnRadiusX), 0f, Random.Range(-spawnRadiusZ, spawnRadiusZ));

        GameObject obs = Instantiate(obstaclePrefab, spawnPos,
                                     Random.rotation);

        Rigidbody rb = obs.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.down * Random.Range(minFallSpeed, maxFallSpeed);

        Destroy(obs, obstacleLifetime);
    }
}
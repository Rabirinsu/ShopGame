using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public List<Vector3> spawnPoints;
    public List<int> spawnRotations;
    public Customer buyerCustomer;
    public Customer sellerCustomer;
    public Customer BuyerBoss;
    [SerializeField] private float BossSpawnTimer = 15;
     private const float BossSpawnDelay = 60;
    [SerializeField] private int minSpawnTime;
    [SerializeField] private int maxSpawnTime;
    [SerializeField] private int currentSpawnTime;
    private float timer;

    public List<GameObject> SpawnedCustomers;
    public int maxCustomerLimit;

    private void Update()
    {
        BossSpawnTimer -= Time.deltaTime;
       if(SpawnedCustomers.Count < maxCustomerLimit )
        {
            timer += Time.deltaTime;
            if (timer >= currentSpawnTime)
                SpawnCustomers();
        }
    }

   private void SpawnCustomers()
    {
        timer = 0;
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        var spawnPointIndex = Random.Range(0, spawnPoints.Count);
        if (BossSpawnTimer <= 0)
        {
            var BossClone = Instantiate(BuyerBoss.customerObject, spawnPoints[spawnPointIndex], Quaternion.Euler(0, spawnRotations[spawnPointIndex], 0));
            SpawnedCustomers.Add(BossClone);
            BossSpawnTimer = BossSpawnDelay;
            return;
        }
        if(Random.Range(0,2) == 0)
        {
            var sellercustomerClone = Instantiate(sellerCustomer.customerObject, spawnPoints[spawnPointIndex], Quaternion.Euler(0, spawnRotations[spawnPointIndex], 0));
            SpawnedCustomers.Add(sellercustomerClone);
            return;
        }
         var buyercustomerClone = Instantiate(buyerCustomer.customerObject, spawnPoints[spawnPointIndex], Quaternion.Euler(0, spawnRotations[spawnPointIndex], 0));
         SpawnedCustomers.Add(buyercustomerClone);



    }   
}

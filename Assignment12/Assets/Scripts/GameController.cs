 using UnityEngine;
 using Mirror;
 
 public class GameController : NetworkBehaviour 
 {
 	public GameObject enemyPrefab;
 
 	private float spawnEnemyTime = 0;
    private int direction;
 
 	void Update () 
	{
 		if(isServer) 
		{
 			if(Time.fixedTime>spawnEnemyTime) 
			{
                direction = Random.Range(0, 2);
                SpawnEnemy();
 			}
 		}
 	}
 
 	[Server]
 	public void SpawnEnemy() 
	{
 		Vector3 position = new Vector3(Random.Range(-6.75f,6.75f),Random.Range(1.0f,8.0f),Random.Range(4.5f, 15.0f));
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, position, Quaternion.identity);
 		NetworkServer.Spawn(enemy);
 		spawnEnemyTime = Time.fixedTime + Random.Range(2,4);
 	}
 }

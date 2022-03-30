using UnityEngine;
using Mirror;
 	
public class Bullet : NetworkBehaviour {
	
	[SyncVar]
	public Color color;
	
	[SyncVar]
	public uint parentNetId;
 			
	public override void OnStartClient() 
	{
		gameObject.GetComponent<Renderer>().material.color = color;
 	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(isServer) 
		{
 			Player player = NetworkIdentity.spawned[parentNetId].GetComponent<Player>();
 			player.score += 50;
 			Destroy(other.gameObject);
 		}
	}
}

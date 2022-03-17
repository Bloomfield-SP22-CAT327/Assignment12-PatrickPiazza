using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public Color color;
	
	public override void OnStartClient() 
	{
 		gameObject.GetComponent<Renderer>().material.color = color;
 	}
}

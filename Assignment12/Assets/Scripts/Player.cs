using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 1.875f;
	public GameObject bulletPrefab;
	private Text scoreText;
	
	[SyncVar]
	public int score;
	
	[SyncVar]
    public Color color;

	void Start()
    {
		score += 500;
    }
	
	void Update() 
	{
 		if(isLocalPlayer && hasAuthority) 
		{
 			GetInput();
			scoreText.text = "Score: " + score;
 		}
 	}
	
	public override void OnStartClient() 
	{
 		gameObject.GetComponent<Renderer>().material.color = color;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
 	}
	
	void GetInput() 
	{
		float x = Input.GetAxisRaw("Horizontal") * (moveSpeed * 2.0f) * Time.deltaTime;
		float y = Input.GetAxisRaw("Vertical") * (moveSpeed * 2.0f) * Time.deltaTime;
		
	if(Input.GetButtonDown("Fire1") && score > 0)
	{
		CmdDoFire();
		score -= 25;
	}
	
	if(isServer)
	{
		RpcMoveIt(x,y);
	}
	else
	{
		CmdMoveIt(x,y);
	}
}
 
 	[ClientRpc]
 	void RpcMoveIt(float x, float y) 
	{
 		transform.Translate(x,y,0);
 	}
 	
 	[Command]
 	public void CmdMoveIt(float x, float y) 
	{
 		RpcMoveIt(x,y);
 	}
	
	[Command]
 	public void CmdDoFire() 
	{ 
 		GameObject bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position + this.transform.right, Quaternion.identity);
 		bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * 17.5f;
 		bullet.GetComponent<Bullet>().color = color;
		bullet.GetComponent<Bullet>().parentNetId = this.netId;
 		Destroy(bullet,0.875f);
 		NetworkServer.Spawn(bullet);
 	}
}

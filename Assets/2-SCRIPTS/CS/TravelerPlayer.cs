using UnityEngine;
using System.Collections;

public class TravelerPlayer : GenericTraveler {

	static public TravelerPlayer singletonInstance;

	public bool hasCollided;
	
	public Vector3 dist;
	
	private int collisionCounter = 0;

	private GameObject bulletTemp;
	public GameObject bulletPrefab;

	public GameObject ColliderPlayer;

	public float speed = 1.0f;
	public float timePressed = 0;

	private int playerScore = 0;
	private int playerDistance = 0;


	void Start(){

		singletonInstance = this;

		hasCollided = false;

	}
	
	void Update(){

		#if !UNITY_IPHONE && !UNITY_ANDROID
		if (Input.GetKey(KeyCode.LeftArrow) && TrackManager.singletonInstance.inGame){
			Beattles(-1, 180 * speed * (genericSpeed / 3.0f));

			if(timePressed < 0.5f){
				timePressed += Time.deltaTime;
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow) && TrackManager.singletonInstance.inGame){
			Beattles(1, 180 * speed * (genericSpeed / 3.0f));

			if(timePressed > -0.5f){
				timePressed -= Time.deltaTime;
			}
		}else{

			if(timePressed > 0){
				timePressed -= Time.deltaTime * genericSpeed;

				if(timePressed < 0) timePressed = 0;

			} else if(timePressed < 0){
				timePressed += Time.deltaTime * genericSpeed;
				if(timePressed > 0) timePressed = 0;
			}

		}

		Vector3 rot = Vector3.zero;

		if(timePressed > 0)	rot = new Vector3(0,0,Mathf.Lerp(0,30, timePressed));
		if(timePressed < 0)	rot = new Vector3(0,0,Mathf.Lerp(0,-30, timePressed * -1));
		
		ColliderPlayer.transform.localEulerAngles = rot;


		if (Input.GetKeyDown(KeyCode.Space)){

			TravelerManager.singletonInstance.spawnPlayerBullet(
				transform.position, transform.rotation, this.pointIndex, this.speed * 2.0f * (genericSpeed / 3.0f));

			//bulletTemp = (GameObject)GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
			//bulletTemp.GetComponent<BulletTraveler>().InitBullet(transform.position, transform.rotation, pointIndex);
			//Debug.Log(transform.position);
			//Debug.Log(transform.rotation);
		}

		#else	
		if(Input.touchCount == 1)
		if (Input.GetTouch(0).position.x < Screen.width/2){
			Beattles(-1, 90);
		}
		else if (Input.GetTouch(0).position.x > Screen.width/2){
			Beattles(1, 90);
		}
		else if (Input.touchCount == 2)
		{
			//TODO DISPARO
		}
		#endif
		
		
		if(DoctorWho(speed)){
			
			TrackManager.singletonInstance.addNewRing();
			//movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - TrackBuilder.singletonInstance.points[pointIndex];
			movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position;

			if(TrackManager.singletonInstance.inGame){
				incrementPlayerDistance(1);

				if (genericSpeed < 6.0f)
				{
					genericSpeed += 0.0055f;
				}
				// Debug.Log(genericSpeed);
			}

		}
	
	}

	public void InitPlayer(int pointIndex) {

		transform.position = TrackBuilder.singletonInstance.points[pointIndex];

		base.InitGeneric(pointIndex);

	}



	public void shootTarget(Transform alvo){
	

		bulletTemp = (GameObject)GameObject.Instantiate(bulletPrefab, ColliderPlayer.transform.position, transform.rotation);

		//bulletTemp.GetComponent<Bullet>().alvo = alvo;

		//Debug.Log(alvo.position);


		//arrumar o easy type
		//iTween.MoveTo(bulletTemp, alvo.position, 5.0f);

		/*
		iTween.MoveTo(bulletTemp, iTween.Hash(
			"x", alvo.position.x,
			"y", alvo.position.y,
			"z", alvo.position.z,
			"easeType", "linear",
			"time", speed
		));
		*/

		iTween.MoveTo(bulletTemp, iTween.Hash(
			"x", alvo.position.x,
			"y", alvo.position.y,
			"z", alvo.position.z,
			"easeType", "linear",
			"time", 0.3f
			));



	}

	public void setPlayerScore(int score){
		playerScore = score;
	}

	public void incrementPlayerScore(int score){
		playerScore += score;
	}

	public int getPlayerScore(){
		return playerScore;
	}

	public void setPlayerDistance(int dist){
		playerDistance = dist;
	}
	
	public void incrementPlayerDistance(int dist){
		playerDistance += dist;
	}
	
	public int getPlayerDistance(){
		return playerDistance;
	}


	/*
	void OnControllerColliderHit(ControllerColliderHit quem) {
		
		string nome = quem.gameObject.name;
		
		Debug.Log("Colidiu..." + nome);
		
	}
	*/
	
	
	//TrackBuilder.addNewRing();
	
	
	/*
	void OnCollisionEnter(Collision collision){
		Debug.Log("TESTE colisao");

		Debug.Log(collision.gameObject.name);

		if (collision.gameObject.name.CompareTo("EndSegment(Clone)") == 0){
			Debug.Log("FINISH");
		}

		if (collision.gameObject.name.CompareTo("ObstacleCollider") == 0){
			collisionCounter++;
			Debug.Log("BAM! " + collisionCounter.ToString());
		}

		if (collision.gameObject.name.CompareTo("Obstacle(Clone)/ObstacleCollider") == 0){
			collisionCounter++;
			Debug.Log("BAM! " + collisionCounter.ToString());
		}

	}
	*/
}



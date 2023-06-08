using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImmovableObjectManager : MonoBehaviour {

	public static ImmovableObjectManager singletonInstance;

	private List<Obstacle> obstacles;
	private List<Wall> walls;
	private List<Wall> walls2;
	private List<Door> doors;

//	private List<PowerUp> powerUps;

	public GameObject wallPrefab;
	public GameObject obstaclePrefab;
	public GameObject doorFourLocksPrefab;
	public GameObject doorThreeLocksPrefab;
	public GameObject doorTwoLocksPrefab;
//	public GameObject powerUpPrefab;

	private int pipeCounterLastMatchStart;

	private int nextDoorLocks;
	private int pipeCounterNextDoor;

	private int obstaclesDistance;
	private int pipeCounterNextObstacle;

	private int wallsDistance;
	private int wallsDistance2;

	private int pipeCounterNextWall;
	private int pipeCounterNextWall2;

	private int doorlessLocksOpened;
     
	public void Awake(){

		singletonInstance = this;

		obstacles = new List<Obstacle>();
		walls = new List<Wall>();
		walls2 = new List<Wall>();
		doors = new List<Door>();
//		powerUps = new List<PowerUp>();

		pipeCounterLastMatchStart = 0;

		doorlessLocksOpened = 0;

		obstaclesDistance = 5;
		wallsDistance = 15;
		wallsDistance2 = 15;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startMatch()
	{
		pipeCounterLastMatchStart = TrackManager.singletonInstance.pipeCounter;

		nextDoorLocks = Random.Range(2, 5);

		pipeCounterNextDoor = doorsDistance() + pipeCounterLastMatchStart;
		pipeCounterNextObstacle = obstaclesDistance + pipeCounterLastMatchStart;
		pipeCounterNextWall = wallsDistance + pipeCounterLastMatchStart;
		pipeCounterNextWall2 = wallsDistance2 + pipeCounterLastMatchStart + 5;
	}

	public void finishMatch()
	{
		foreach (Obstacle obstacle in obstacles)
		{
			obstacle.animateFinishMatch();
		}

		foreach (Wall wall in walls)
		{
			wall.animateFinishMatch();
		}

		foreach (Wall wall2 in walls2)
		{
			wall2.animateFinishMatch();
		}

		foreach (Door door in doors)
		{
			door.animateFinishMatch();
		}
		
		obstacles.Clear();
		walls.Clear();
		walls2.Clear();
		doors.Clear();
	}

	public GameObject spawnObject(GameObject objectPrefab){

		//SPAWN NO FIM DA PISTA
		Vector3 pos = TrackBuilder.singletonInstance.transform.position + (TrackBuilder.singletonInstance.transform.up * 0.9f);

		Quaternion rot = TrackBuilder.singletonInstance.transform.rotation;

		GameObject newObject = (GameObject)GameObject.Instantiate(objectPrefab, pos, rot);
		//GameObject newObject = (GameObject)GameObject.Instantiate(objectPrefab);

		return newObject;
		
	}

	private bool spawnWall(){

		//se esta em ingame
		if(TrackManager.singletonInstance.inGame){
			//se ja esta na hora de criar uma parede
			if((TrackManager.singletonInstance.pipeCounter >= pipeCounterNextWall)
			   //se a posicao atual nao e de uma parede
			   && (TrackManager.singletonInstance.pipeCounter != pipeCounterNextDoor)
			   //se a posicao atual corresponde ao intervalo configurado, evita parede logo depois da porta
			   && (TrackManager.singletonInstance.pipeCounter - pipeCounterNextWall) % wallsDistance == 0){

				walls.Add(spawnObject(wallPrefab).GetComponent<Wall>());
				pipeCounterNextWall += wallsDistance;
				return true;
			}
		}

		return false;
	}

	private bool spawnWall2(){
		
		//se esta em ingame
		if(TrackManager.singletonInstance.inGame){
			//se ja esta na hora de criar uma parede
			if((TrackManager.singletonInstance.pipeCounter >= pipeCounterNextWall2)
			   //se a posicao atual nao e de uma parede
			   && (TrackManager.singletonInstance.pipeCounter != pipeCounterNextDoor)
			   //se a posicao atual corresponde ao intervalo configurado, evita parede logo depois da porta
			   && (TrackManager.singletonInstance.pipeCounter - pipeCounterNextWall2) % wallsDistance2 == 0){
				
				walls2.Add(spawnObject(wallPrefab).GetComponent<Wall>());
				pipeCounterNextWall2 += wallsDistance2;
				return true;
			}
		}
		
		return false;
	}

	private bool spawnObstacle(){
		
		if(TrackManager.singletonInstance.inGame){

			if((TrackManager.singletonInstance.pipeCounter >= pipeCounterNextObstacle)
			   && (TrackManager.singletonInstance.pipeCounter != pipeCounterNextDoor)
			   && (TrackManager.singletonInstance.pipeCounter - pipeCounterNextObstacle) % obstaclesDistance == 0){
				obstacles.Add(spawnObject(obstaclePrefab).GetComponent<Obstacle>());
				pipeCounterNextObstacle += obstaclesDistance;
				return true;
			}
		}
		
		return false;
	}
	
	private bool spawnDoor(){
		
		if(TrackManager.singletonInstance.inGame){

			if(TrackManager.singletonInstance.pipeCounter >= pipeCounterNextDoor){
				int locks = nextDoorLocks;
				if (locks == 4)
				{
					doors.Add(spawnObject(doorFourLocksPrefab).GetComponentInChildren<Door>());
				}
				else if (locks == 3)
				{
					doors.Add(spawnObject(doorThreeLocksPrefab).GetComponentInChildren<Door>());
				}
				else if (locks == 2)
				{
					doors.Add(spawnObject(doorTwoLocksPrefab).GetComponentInChildren<Door>());
				}

				doors[doors.Count - 1].preOpenedLocks = doorlessLocksOpened;

				doorlessLocksOpened = 0;

				nextDoorLocks = Random.Range(2, 5);

				pipeCounterNextObstacle = pipeCounterNextDoor + obstaclesDistance;

				pipeCounterNextDoor += doorsDistance();

				return true;
			}
		}
		
		return false;
	}

//	private bool spawnPowerUp(){
//
//		if(TrackManager.singletonInstance.inGame){
//			
//			if(TrackManager.singletonInstance.pipeCounter % 15 == 0){
//				powerUps.Add(spawnObject(powerUpPrefab).GetComponentInChildren<PowerUp>());
//				return true;
//			}
//		}
//
//		return false;
//	}


	public void newRingAdded(){

		for(int i=0; i < obstacles.Count; i++){
			if(obstacles[i].newRingAdded()){
				obstacles.RemoveAt(i);
			} 
		}
		
		for(int j=0; j < walls.Count; j++){
			if(walls[j].newRingAdded()){
				walls.RemoveAt(j);
			} 
		}

		for(int j=0; j < walls2.Count; j++){
			if(walls2[j].newRingAdded()){
				walls2.RemoveAt(j);
			} 
		}

		for(int j=0; j < doors.Count; j++){
			if(doors[j].newRingAdded()){
				doors.RemoveAt(j);
			} 
		}

//		for(int n=0; n < powerUps.Count; n++){
//			if(powerUps[n].newRingAdded()){
//				powerUps.RemoveAt(n);
//			} 
//		}

		spawnObstacle();
		spawnWall();
		spawnWall2();
		spawnDoor();
//		spawnPowerUp();

	}
	
	public void removeObstacle(Obstacle obs){
		
		obstacles.Remove(obs);
		
	}
	
	public void removeDoor(Door door){
		doors.Remove(door);
	}

	public void OpenLockNextDoor(Obstacle obstacleCollided){
		foreach(Door door in doors){
			if (door.pipeCounter >= obstacleCollided.pipeCounter){
				//Debug.Log("Key: " + obstacleCollided.pipeCounter.ToString());
				//Debug.Log("Door: " + door.pipeCounter.ToString());
				door.OpenNextLock();
				return;
			}
		}

		doorlessLocksOpened++;
	}

	private int doorsDistance(){
		return (nextDoorLocks + 1) * obstaclesDistance;
	}

	public Obstacle GetObstacleAtPipeCounter(int pipeCounter){
		for (int o = 0; o < obstacles.Count; o++) {
			if (obstacles[o].pipeCounter == pipeCounter){
				return obstacles[o];
			}
		}
		return null;
	}

	public Wall GetWall2AtPipeCounter(int pipeCounter){
		for (int w = 0; w < walls2.Count; w++) {
			if (walls2[w].pipeCounter == pipeCounter){
				return walls2[w];
			}
		}
		return null;
	}

	public Wall GetLastWallAtPipeCounter(int pipeCounter){
		Wall last = null;
		for (int w = 0; w < walls.Count; w++) {
			if (walls[w].pipeCounter < pipeCounter && (last != null || last.pipeCounter < walls[w].pipeCounter)){
				last = walls[w];

			}
		}
		return last;
		//return null;
	}

}

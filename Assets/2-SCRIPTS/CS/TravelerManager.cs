using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TravelerManager : MonoBehaviour {

	public static bool hasSpawnedEnemy;

	public static TravelerManager singletonInstance;

	private List<TravelerEnemy> enemies;
	private List<TravelerBullet> bullets;
	private List<TravelerPlayerBullet> playerBullets;

	public GameObject enemyPrefab;
	public GameObject bulletPrefab;
	public GameObject playerBulletPrefab;

	public void Awake(){
		singletonInstance = this;

		enemies = new List<TravelerEnemy>();
		bullets = new List<TravelerBullet>();
		playerBullets = new List<TravelerPlayerBullet>();
	}


	public void Update(){

	}

	public void spawnEnemy(){
		//SPAWN NO FIM DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TrackBuilder.singletonInstance.points.Count - 2];

		//SPAWN NO INICIO DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TravelerPlayer.singletonInstance.pointIndex];
		Vector3 pos = Vector3.zero;

		Quaternion rot = Quaternion.identity;


		GameObject newEnemy = (GameObject)GameObject.Instantiate(enemyPrefab, pos, rot);

		TravelerEnemy scriptTravelerEnemy = newEnemy.GetComponent<TravelerEnemy>();

		//scriptTravelerEnemy.InitEnemy(TravelerPlayer.singletonInstance.pointIndex);
		scriptTravelerEnemy.InitEnemy(TrackBuilder.singletonInstance.points.Count - 5);

		enemies.Add(scriptTravelerEnemy);
	}

	public void spawnBullet(Vector3 pos, Quaternion rot, int pointIndex, float speed){
		//SPAWN NO FIM DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TrackBuilder.singletonInstance.points.Count - 2];
		
		//SPAWN NO INICIO DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TravelerPlayer.singletonInstance.pointIndex];
		//Vector3 pos = Vector3.zero;
		
		//Quaternion rot = Quaternion.identity;
		
		
		GameObject newBullet = (GameObject)GameObject.Instantiate(bulletPrefab, pos, rot);
		
		TravelerBullet scriptTravelerBullet = newBullet.GetComponent<TravelerBullet>();
		
		scriptTravelerBullet.InitBullet(pointIndex, speed);
		
		bullets.Add(scriptTravelerBullet);
	}

	public void spawnPlayerBullet(Vector3 pos, Quaternion rot, int pointIndex, float speed){
		//SPAWN NO FIM DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TrackBuilder.singletonInstance.points.Count - 2];
		
		//SPAWN NO INICIO DA PISTA
		//Vector3 pos = TrackBuilder.singletonInstance.points[TravelerPlayer.singletonInstance.pointIndex];
		//Vector3 pos = Vector3.zero;
		
		//Quaternion rot = Quaternion.identity;
		
		
		GameObject newBullet = (GameObject)GameObject.Instantiate(playerBulletPrefab, pos, rot);
		
		TravelerPlayerBullet scriptTravelerBullet = newBullet.GetComponent<TravelerPlayerBullet>();
		
		scriptTravelerBullet.InitBullet(pointIndex, speed);
		
		playerBullets.Add(scriptTravelerBullet);
	}

	public void newRingAdded(){

//		if(TrackManager.singletonInstance.inGame){
//			if(TrackManager.singletonInstance.pipeCounter % 20 == 0 && enemies.Count < 4){
//				spawnEnemy();
//			}
//		}


		foreach (TravelerEnemy enemy in enemies){
			enemy.newRingAdded();
		}

		/*
		for(int i=0; i < enemies.Count; i++){
			if(enemies[i].newRingAdded()){
				enemies.RemoveAt(i);
			} 
		}
		*/
		
//		for(int n=0; n < bullets.Count; n++){
//			if(bullets[n].newRingAdded()){
//				bullets.RemoveAt(n);
//			} 
//		}
//		
//		for(int n=0; n < playerBullets.Count; n++){
//			if(playerBullets[n].newRingAdded()){
//				playerBullets.RemoveAt(n);
//			} 
//		}

	}

	public void removePlayerBullet(TravelerPlayerBullet playerBulletScript)
	{
		playerBullets.Remove(playerBulletScript);
	}

	public void removeTravelerEnemy(TravelerEnemy enemy)
	{
		enemies.Remove(enemy);
	}
	
}

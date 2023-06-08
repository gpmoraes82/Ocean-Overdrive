using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class GroundClick : MonoBehaviour {
	
	public GameObject road;
	private MeshCollider meshCollider;
	
	public GameObject endPoint;
	
	public Material mat;
	
	public List<Vector3> points = new List<Vector3>();
	//public List<Vector3> pointsDirection = new List<Vector3>();
	
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uv = new List<Vector2>();
	public List<Vector3> normals = new List<Vector3>();
	
	int pipeCounter = 0;
	
	public Vector3 exitPoint;
	public Vector3 exitDirection;
	
	int sides = 4;
	float aux;

	void Start() {
		
		init();
	}
	
	void Update() {
		for(int i = 0; i < points.Count - 1; i++)
			Debug.DrawLine(points[i], points[i+1], Color.red);
		
		for(int j = vertices.Count - 4; j < vertices.Count - 1; j++)
			Debug.DrawLine(vertices[j], vertices[j+1], Color.red);		
		

	}

	void OnMouseDown() {
		
		addNewRing();
		
		//TrackBuilder.singletonInstance.move();
		
		Debug.DrawLine(points[points.Count-2], points[points.Count-1], Color.red);
	}
	
	void init(){
		road = new GameObject ( "road", typeof(MeshFilter), typeof(MeshRenderer));
		road.transform.position = new Vector3(0,0,0);
		
		//pipeCounter++;
		
		/*points.Add(new Vector3(0.0f, 0.0f , 0.0f));
		vertices.Add(new Vector3(-0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f ,  0.5f));
		
		points.Add(new Vector3(0.0f, 1.0f , 0.0f));
		vertices.Add(new Vector3(-0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f,  1.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f ,  0.5f));*/
		
		points.Add(new Vector3(0.0f, 0.0f , 0.0f));
		vertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));
		vertices.Add(new Vector3(-0.5f,  0.5f, 0.0f));
		vertices.Add(new Vector3( 0.5f, -0.5f, 0.0f));
		vertices.Add(new Vector3( 0.5f,  0.5f, 0.0f));
		
		points.Add(new Vector3(0.0f, 0.0f , 1.0f));
		vertices.Add(new Vector3(-0.5f, -0.5f , 1.0f));
		vertices.Add(new Vector3(-0.5f,  0.5f , 1.0f));
		vertices.Add(new Vector3( 0.5f, -0.5f , 1.0f));
		vertices.Add(new Vector3( 0.5f,  0.5f , 1.0f));
		
		triangles.Add(2);
		triangles.Add(0);
		triangles.Add(6);
		
		triangles.Add(6);
		triangles.Add(0);
		triangles.Add(4);
		
		triangles.Add(4);
		triangles.Add(0);
		triangles.Add(5);
		
		triangles.Add(5);
		triangles.Add(0);
		triangles.Add(1);
		
		triangles.Add(7);
		triangles.Add(2);
		triangles.Add(6);
		
		triangles.Add(3);
		triangles.Add(2);
		triangles.Add(7);
		
		triangles.Add(7);
		triangles.Add(1);
		triangles.Add(3);
		
		triangles.Add(5);
		triangles.Add(1);
		triangles.Add(7);
		
		uv.Add(new Vector2(0,0));
		uv.Add(new Vector2(1,0));
		uv.Add(new Vector2(1,0));
		uv.Add(new Vector2(0,0));
		
		uv.Add(new Vector2(0,1));
		uv.Add(new Vector2(1,1));
		uv.Add(new Vector2(1,1));
		uv.Add(new Vector2(0,1));

		
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));

		
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		
		MeshFilter mesh_filter = road.GetComponent<MeshFilter>();
		mesh_filter.mesh = mesh;	
		
		road.AddComponent<MeshCollider>();
		meshCollider = road.GetComponent<MeshCollider>();
		meshCollider.sharedMesh = mesh;
		
		road.GetComponent<Renderer>().material = mat;
		
		for(int i=0; i < 10 ;i++){
			addNewRing();
		}
		
	}
	
	void addNewRing(){
		
		TrackBuilder.singletonInstance.move();
		
		points.Add(TrackBuilder.singletonInstance.gameObject.transform.position); // adiciona ponto de caminho (vertice pai)
	
		pipeCounter++;
		
		//print(pipeCounter);
		
		//centralizou na linha
		vertices.Add(TrackBuilder.singletonInstance.getPointPosition(0));
		vertices.Add(TrackBuilder.singletonInstance.getPointPosition(1));
		vertices.Add(TrackBuilder.singletonInstance.getPointPosition(2));
		vertices.Add(TrackBuilder.singletonInstance.getPointPosition(3));
		
		int vertexIndexOffset = 4 * pipeCounter;
		
		print(vertices.Count);
		
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(6 + vertexIndexOffset);
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(4 + vertexIndexOffset);
		
		triangles.Add(4 + vertexIndexOffset);
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		
		triangles.Add(5 + vertexIndexOffset);
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(1 + vertexIndexOffset);
		
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(3 + vertexIndexOffset);
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(3 + vertexIndexOffset);
		
		triangles.Add(5 + vertexIndexOffset);
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);

		if (pipeCounter % 2 == 1){
			uv.Add(new Vector2(0,0));
			uv.Add(new Vector2(1,0));
			uv.Add(new Vector2(1,0));
			uv.Add(new Vector2(0,0));
		}
		else{
			uv.Add(new Vector2(0,1));
			uv.Add(new Vector2(1,1));
			uv.Add(new Vector2(1,1));
			uv.Add(new Vector2(0,1));
		}


		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));
		normals.Add(new Vector3(0,1,0));

		
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		
		MeshFilter mesh_filter = road.GetComponent<MeshFilter>();
		mesh_filter.mesh = mesh;
		
		meshCollider.sharedMesh = mesh;
		
		road.GetComponent<Renderer>().material = mat;
	}	
	
/*	
	void addNewRing(){
		
		Vector3 pointA;
		Vector3 pointB;
		
		float angX = Random.Range(-0.99f, 0.99f);
		float angZ = Random.Range(-0.99f, 0.99f); 
		
		pointA = points[points.Count-2];
		pointB = points[points.Count-1];
		
		//---------------------------------------------------------------------------------------------------------
		Vector3 tempDirection = pointB - pointA;
		Vector3 copyTempDirection = tempDirection;
		
		
		tempDirection.x = (Mathf.Cos(angZ) * copyTempDirection.x) - (Mathf.Sin(angZ) * copyTempDirection.y);
		tempDirection.y = (Mathf.Sin(angZ) * copyTempDirection.x) + (Mathf.Cos(angZ) * copyTempDirection.y);
		aux = tempDirection.y;
		tempDirection.y = (Mathf.Sin(angX) * copyTempDirection.z) + (Mathf.Cos(angX) * copyTempDirection.y) * aux;
		tempDirection.z = (Mathf.Cos(angX) * copyTempDirection.z) - (Mathf.Sin(angX) * copyTempDirection.y) * aux;
		
		tempDirection.Normalize();
		
		tempDirection += pointB;
		//---------------------------------------------------------------------------------------------------------
		
		points.Add(tempDirection);
		
		endPoint.transform.position = tempDirection;
		
		//endPoint.transform.Rotate(tempDirection, angZ);
		endPoint.transform.Rotate(pointB, angZ * 57.2957795f);
		endPoint.transform.Rotate(pointB, angX * 57.2957795f);		
		
		print(Vector3.Distance(pointA, pointB));
		
		pipeCounter++;
		
		//centralizou na linha
		vertices.Add(vertices[vertices.Count-4] + tempDirection - pointB);
		vertices.Add(vertices[vertices.Count-4] + tempDirection - pointB);
		vertices.Add(vertices[vertices.Count-4] + tempDirection - pointB);
		vertices.Add(vertices[vertices.Count-4] + tempDirection - pointB);
		
		

	}
*/

}

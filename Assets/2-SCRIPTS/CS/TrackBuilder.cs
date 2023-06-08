using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum TunnelType{
	STRAIGHT,
	CURVE
};


public class TrackBuilder : MonoBehaviour {

	static private int pipeCounterToNextCurve;

	static public TrackBuilder singletonInstance;

	static bool invertUV = true; // corrige uvs da track
	
	public List<GameObject> objects = new List<GameObject>();
	
	/*----------------------------------------*/
	//private GameObject road;
	public GameObject road;
	private MeshCollider meshCollider;
	
	public Material trackMaterial;
	
	private int pipeOffSet = 0;

	private int sides = 64;
	
	public List<Vector3> points = new List<Vector3>();
	
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uv = new List<Vector2>();
	public List<Vector3> normals = new List<Vector3>();
	/*-----------------------------------------*/
	
	public List<Quaternion> orientations = new List<Quaternion>();
	public List<Vector3> directions = new List<Vector3>();

	public TunnelType tunnelType;

	public float segmentLength = 1.0f;

	
	private float angX = 0;
	private float angY = 0;

	void Awake () {

		singletonInstance = this;

		init();

		//Invoke("nextAngle", Random.Range(2.0f, 8.0f));
	}

	void Update (){

		// Gerenciar emissao do mat da pista
		// DynamicGI.SetEmissive(road.GetComponent<Renderer>(), new Color(1f, 0.1f, 0.5f, 1.0f) * Random.Range(0.0f, 1.0f));

	}
	
	void init(){

		road = new GameObject ( "road", typeof(MeshFilter), typeof(MeshRenderer));
		road.transform.position = new Vector3(0,0,0);
		
		points.Add(new Vector3(0.0f, 0.0f , 0.0f));
		for (int a = 0; a < sides; a++){
			float angle = (6.28318f / sides) * a;
			vertices.Add(new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
			
			/*GameObject temp = new GameObject("Point" + a.ToString());
			temp.transform.position = vertices[a];
			temp.transform.parent = transform;
			objects.Add(temp);*/
		}

		points.Add(new Vector3(0.0f, 0.0f , 1.0f));
		for (int a = 0; a < sides; a++){
			float angle = (6.28318f / sides) * a;
			vertices.Add(new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 1.0f));
			
			GameObject temp = new GameObject("Point" + a.ToString());
			temp.transform.position = vertices[a + sides];
			temp.transform.parent = transform;
			objects.Add(temp);
		}
		
		for (int i = 0; i < sides - 1; i++){
			triangles.Add(i);
			triangles.Add(1 + i);
			triangles.Add(i + sides);
			
			triangles.Add(i + sides);
			triangles.Add(1 + i);
			triangles.Add(i + sides + 1);
		}
		
		triangles.Add(0);
		triangles.Add(sides);
		triangles.Add((sides * 2) - 1);
		
		triangles.Add((sides * 2) - 1);
		triangles.Add(sides - 1);
		triangles.Add(0);
		
		for (int i = 0; i < sides / 4; i++){
			uv.Add(new Vector2(0,0));
			uv.Add(new Vector2(1,0));
			uv.Add(new Vector2(0,0));
			uv.Add(new Vector2(1,0));
		}
		
		for (int i = 0; i < sides / 4; i++){
			uv.Add(new Vector2(0,1));
			uv.Add(new Vector2(1,1));
			uv.Add(new Vector2(0,1));
			uv.Add(new Vector2(1,1));
		}
		
		for (int i = 0; i < sides * 2; i++){
			normals.Add(new Vector3(0,1,0));
		}

		
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
		
		road.GetComponent<Renderer>().material = trackMaterial;
		
		orientations.Add(Quaternion.identity);
		directions.Add(Vector3.forward);


		//tamanho da pista


		for(int i = 0; i < TrackManager.ringAmmount; i++){
			internalAddNewRing();
		}
	}

	/* Debug 
	void makeNewRing(){
		addNewRing();
		Invoke("makeNewRing", 0.5f);
	}
	*/

	public Vector3 getPointPosition(int index){
		return objects[index].transform.position;
	}
	
	public void move(){
		
		if(tunnelType == TunnelType.CURVE){
		
			transform.RotateAround(transform.position, transform.up, angY);
			transform.RotateAround(transform.position, transform.right, angX);
			
			transform.position += transform.forward * segmentLength;

			if (--pipeCounterToNextCurve <= 0){
				nextAngle();
			}
		}
		else{
			transform.position += transform.forward;
		}

		orientations.Add(transform.rotation);
		directions.Add(transform.forward);
	}

	public void startMatch(){
		tunnelType = TunnelType.CURVE;
		pipeCounterToNextCurve = 15;
		//Debug.Log(pipeCounterToNextCurve);
	}

	public void nextAngle(){
		angX = Random.Range(-8.0f, 8.0f);
		angY = Random.Range(-5.0f, 5.0f);

		// angulo limte = 4x variacao maxima de angulos
		segmentLength = 1 - ((Mathf.Abs(angY) + Mathf.Abs(angX)) / 40);
		
		//Invoke("nextAngle", segmentLength * 5);
		//Invoke("nextAngle", Random.Range(2.0f, 8.0f));

		pipeCounterToNextCurve = (int)(segmentLength * 15);

		//Debug.Log(pipeCounterToNextCurve);

	}
	
	/*
	public void internalAddObstacle(){

		GameObject.Instantiate(obstacle, transform.position + (transform.up * 0.9f), transform.rotation);

	}
	*/

	public void internalAddNewRing(){
		
		TrackBuilder.singletonInstance.move();

		points.Add(TrackBuilder.singletonInstance.gameObject.transform.position); // adiciona ponto de caminho (vertice pai)
	
		pipeOffSet++;
		
		for (int v = 0; v < sides; v++){
			vertices.Add(objects[v].transform.position);
		}
		
		int vertexIndexOffset = sides * pipeOffSet;

		for (int i = 0; i < sides - 1; i++){
			triangles.Add(i + vertexIndexOffset);
			triangles.Add(1 + i + vertexIndexOffset);
			triangles.Add(i + sides + vertexIndexOffset);
			
			triangles.Add(i + sides + vertexIndexOffset);
			triangles.Add(1 + i + vertexIndexOffset);
			triangles.Add(i + sides + 1 + vertexIndexOffset);
		}
		
		triangles.Add(vertexIndexOffset);
		triangles.Add(sides + vertexIndexOffset);
		triangles.Add((sides * 2) - 1 + vertexIndexOffset);
		
		triangles.Add((sides * 2) - 1 + vertexIndexOffset);
		triangles.Add(sides - 1 + vertexIndexOffset);
		triangles.Add(vertexIndexOffset);
		
		if (invertUV){
			for (int i = 0; i < sides / 4; i++){
				uv.Add(new Vector2(0,0));
				uv.Add(new Vector2(1,0));
				uv.Add(new Vector2(0,0));
				uv.Add(new Vector2(1,0));
			}
			invertUV = false;
		}
		else{
			for (int i = 0; i < sides / 4; i++){
				uv.Add(new Vector2(0,1));
				uv.Add(new Vector2(1,1));
				uv.Add(new Vector2(0,1));
				uv.Add(new Vector2(1,1));
			}
			invertUV = true;
		}

		for (int i = 0; i < sides; i++){
			normals.Add(new Vector3(0,1,0));
		}

		/*
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		*/

		MeshFilter mesh_filter = road.GetComponent<MeshFilter>();

		mesh_filter.mesh.Clear();

		mesh_filter.mesh.vertices = vertices.ToArray();
		mesh_filter.mesh.triangles = triangles.ToArray();
		
		mesh_filter.mesh.uv = uv.ToArray();
		mesh_filter.mesh.normals = normals.ToArray();

		meshCollider.sharedMesh = mesh_filter.mesh;
		
		road.GetComponent<Renderer>().material = trackMaterial;
	}	

	public void internalRemoveRing(){

		points.RemoveRange(0, 1);
		vertices.RemoveRange(0, sides);
		triangles.RemoveRange(triangles.Count - (sides * 6), sides * 6);
		uv.RemoveRange(0, sides);
		normals.RemoveRange(0, sides);

		pipeOffSet -= 1;


	}

//	Muda shader de pista
//	public void roadRenderChangerStarter (){
//
//		iTween.ValueTo (gameObject, iTween.Hash (
//			"from", 0f,
//			"to", 1f,
//			"time", 1f,
//			"onupdatetarget", gameObject,
//			"onupdate", "roadRenderChanger",
//			"easetype", iTween.EaseType.easeOutQuad
//			)
//		);
//	}
//
//	public void roadRenderChanger (float newValue){
//
//		trackMaterial.SetFloat ("_Metallic", newValue);
//
//	}




}

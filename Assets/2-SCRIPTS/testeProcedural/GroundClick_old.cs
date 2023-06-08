using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class GroundClick_old : MonoBehaviour {
	
	public GameObject road;
	
	public Material mat;
	
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uv = new List<Vector2>();
	public List<Vector3> normals = new List<Vector3>();
	
	int offsetCounter = 0;
	
	void Start() {
		init();
	}	

	void init() {

		road = new GameObject ( "road", typeof(MeshFilter), typeof(MeshRenderer));
		road.transform.position = new Vector3(0,1,0);

		vertices.Add(new Vector3(-0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f ,  0.5f));
		
		vertices.Add(new Vector3(-0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f,  1.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f ,  0.5f));
		
		triangles.Add(0);
		triangles.Add(2);
		triangles.Add(6);
		
		triangles.Add(0);
		triangles.Add(6);
		triangles.Add(4);
		
		triangles.Add(0);
		triangles.Add(4);
		triangles.Add(5);
		
		triangles.Add(0);
		triangles.Add(5);
		triangles.Add(1);
		
		triangles.Add(2);
		triangles.Add(7);
		triangles.Add(6);
		
		triangles.Add(2);
		triangles.Add(3);
		triangles.Add(7);
		
		triangles.Add(1);
		triangles.Add(7);
		triangles.Add(3);
		
		triangles.Add(1);
		triangles.Add(5);
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
		
		road.GetComponent<Renderer>().material = mat;	
	}
	
	void addNewRing() {
	
		offsetCounter++;
		
		vertices.Add(new Vector3(-0.5f,  1.0f + offsetCounter, -0.5f));
		vertices.Add(new Vector3(-0.5f,  1.0f + offsetCounter,  0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f + offsetCounter, -0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f + offsetCounter,  0.5f));
		
		int vertexIndexOffset = 4 * offsetCounter;
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		triangles.Add(4 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(4 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		triangles.Add(1 + vertexIndexOffset);
		
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(3 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(3 + vertexIndexOffset);
		
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);

		if (offsetCounter % 2 == 1){
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
		
		road.GetComponent<Renderer>().material = mat;
	}
	
	void OnMouseDown() {
		addNewRing();
	}		
}


/*
 * 
 * using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class GroundClick : MonoBehaviour {
	
	public GameObject road;
	
	public Material mat;
	
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
	
	void OnMouseDown() {
		addNewRing();
	}
	
	void init(){
		
		road = new GameObject ( "road", typeof(MeshFilter), typeof(MeshRenderer));
		road.transform.position = new Vector3(0,1,0);
		
		vertices.Add(new Vector3(-0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f, 0.0f ,  0.5f));
		
		vertices.Add(new Vector3(-0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3(-0.5f,  1.0f ,  0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f , -0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f ,  0.5f));
		
		triangles.Add(0);
		triangles.Add(2);
		triangles.Add(6);
		
		triangles.Add(0);
		triangles.Add(6);
		triangles.Add(4);
		
		triangles.Add(0);
		triangles.Add(4);
		triangles.Add(5);
		
		triangles.Add(0);
		triangles.Add(5);
		triangles.Add(1);
		
		triangles.Add(2);
		triangles.Add(7);
		triangles.Add(6);
		
		triangles.Add(2);
		triangles.Add(3);
		triangles.Add(7);
		
		triangles.Add(1);
		triangles.Add(7);
		triangles.Add(3);
		
		triangles.Add(1);
		triangles.Add(5);
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
		
		exitPoint = transform.position + Vector3.up;
		exitDirection = Vector3.up;

		
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		
		MeshFilter mesh_filter = road.GetComponent<MeshFilter>();
		mesh_filter.mesh = mesh;	
		
		road.renderer.material = mat;
	}
	
	void addNewRing(){
		float angX = Random.Range(-0.17f, 0.17f);
		float angZ = Random.Range(-0.17f, 0.17f); 
		/*
		Vector3 tempDirection = exitDirection;
		
		tempDirection.x = (Mathf.Cos(angZ) * exitDirection.x) - (Mathf.Sin(angZ) * exitDirection.y);
		tempDirection.y = (Mathf.Sin(angZ) * exitDirection.x) + (Mathf.Cos(angZ) * exitDirection.y);
		aux = tempDirection.y;
		tempDirection.y = (Mathf.Sin(angX) * exitDirection.z) + (Mathf.Cos(angX) * exitDirection.y) * aux;
		tempDirection.z = (Mathf.Cos(angX) * exitDirection.z) - (Mathf.Sin(angX) * exitDirection.y) * aux;
		
		exitDirection = tempDirection;
		*/
		
//		print(exitDirection);
//		print(exitPoint);
		
//		exitPoint += exitDirection;
		
//		print(exitDirection.x);
//		print(exitDirection.y);
//		print(exitDirection.z);

/*
		pipeCounter++;
		
		vertices.Add(vertices[vertices.Count - 4] - exitPoint);
		vertices.Add(vertices[vertices.Count - 4] - exitPoint);
		vertices.Add(vertices[vertices.Count - 4] - exitPoint);
		vertices.Add(vertices[vertices.Count - 4] - exitPoint);
		
		//print(vertices.Count - sides);
		Vector3 temp;
		for (int vertexCounter = vertices.Count - sides; vertexCounter < vertices.Count; vertexCounter++){
			temp = vertices[vertexCounter];
			
			temp.x = (Mathf.Cos(angZ) * vertices[vertexCounter].x) - (Mathf.Sin(angZ) * vertices[vertexCounter].y);
			temp.y = (Mathf.Sin(angZ) * vertices[vertexCounter].x) + (Mathf.Cos(angZ) * vertices[vertexCounter].y);
			aux = temp.y;
			temp.y = (Mathf.Sin(angX) * vertices[vertexCounter].z) + (Mathf.Cos(angX) * vertices[vertexCounter].y) * aux;
			temp.z = (Mathf.Cos(angX) * vertices[vertexCounter].z) - (Mathf.Sin(angX) * vertices[vertexCounter].y) * aux;
			
			//temp += exitPoint + exitDirection;
			
		
			
			vertices[vertexCounter] = temp;
			//vertices[vertexCounter] += exitDirection;
			//print(temp);
		}
		
		vertices[vertices.Count - 1] += exitPoint;
		vertices[vertices.Count - 2] += exitPoint;
		vertices[vertices.Count - 3] += exitPoint;
		vertices[vertices.Count - 4] += exitPoint;
		
		
		vertices[vertices.Count - 1] += exitDirection;
		vertices[vertices.Count - 2] += exitDirection;
		vertices[vertices.Count - 3] += exitDirection;
		vertices[vertices.Count - 4] += exitDirection;
		
		exitPoint = exitPoint+exitDirection;

		Vector3 tempDirection = exitDirection;
		
		tempDirection.x = (Mathf.Cos(angZ) * exitDirection.x) - (Mathf.Sin(angZ) * exitDirection.y);
		tempDirection.y = (Mathf.Sin(angZ) * exitDirection.x) + (Mathf.Cos(angZ) * exitDirection.y);
		aux = tempDirection.y;
		tempDirection.y = (Mathf.Sin(angX) * exitDirection.z) + (Mathf.Cos(angX) * exitDirection.y) * aux;
		tempDirection.z = (Mathf.Cos(angX) * exitDirection.z) - (Mathf.Sin(angX) * exitDirection.y) * aux;
		
		exitDirection = tempDirection;
		
		
		/*
		vertices.Add(new Vector3(-0.5f,  1.0f, -0.5f));
		vertices.Add(new Vector3(-0.5f,  1.0f,  0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f, -0.5f));
		vertices.Add(new Vector3( 0.5f,  1.0f,  0.5f));
		*/
/*		
		int vertexIndexOffset = 4 * pipeCounter;
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		triangles.Add(4 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(4 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		
		triangles.Add(0 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
		triangles.Add(1 + vertexIndexOffset);
		
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(6 + vertexIndexOffset);
		
		triangles.Add(2 + vertexIndexOffset);
		triangles.Add(3 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(7 + vertexIndexOffset);
		triangles.Add(3 + vertexIndexOffset);
		
		triangles.Add(1 + vertexIndexOffset);
		triangles.Add(5 + vertexIndexOffset);
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
		
		

		
		exitPoint += exitDirection;
		
		Mesh mesh = new Mesh();
		
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		
		MeshFilter mesh_filter = road.GetComponent<MeshFilter>();
		mesh_filter.mesh = mesh;	
		
		road.renderer.material = mat;
	}
}

*/
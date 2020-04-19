using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    
    public float fov = 45;
    public float viewDistance = 1.2f;
    private Mesh mesh;
    private Vector3 origin = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private float startingAngle = 0f;
    private GameObject target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        //Vector3 origin = Vector3.zero;
        int rayCount = 90;
        float angle = startingAngle;// 0f;
        float angleIncrease = fov / rayCount;
        Collider2D targetCollider = null;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), viewDistance, layerMask);
            RaycastHit2D raycastTarget = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), viewDistance);
            
            if(raycastTarget.collider != null)
            {
                if(raycastTarget.collider.gameObject.tag == "Player")
                {
                    targetCollider = raycastTarget.collider;
                    Debug.Log("Found player");
                }
            } else
            {
                Debug.Log("doesn find sh1t");
            }

            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = transform.InverseTransformPoint(raycastHit2D.point);
                
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        target = targetCollider != null ? targetCollider.gameObject : null;
    }

    public bool Spot(GameObject go)
    {
        if (Vector3.Distance(origin, go.transform.position) < viewDistance)
        {
            Vector3 dir = (go.transform.position - origin).normalized;
            //if (Vector3.Angle(direction, dir) < fov / 2f)
            if (Mathf.DeltaAngle(startingAngle - fov / 2f, GetAngleFromVectorFloat(dir)) < fov / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, viewDistance);
                if (hit.collider != null)
                {
                    return hit.collider.gameObject == go;
                }
            }
        }
        return false;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = transform.InverseTransformPoint(origin);
        //origin = transform.position;
        //rb.MovePosition(origin);
        //this.origin = rb.position;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
        direction = aimDirection;
    }

    public GameObject GetPlayerTarget()
    {
        return target;
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }


}

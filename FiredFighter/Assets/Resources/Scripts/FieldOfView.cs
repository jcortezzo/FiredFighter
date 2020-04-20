using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask targetLayerMask;
    
    public float fov = 45;
    public float viewDistance = 1.2f;
    private Mesh mesh;
    private Vector3 origin = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private float startingAngle = 0f;
    private GameObject target;
    MeshRenderer mr;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mr = GetComponent<MeshRenderer>();
        mr.material.SetColor("_TintColor", Color.yellow);
        GetComponent<MeshFilter>().mesh = mesh;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        mesh = new Mesh();
        mr = GetComponent<MeshRenderer>();
        mr.material.SetColor("_TintColor", Color.yellow);
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
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), viewDistance, layerMask);//, QueryTriggerInteraction.Ignore);
            RaycastHit2D raycastTarget = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), viewDistance);
            
            if(raycastTarget.collider != null)
            {
                //if(raycastTarget.collider.gameObject.tag == "Player")
                //{
                //    targetCollider = raycastTarget.collider;
                //    //Debug.Log("Found player");
                //} else
                //{
                //    //Debug.Log("Found " + raycastTarget.collider.gameObject.name);
                //}
            } else
            {
                //Debug.Log("doesn find sh1t");
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
        //Vector3 adjustedOrigin = transform.TransformPoint(origin);
        Vector3 adjPos = transform.InverseTransformPoint(go.transform.position);
        if (Vector3.Distance(origin, adjPos) < viewDistance + 2)
        {
            //Debug.Log("Within distance");
            Vector3 dir = (adjPos - origin).normalized;
            if (Mathf.Abs(Mathf.DeltaAngle(GetAngleFromVectorFloat(direction), GetAngleFromVectorFloat(dir))) < fov / 2)
            {
                //Debug.Log("Within angle");
                RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(origin), dir, viewDistance, targetLayerMask);
                Debug.DrawRay(transform.TransformPoint(origin), dir);
                if (hit.collider != null)
                {
                    //Debug.DrawRay(origin, dir);
                    //return true;
                    //Debug.Log(hit.collider.gameObject.name);
                    return hit.collider.gameObject == go;
                } else
                {
                    Debug.Log("what the fuck");
                }
            }
        }
        return false;
    }

    public void SetColor(Color color)
    {
        mr.sharedMaterial.SetColor("_Color",color);
    }

    public void SetColorAlpha(float a)
    {
        Color c = mr.sharedMaterial.color;
        mr.sharedMaterial.SetColor("_Color", new Color(c.r, c.g, c.b, a));
    }

    public void SetOrigin(Vector3 origin)
    {
        //Vector3 vec = transform.InverseTransformPoint(origin);
        //this.origin = vec;// + (vec - transform.localPosition);
        //origin = transform.position;
        rb.MovePosition(transform.InverseTransformPoint(origin));
        this.origin = rb.position;
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

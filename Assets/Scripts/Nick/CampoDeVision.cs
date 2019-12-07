using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoDeVision : MonoBehaviour
{
    public float radioVision;
    [Range(0, 360)]
    public float anguloVision;

    public LayerMask mascaraObjetivos;
    public LayerMask mascaraObstaculos;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public float resolucionMesh;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    float maskCutDist = .1f;

    public List<Transform> objetivosVisibles = new List<Transform>();

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("EncontrarObjetivos", 0.05f);
    }

    void LateUpdate()
    {
        DibujarCampo();
    }

    IEnumerator EncontrarObjetivos(float retraso)
    {
        while (true)
        {
            yield return new WaitForSeconds(retraso);
            encontrarObjetivos();
        }
    }

    void encontrarObjetivos()
    {
        objetivosVisibles.Clear();
        Collider2D[] objetivosEnRango = Physics2D.OverlapCircleAll((Vector2)transform.position, radioVision, mascaraObjetivos);
        for (int i = 0; i < objetivosEnRango.Length; i++)
        {
            Transform objetivo = objetivosEnRango[i].transform;
            Vector3 dirObjetivo = (objetivo.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirObjetivo) < anguloVision / 2)
            {
                float distObjetivo = Vector3.Distance(transform.position, objetivo.position);
                if (!Physics2D.Raycast(transform.position, dirObjetivo, distObjetivo, mascaraObstaculos))
                {
                    objetivosVisibles.Add(objetivo);
                }
            }
        }
    }

    void DibujarCampo()
    {
        int stepCount = Mathf.RoundToInt(anguloVision * resolucionMesh);
        float stepAngleSize = anguloVision / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angulo = -transform.eulerAngles.z - anguloVision / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angulo);
            viewPoints.Add(newViewCast.hitPoint);

            if (i > 0)
            {
                bool ThresholdExceeded = Mathf.Abs(oldViewCast.dist- newViewCast.dist)> edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (newViewCast.hit && oldViewCast.hit &&ThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            oldViewCast = newViewCast;
        }

        int CantVertices = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[CantVertices];
        int[] triangulos = new int[(CantVertices - 2)*3];

        vertices[0] = Vector3.zero;
        for (int i=0; i<CantVertices-1; i++)
        {
			
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i] + (viewPoints[i]-transform.position).normalized*maskCutDist);
            if (i < CantVertices - 2)
            {
                triangulos[i * 3] = 0;
                triangulos[i * 3 + 1] = i + 1;
                triangulos[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangulos;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minVC, ViewCastInfo maxVC)
    {
        float minAngle = minVC.angulo;
        float maxAngle = maxVC.angulo;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angulo = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angulo);
            bool ThresholdExceeded = Mathf.Abs(minVC.dist - newViewCast.dist) > edgeDstThreshold;
            if (newViewCast.hit == minVC.hit && !ThresholdExceeded)
            {
                minAngle = angulo;
                minPoint = newViewCast.hitPoint;
            }
            else
            {
                maxAngle = angulo;
                maxPoint = newViewCast.hitPoint;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float anguloGlobal)
    {
        Vector3 dir = DirAngulo(anguloGlobal, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radioVision, mascaraObstaculos);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, anguloGlobal);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * radioVision, radioVision, anguloGlobal);
        }
    }

    public Vector3 DirAngulo(float anguloGrados, bool anguloGlobal)
    {
        if (!anguloGlobal)
        {
            anguloGrados -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(anguloGrados * Mathf.Deg2Rad), Mathf.Cos(anguloGrados * Mathf.Deg2Rad), 0);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 hitPoint;
        public float dist;
        public float angulo;

        public ViewCastInfo(bool _hit, Vector3 _hitPoint, float _dist, float _angulo)
        {
            hit = _hit;
            hitPoint = _hitPoint;
            dist = _dist;
            angulo = _angulo;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}

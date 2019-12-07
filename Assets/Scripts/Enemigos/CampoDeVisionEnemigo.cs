using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoDeVisionEnemigo : MonoBehaviour
{
	public float radioVision;
	[Range(0, 360)]
	public float anguloVision;

	public LayerMask mascaraObjetivos;
	public LayerMask mascaraObstaculos;

	public List<Transform> objetivosVisibles = new List<Transform>();

	void Start()
	{
		StartCoroutine("EncontrarObjetivos", 0.2f);
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

	public Vector3 DirAngulo(float anguloGrados, bool anguloGlobal)
	{
		if (!anguloGlobal)
		{
			anguloGrados -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(anguloGrados * Mathf.Deg2Rad), Mathf.Cos(anguloGrados * Mathf.Deg2Rad), 0);
	}
}

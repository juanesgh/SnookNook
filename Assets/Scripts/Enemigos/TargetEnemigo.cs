using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TargetEnemigo : MonoBehaviour
{
	CampoDeVisionEnemigo cdv;
	AIPath ai;

	public List<Transform> puntos = new List<Transform>();
	int contPuntos = 1;
	public Vector3 objetivo;
	bool patrullando = true;
	float tiempoEspera = 3f;
	float tiempoRecalcular = 0f;

    void Start()
    {
		cdv = GetComponent<CampoDeVisionEnemigo>();
		ai = GetComponent<AIPath>();
		objetivo = puntos[contPuntos].position;
		objetivo.z = 0;
		ai.destination = objetivo;
    }

    void Update()
    {
		if (cdv.objetivosVisibles.Count>0)
		{
			Persecucion(cdv.objetivosVisibles[0].position);
		}
		else
		{
			if (patrullando)
			{
				if (Vector3.Distance(transform.position, objetivo)<.5f)
				{
					patrullando = false;
				}
			}
			else
			{
				if (tiempoEspera>=0)
				{
					transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime*60);
					tiempoEspera -= Time.deltaTime;
				}
				else
				{
					patrullando = true;
					CambiarObjetivo();
					tiempoEspera = 3f;
				}
			}
		}
	}


	void CambiarObjetivo()
	{
		if (contPuntos<puntos.Count-1)
		{
			contPuntos += 1;
		}
		else
		{
			contPuntos = 0;
		}
		objetivo = puntos[contPuntos].position;
		objetivo.z = 0;
		ai.destination = objetivo;
	}

	public void Persecucion(Vector3 posicion)
	{
		patrullando = false;
		if (tiempoRecalcular<=0)
		{
			objetivo = posicion;
			objetivo.z = 0;
			ai.destination = objetivo;
			tiempoRecalcular += .5f;
		}
		else
		{
			tiempoRecalcular -= Time.deltaTime;
		}
		if (Vector3.Distance(transform.position, objetivo)<.5f)
		{
			if (tiempoEspera>=0)
			{
				transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime*60);
				tiempoEspera -= Time.deltaTime;
			}
			else
			{
				patrullando = true;
				objetivo = puntos[contPuntos].position;
				objetivo.z = 0;
				ai.destination = objetivo;
				tiempoEspera = 3f;
			}
		}
	}
}

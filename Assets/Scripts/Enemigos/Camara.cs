using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
	CampoDeVisionEnemigo cdv;
	GameObject nick;

	[Range(0,360)]
	public int rangoRotacion = 0;
	public int velocidadRotacion = 40;
	public float rotacion = 0f;
	int sentido = 1;
	float tiempoEspera = 4f;
	bool esperando = false;

    void Start()
    {
		cdv = GetComponent<CampoDeVisionEnemigo>();
    }

    void Update()
    {
		if (cdv.objetivosVisibles.Count>0)
		{
			nick = GameObject.Find("Nick");
			MovNick script = nick.GetComponent<MovNick>();
			script.Ruido(7);
		}
    }

	void FixedUpdate()
	{
		if (esperando)
		{
			if (tiempoEspera>=0)
			{
				tiempoEspera -= Time.deltaTime;
			}
			else
			{
				esperando = false;
				tiempoEspera = 3f;
			}
		}
		else
		{
			if (sentido == 1)
			{
				if (rotacion < rangoRotacion)
				{
					float tiempo = Time.deltaTime*velocidadRotacion;
					transform.RotateAround(transform.position, sentido*Vector3.forward, tiempo);
					rotacion += tiempo;
				}
				else
				{
					esperando = true;
					sentido = -1;
				}	
			}
			else if (sentido == -1)
			{
				if (rotacion > 0)
				{
					float tiempo = Time.deltaTime*velocidadRotacion;
					transform.RotateAround(transform.position, sentido*Vector3.forward, tiempo);
					rotacion -= tiempo;
				}
				else
				{
					esperando = true;
					sentido = 1;
				}
			}


		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
	bool abierto = false;
	float velocidadRotacion = 100f;
	Vector3 posicionPivote;
	float rotacionInicial;
	float rotacion;

	void Start()
	{
		rotacionInicial = transform.eulerAngles.z;
		posicionPivote = transform.position + transform.right*transform.localScale.x/2 + transform.up*transform.localScale.y/2;
	}

	public void cambiarEstado()
	{
		abierto = !abierto;
	}

	void FixedUpdate()
	{
		if (abierto)
		{
			if (rotacion < 90)
			{
				float tiempo = Time.deltaTime*velocidadRotacion;
				transform.RotateAround(posicionPivote, Vector3.forward, tiempo);
				rotacion += tiempo;
				if (rotacion >= 90)
				{
					AstarPath.active.Scan();
				}
			}
		}
		else
		{
			if (rotacion > 0)
			{
				float tiempo = Time.deltaTime*velocidadRotacion;
				transform.RotateAround(posicionPivote, -Vector3.forward, tiempo);
				rotacion -= tiempo;
				if (rotacion <= 0)
				{
					AstarPath.active.Scan();
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionNick : MonoBehaviour
{
	bool interactuo = false;

	public GameObject mensajeInteraccion;
	public Puerta scriptPuerta;
	
	void OnTriggerEnter2D(Collider2D otro)
	{
		if (otro.CompareTag("Puerta"))
		{
			scriptPuerta = otro.GetComponent<Puerta>();
		}
		mensajeInteraccion.SetActive(true);
		interactuo = false;
		if (otro.CompareTag("Enemigo")){
			SceneManager.LoadScene("GameOver");
		}
	}

	void OnTriggerStay2D(Collider2D otro)
	{
		if (otro.CompareTag("Puerta"))
		{
			if (Input.GetKey(KeyCode.E))
			{
				if (!interactuo)
				{
					scriptPuerta.cambiarEstado();
					interactuo = true;
				}

			}
		}
		if (otro.CompareTag("Enemigo")){
			SceneManager.LoadScene("GameOver");
		}
	}

	void OnTriggerExit2D()
	{
		mensajeInteraccion.SetActive(false);
		interactuo = false;
	}
}

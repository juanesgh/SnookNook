using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovNick : MonoBehaviour
{
	public LayerMask mascaraObjetivos;

    Vector3 direccion;
    public float velocidadInicial = 5f;
    [Range (0.1f,0.9f)]
    public float multSigilo = 0.5f;
    [Range (1.1f,2f)]
    public float multVeloz = 1.5f;
    float velocidad;

	public GameObject panelmapa;
    
    void Update()
    {
        direccion.x = Input.GetAxis("Horizontal");
        direccion.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = velocidadInicial * multVeloz;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            velocidad = velocidadInicial * multSigilo;
        }
        else
        {
            velocidad = velocidadInicial;
        }
        
		if (Input.GetKey(KeyCode.M))
		{
			panelmapa.SetActive(true);
		}
		else
		{
			panelmapa.SetActive(false);
		}
	}

    void FixedUpdate()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
		if (direccion!= Vector3.zero)
		{
			if (velocidad == velocidadInicial)
			{
				if (Random.value<0.02f)
				{
					Ruido(1);
				}
			}
			else if (velocidad == velocidadInicial * multSigilo)
			{
				if (Random.value<0.06f)
				{
					Ruido(2);
				}
			}
		}
    }

	public void Ruido(int rango)
	{
		Collider2D[] objetivosEnRango = Physics2D.OverlapCircleAll((Vector2)transform.position, rango, mascaraObjetivos);
		for (int i = 0; i < objetivosEnRango.Length; i++)
		{
			if (objetivosEnRango[i].GetComponent<TargetEnemigo>() != null)
			{
				TargetEnemigo target = objetivosEnRango[i].GetComponent<TargetEnemigo>();
				target.Persecucion(transform.position);
			}
		}
	}
}

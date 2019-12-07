using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float Tiempos;
    public Text Cronometro;
    public Text Celulars;
    public List<string> Mensajes = new List<string>();
    private int contador ;

    // Start is called before the first frame update
    void Start()
    {
        Mensajes.Add("Acercate a las murallas,\n pueden haber puertas pero\n cuidado con los enemigos");
        Mensajes.Add("Hay items en las habitaciones\n pueden ser útiles");
        Mensajes.Add("Si te encuentran \n van a estar más alerta");
        Mensajes.Add("2Hay objetos en las habitaciones\n pueden ser útiles");
        Mensajes.Add("3Hay objetos en las habitaciones\n pueden ser útiles");
        Mensajes.Add("4Hay objetos en las habitaciones\n pueden ser útiles");
        Mensajes.Add("");
        Cronometro.text = Tiempos.ToString();
        contador = Mensajes.Count;
        StartCoroutine(Test());
    }   
    IEnumerator Test()
    {
        for (int i = 0; i < contador; i++)
        {
            yield return new WaitForSeconds(10);
            Celulars.text = Mensajes[i];    
            yield return new WaitForSeconds(3);
            Celulars.text = "";
        }

    }

    // Update is called once per frame
    void Update()
    {
        Tiempos -= Time.deltaTime;
        Cronometro.text = Mathf.Round(Tiempos).ToString();
        if (Tiempos < 0)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }


    }
}

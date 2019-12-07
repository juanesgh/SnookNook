using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerItem : MonoBehaviour
{
   private Inventario inventario;
   public GameObject itemBoton;

   private void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     for( int i =0;i< inventario.slots.Length; i++)
        //     {
        //         if(inventario.isFull[i]== false)
        //         {
        //             // add
        //             inventario.isFull[i] = true;
        //             Instantiate(itemBoton,inventario.slots[i].transform, false);
        //             Destroy(gameObject);
        //             break;
        //         }
        //     }
        // }
    }
}

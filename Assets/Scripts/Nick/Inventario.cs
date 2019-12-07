using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public bool[] isFull;
    public static GameObject[] slots;

    int scroll;
    int amount;
    int currentItem;
    float hold;
    bool pressed;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        slots =  new GameObject[10];
        GameObject stapler = new GameObject("stapler");
        slots[0] = (stapler);
        GameObject card = new GameObject("card");
        slots[1] = (card);
    }

    // Update is called once per frame
    void Update()
    {
        scroll = (int)Input.mouseScrollDelta.y;
    }
    void FixedUpdate(){
        amount = count(slots);
        GameObject item = GameObject.Find("ItemImage");
        GameObject textItem = GameObject.Find("ItemName");
        if (amount > 0 && !Input.GetMouseButtonDown(0)){
            scroll = scroll % amount;
            if (amount > 0){
                int constant = currentItem + scroll;
                if (constant < 0){
                    currentItem = amount + scroll;
                }
                else if (constant >= amount){
                    currentItem = constant - amount;
                }
                else{
                    currentItem = currentItem + scroll;
                }
            }
            //set item to curretn item
            string name = slots[currentItem].name;
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            textItem.GetComponent<UnityEngine.UI.Text>().text = slots[currentItem].name;

        }
        else{
            if (!Input.GetMouseButtonDown(0)){
                //item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>();
            }
        }
        //ejecutar la accion del item si se preisona e
        //Inv: Stapler, arrojar. Tarjeta, abrir. Gas, Auyentar. CloroformoBomb, desactivar. 
        //ChaquetaUV, Desactivar camaras.
        if (Input.GetMouseButtonDown(0) || pressed) {
            if(amount > 0){
                //revisa que exista un item
                string name = slots[currentItem].name;
                if (name == "stapler"){
                    pressed = true;
                    if(Input.GetMouseButtonDown(0) && hold < 5){
                        //sigue cargando
                        hold += Time.deltaTime;
                    }
                    else{
                        //lanza la corchetera
                        pressed = false;
                        hold = 0;
                        Vector3 mousePos = Input.mousePosition;
                        //speed = hold + 1
                        //lamar a funcion que arroje el objeto throw(hold, mousePos)
                        //crear instancia de gameobject corchetera cuando deje de moverse
                        //con rigidbody2d

                    }
                }
                else if (name == "card"){
                    //setear puertas que solo se abren si este item es usado
                }
                else if (name == "gas"){
                    //conseguir ubicacion de nick y armar el radio desde ahi
                    //crear el game object y dejarlo con el sprite puesto todo ok
                    Vector2 posBomb = GameObject.Find("Nick").transform.position;
                    List<GameObject> go = GameObjectsInRadius(distance, posBomb);
                    foreach(GameObject g in go){
                        //setear el flee de estos guardias
                    }
                }
                else if (name == "cloroformobomb"){
                    GameObject[] go = GameObject.FindGameObjectsWithTag("Enemigo");
                    //setear velocidad a cero en el movimiento por 5 minutos

                }
                else if (name == "uvjacket"){
                    //rango de visoin de camaras baja a cero
                }
            }
        }
    }

    int count(GameObject[] lst){
        int result = 0;
        foreach (GameObject g in lst){
            if (g != null){
                result += 1;
            }
        }
        return result;
    }
    public GameObject[] GetItems(){
        return slots;
    }

    private void Throw(int hold, Vector3 mousePos){
        //hacer que la el objeto se mueva independiente del resto de weaitas
    }
    public List<GameObject> GameObjectsInRadius(float radio, Vector2 center){
        GameObject[] lst = GameObject.FindGameObjectsWithTag("Enemigo");
        List<GameObject> affected = new List<GameObject>();
        foreach (GameObject g in lst){
            Vector2 a = g.transform.position;
            if (radio <= Vector2.Distance(a, center)){
                affected.Add(g);
            }
        }
        return affected;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    // Start is called before the first frame update
    //esto genera un boon paara pasaar a la siguiente escena y guarda los objetos que no se quieren perder
    private void OnGUI(){
        int xCenter = (Screen.width / 2);
        int yCenter = (Screen.height / 2);
        int width = 400;
        int height = 120;

        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("button"));
        fontSize.fontSize = 32;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "SampleScene")
        {

            // Show a button to allow scene2 to be switched to.
            if (GUI.Button(new Rect(xCenter - width / 2, yCenter - height / 2, width, height), "Load second scene", fontSize))
            {
                //Se realiza aqui en not destroy, porque los items se crean y usan variablemente, asi no
                //usamos toda la memoria guardando objetos inuties
                DontDestroyOnLoad(GameObject.Find("Nick"));
                GameObject[] it = Inventario.slots;
                DontDestroyOnLoad(GameObject.Find("CM vcam1"));
                DontDestroyOnLoad(GameObject.Find("Obstaculos"));
                DontDestroyOnLoad(GameObject.Find("Plano"));
                DontDestroyOnLoad(GameObject.Find("Suelo"));
                DontDestroyOnLoad(GameObject.Find("Canvas"));
                DontDestroyOnLoad(GameObject.Find("GameStats"));
                DontDestroyOnLoad(GameObject.Find("Directional Light"));

                foreach (GameObject g in it){
                    if (g){
                        DontDestroyOnLoad(g);
                    }
                }
                SceneManager.LoadScene("Level2");
            }
        }
        else
        {
            // Show a button to allow scene1 to be returned to.
            if (GUI.Button(new Rect(xCenter - width / 2, yCenter - height / 2, width, height), "Return to first scene", fontSize))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}

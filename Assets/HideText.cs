using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideText : MonoBehaviour
{
    // Start is called before the first frame update
    bool active;
    GameObject tex;
    void Start()
    {
        active = true;
        tex = GameObject.Find("Help");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("h")){
            active = !active;
            tex.SetActive(active);
        }
    }
}

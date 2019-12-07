using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (CampoDeVision))]
public class EditorCampoDeVision : Editor
{
    void OnSceneGUI()
    {
        CampoDeVision cdv = (CampoDeVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(cdv.transform.position,Vector3.forward,Vector3.up,360,cdv.radioVision);
        Vector3 anguloVisionA = cdv.DirAngulo(-cdv.anguloVision / 2, false);
        Vector3 anguloVisionB = cdv.DirAngulo(cdv.anguloVision / 2, false);
        Handles.DrawLine(cdv.transform.position, cdv.transform.position + anguloVisionA * cdv.radioVision);
        Handles.DrawLine(cdv.transform.position, cdv.transform.position + anguloVisionB * cdv.radioVision);

        Handles.color = Color.red;
        foreach (Transform objetivo in cdv.objetivosVisibles)
        {
            Handles.DrawLine(cdv.transform.position, objetivo.position);
        }
    }
}

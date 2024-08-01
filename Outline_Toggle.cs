using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutlineComponent;
public class Outline_Tougue : MonoBehaviour
{

    public Outline outlineComponent;

    // Update is called once per frame
    void Update()
    {
        // O tuþuna basýldýðýnda outline efektini aç/kapat
        if (Input.GetKeyDown(KeyCode.O))
        {
            outlineComponent.enabled = !outlineComponent.enabled;
        }
    }
}

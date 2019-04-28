using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPrueba : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer m_Renderer;
    void Start() {
        m_Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Polar")
                {
                   m_Renderer.material.color = Color.red;
                }
                else {
                  m_Renderer.material.color = Color.blue;   
                }
            }
        } 
       
    }
}

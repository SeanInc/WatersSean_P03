using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitUI : MonoBehaviour
{
    PhysicsRaycaster m_Raycaster;
    GraphicRaycaster g_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public bool hitUI;

    // Start is called before the first frame update
    void Start()
    {
        //m_Raycaster = Camera.main.GetComponent<PhysicsRaycaster>();
        g_Raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        g_Raycaster.Raycast(m_PointerEventData, results);

        hitUI = false;

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {

            // Debug.Log("Hit " + result.gameObject.name);

            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                hitUI = true;
            }
            else
            {
                hitUI = false;
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float amount = 1.0f;
    public bool isPressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Paddle"))
            {
                obj.GetComponent<Paddle>().Move(amount);
            }
        }
    }
}

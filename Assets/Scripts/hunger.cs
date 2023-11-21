using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class hunger : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    
    private float fullness; 
    private float timer;
  
    // Start is called before the first frame update
    void Start()
    {
        fullness = 50f;
        SetStatusText();
    }

    void HungerUpdate()
    {
        if (fullness > 0)
        {
            fullness -= (0.1f * timer);
        }
        
        if (fullness <= 0)
        {
            fullness = 0;
        }
    }

   public void OnButtonClicked()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fullness += 10f;
            print("Yum!");  
        }
    }
    
    void SetStatusText()
    {
        statusText.text = "Fullness: " + fullness.ToString();
    }
    
    // Update is called once per frame
    void Update()
    { 
        timer = Time.deltaTime;
        HungerUpdate();
        OnButtonClicked();
        SetStatusText();
    }

    private void FixedUpdate()
    {
    }
}

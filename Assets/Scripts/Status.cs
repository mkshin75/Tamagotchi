using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Status : MonoBehaviour
{
    private NavScript navScript;
    public TextMeshProUGUI statusText;
    
    public float fullness;
    public float hygiene;
    public float happiness;
    public float energy;
    public float life;
    
    private float timer;
    
    public Button feed;
    public Button clean;
    public Button play;
    public Button sleep;
  
    // Start is called before the first frame update
    void Start()
    {
        fullness = 22f;
        hygiene = 22f;
        happiness = 22f;
        energy = 22f;
        life = fullness + hygiene + happiness + energy;
        SetStatusText();

        Button btn1 = feed.GetComponent<Button>();
        btn1.onClick.AddListener(FeedThePet);

        Button btn2 = clean.GetComponent<Button>();
        btn2.onClick.AddListener(CleanThePet);
        
        Button btn3 = play.GetComponent<Button>();
        btn3.onClick.AddListener(PlayThePet);

        Button btn4 = sleep.GetComponent<Button>();
        btn4.onClick.AddListener(SleepThePet);
    }

    void StatusUpdate()
    {
        if (fullness > 0)
        {
            fullness -= (0.1f * timer);
        }

        if (fullness <= 0)
        {
            fullness = 0;
        }

        if (hygiene > 0)
        {
            hygiene -= (0.1f * timer);
        }
        
        if (hygiene <= 0)
        {
            hygiene = 0;
        }
    
        if (happiness > 0)
        {
            happiness -= (0.1f * timer);
        }
        
        if (happiness <= 0)
        {
            happiness = 0;
        }
    
        if (energy > 0)
        {
            energy -= (0.1f * timer);
        }
        
        if (energy <= 0)
        {
            energy = 0;
        }
    }

    Boolean isHungry()
    {
        if (fullness < 20)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    Boolean isDirty()
    {
        if (hygiene < 20)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    Boolean isUnhappy()
    {
        if (happiness < 20)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    Boolean isSleepy()
    {
        if (energy < 20)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

   public void FeedThePet()
    {
        fullness += 10f;
        print("Yum!");  
    }

   public void CleanThePet()
   {
       hygiene += 10f;
       print("Got clean!");
   }
   
   public void PlayThePet()
   {
       happiness += 10f;
       print("Fun!");
   }

   public void SleepThePet()
   {
       energy += 10f;
       print("zzz");
   }
   
   private void OnCollisionEnter(Collision col)
   {
       if (col.gameObject.CompareTag("Draggable"))
       {
           Destroy(col.gameObject);
           fullness += 10f;
           gameObject.GetComponent<NavScript>().enabled = false;
       }
   }
   
    void SetStatusText()
    {
        statusText.text = "Fullness: " + fullness.ToString() + "\n" + 
                          "Hygiene: " + hygiene.ToString() + "\n" +
                          "Happiness: " + happiness.ToString() + "\n" +
                          "Energy: " + energy.ToString();
    }

    void SendAlert()
    {
        if (isHungry())
        {
            print("Pet seems hungry");
        }

        if (isDirty())
        {
            print("Pet seems dirty");
        }

        if (isUnhappy())
        {
            print("Pet seems unhappy");    
        }
        
        if (isSleepy())
        {
            print("Pet seems sleepy");
        }
    }
    
    // Update is called once per frame
    void Update()
    { 
        timer = Time.deltaTime;
        StatusUpdate();
    }

    private void FixedUpdate()
    {
        SetStatusText();
        SendAlert();
    }
}

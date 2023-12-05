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
    public TextMeshProUGUI alertText;
    public TextMeshProUGUI reactionText;
    
    public float fullness;
    public float hygiene;
    public float happiness;
    public float energy;
    public float life;

    public GameObject food;
    
    private float timer;
    
    public Button feed;
    public Button clean;
    public Button play;
    public Button sleep;
    
    string hungrystate;
    string cleanstate;
    string happystate;
    string energystate;
  
    // Start is called before the first frame update
    void Start()
    {
        fullness = 22f;
        hygiene = 22f;
        happiness = 22f;
        energy = 22f;
        life = fullness + hygiene + happiness + energy;
        SetStatusText();
        SetAlertText();
        HideReactionText();
        SendAlert();

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
            fullness -= (0.5f * timer);
        }

        if (fullness <= 0)
        {
            fullness = 0;
        }

        if (hygiene > 0)
        {
            hygiene -= (0.5f * timer);
        }
        
        if (hygiene <= 0)
        {
            hygiene = 0;
        }
    
        if (happiness > 0)
        {
            happiness -= (0.5f * timer);
        }
        
        if (happiness <= 0)
        {
            happiness = 0;
        }
    
        if (energy > 0)
        {
            energy -= (0.5f * timer);
        }
        
        if (energy <= 0)
        {
            energy = 0;
        }

        life = fullness + hygiene + happiness + energy;
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

    Boolean isFaint()
    {
        if (life <= 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    Boolean isHealthy()
    {
        if (fullness >= 80 && hygiene >= 80 && happiness >= 80 && energy >= 80)
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
        food.transform.position = new Vector3(-4, 4, -4);
        // fullness += 10f;
        // reactionText.text = "Yum!";
        // ShowReactionText(); 
    }

   public void CleanThePet()
   {
       hygiene += 10f;
       reactionText.text = "Wow, so fresh!";
       ShowReactionText();
   }
   
   public void PlayThePet()
   {
       happiness += 10f;
       reactionText.text = "Fun!";
       ShowReactionText();
   }

   public void SleepThePet()
   {
       energy += 10f;
       reactionText.text = "zzz...";
       ShowReactionText();
   }
   
   private void OnCollisionEnter(Collision col)
   {
       if (col.gameObject.CompareTag("Draggable"))
       {
           col.gameObject.transform.position = new Vector3(-11, 1, 0.3f);
           fullness += 10f;
           // *FIXME
           // *gameobject is the Food, NavScript is a component of the Pet. Currently solved by changing destination to the bed
           // gameObject.GetComponent<NavScript>().enabled = false;
           reactionText.text = "Yum yum... Is it my birthday?";
           ShowReactionText();
       }
   }
   
    void SetStatusText()
    {
        statusText.text = "Fullness: " + fullness.ToString() + "\n" + 
                          "Hygiene: " + hygiene.ToString() + "\n" +
                          "Happiness: " + happiness.ToString() + "\n" +
                          "Energy: " + energy.ToString();
    }

    void SetAlertText()
    {
        if (isHungry())
            hungrystate = "hungry ";
        if (!isHungry())
            hungrystate = "";
        
        if (isDirty())
            cleanstate = "dirty ";
        if (!isDirty())
            cleanstate = "";
        
        if (isUnhappy())
            happystate = "unhappy ";
        if (!isUnhappy())
            happystate = "";
        
        if (isSleepy())
            energystate = "sleepy ";
        if (!isSleepy())
            energystate = "";
    }

    void ShowReactionText()
    {
        reactionText.enabled = true;
        Invoke("HideReactionText", 3);
    }

    void HideReactionText()
    {
        reactionText.enabled = false;
    }

    void SendAlert()
    {
        if (isFaint())
            alertText.text = "Oh no, Your pet fainted!";
        
        else if (isHealthy())
            alertText.text = "Your pet seems healthy and satisfied!";

        else if (isHungry() || isUnhappy() || isDirty() || isSleepy())
            alertText.text = "Your pet seems " + hungrystate + cleanstate + happystate + energystate;

        else
            alertText.text = "";
    }

    void PressOneToFaint()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fullness = 0;
            hygiene = 0;
            happiness = 0;
            energy = 0;
        }
    }
    
    // Update is called once per frame
    void Update()
    { 
        timer = Time.deltaTime;
        StatusUpdate();
        SetAlertText();
        PressOneToFaint();
    }

    private void FixedUpdate()
    {
        SetStatusText();
        SendAlert();
    }
}

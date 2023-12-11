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
    public TextMeshProUGUI growthHealthText;
    public TextMeshProUGUI winLoseText;
    
    public float fullness;
    public float hygiene;
    public float happiness;
    public float energy;
    public float life;

    private float growthPoint;
    private float healthPoint;

    public GameObject food;
    
    private float timer;
    private float timespeed = 0.2f;

    private float growthTimer;
    private float normalGrowthTimer;
    private float goodGrowthTimer;
    private float greatGrowthTimer;
    private float faintTimer;
    
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

        growthPoint = 0;
        healthPoint = 3;

        growthTimer = 30f;
        normalGrowthTimer = 30f;
        goodGrowthTimer = 30f;
        greatGrowthTimer = 30f;
        faintTimer = 20f;
        
        SetStatusText();
        SetAlertText();
        HideReactionText();
        SendAlert();
        winLoseText.enabled = false;

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
            fullness -= (timespeed * timer);
        }

        if (fullness <= 0)
        {
            fullness = 0;
        }

        if (hygiene > 0)
        {
            hygiene -= (timespeed * timer);
        }
        
        if (hygiene <= 0)
        {
            hygiene = 0;
        }
    
        if (happiness > 0)
        {
            happiness -= (timespeed * timer);
        }
        
        if (happiness <= 0)
        {
            happiness = 0;
        }
    
        if (energy > 0)
        {
            energy -= (timespeed * timer);
        }
        
        if (energy <= 0)
        {
            energy = 0;
        }

        life = fullness + hygiene + happiness + energy;
    }

    void startGrowthTimer()
    {
        if (growthTimer >= 0)
        {
            growthTimer -= Time.deltaTime;
        }
        
        else if (growthTimer < 0)
        {
            growthPoint += 0.1f;
            growthTimer += 30f;
        }
    }

    void startNormalGrowthTimer()
    {
        startGrowthTimer();
        
        if (normalGrowthTimer >= 0)
        {
            normalGrowthTimer -= Time.deltaTime;
        }
        
        else if (normalGrowthTimer < 0)
        {
            growthPoint += 0.1f;
            healthPoint += 0.1f;
            normalGrowthTimer += 30f;
        }
    }

    void startGoodGrowthTimer()
    {
        startGrowthTimer();
        startNormalGrowthTimer();
        
        if (goodGrowthTimer >= 0)
        {
            goodGrowthTimer -= Time.deltaTime;
        }
        
        else if (goodGrowthTimer < 0)
        {
            growthPoint += 0.1f;
            goodGrowthTimer += 30f;
        }
    }

    void startGreatGrowthTimer()
    {
        startGrowthTimer();
        startNormalGrowthTimer();
        startGoodGrowthTimer();
        
        if (greatGrowthTimer >= 0)
        {
            greatGrowthTimer -= Time.deltaTime;
        }
        
        else if (greatGrowthTimer < 0)
        {
            growthPoint += 0.1f;
            greatGrowthTimer += 30f;
        }
    }

    void GrowthUpdate()
    {
        if (isFaint())
        {
            growthTimer = 30f;
            normalGrowthTimer = 30f;
            goodGrowthTimer = 30f;
            greatGrowthTimer = 30f;
        }
        
        else if (isBadCondition())
        {
            startGrowthTimer();
            normalGrowthTimer = 30f;
            goodGrowthTimer = 30f;
            greatGrowthTimer = 30f;
        }

        else if (isNormalCondition())
        {
            startNormalGrowthTimer();
            goodGrowthTimer = 30f;
            greatGrowthTimer = 30f;
        }

        else if (isGoodCondition())
        {
            startGoodGrowthTimer();
            greatGrowthTimer = 30f;
        }

        else if (isGreatCondition())
        {
            startGreatGrowthTimer();
        }
    }

    void startFaintTimer()
    {
        if (faintTimer >= 0)
        {
            faintTimer -= Time.deltaTime;
        }
        
        else if (faintTimer < 0)
        {
            healthPoint -= 1;
            faintTimer += 20f;
        }
    }

    void FaintCheck()
    {
        if (isFaint())
        {
            startFaintTimer();
        }

        else
        {
            faintTimer = 20f;
        }
    }

    void HealthUpdate()
    {
        FaintCheck();

        if (healthPoint <= 0)
        {
            healthPoint = 0;
        }
        
        else if (healthPoint > 3)
        {
            healthPoint = 3;
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

    Boolean isGreatCondition()
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

    Boolean isGoodCondition()
    {
        if (!isGreatCondition() && !(isHungry() || isUnhappy() || isDirty() || isSleepy()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Boolean isNormalCondition()
    {
        if (!isFaint() && !isBadCondition() && (isHungry() || isUnhappy() || isDirty() || isSleepy()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    Boolean isBadCondition()
    {
        if (!isFaint() && (isHungry() && isUnhappy() && isDirty() && isSleepy()))
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
        statusText.text = "Fullness: " + fullness.ToString("F1") + "\n" + 
                          "Hygiene: " + hygiene.ToString("F1") + "\n" +
                          "Happiness: " + happiness.ToString("F1") + "\n" +
                          "Energy: " + energy.ToString("F1");
        
        growthHealthText.text = "Growth Point: " + growthPoint.ToString("F1") + "\n" 
                                + "Health Point:" + healthPoint.ToString("F1") + "\n" 
                                + "Growth Timer: " + growthTimer.ToString(("F3")) + "\n" 
                                + "Faint Timer: " + faintTimer.ToString(("F3"));
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
        
        else if (isGreatCondition())
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

    void PressTwoToGrow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            growthPoint += 1;
        }
    }

    void PressThreeToLoseHealth()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            healthPoint -= 1;
        }
    }

    void EndingCheck()
    {
        if (healthPoint <= 0)
        {
            winLoseText.text = "GAME OVER" + "\n" + "Your pet is dead...";
            winLoseText.enabled = true;
        }
        
        else if (growthPoint >= 3)
        {
            winLoseText.text = "STAGE CLEAR" + "\n" + "Your pet has grown up!";
            winLoseText.enabled = true;
        }
        

    }
    
    // Update is called once per frame
    void Update()
    { 
        timer = Time.deltaTime;
        StatusUpdate();
        SetAlertText();
        PressOneToFaint();
        PressTwoToGrow();
        PressThreeToLoseHealth();
        HealthUpdate();
        GrowthUpdate();
    }

    private void FixedUpdate()
    {
        SetStatusText();
        SendAlert();
        EndingCheck();
    }
}

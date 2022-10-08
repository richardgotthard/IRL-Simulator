using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentStats : MonoBehaviour
{
   StatsUI statsUI;
   public float hunger = 75;
   public float energy = 75;
   public float fun = 75;

   public float happiness = 0;

[Header("UI")]
   public Image hungerBar;
   public Image energyBar;
   public Image funBar;
   public Image happinessBar;
   
    void Start()
    {
     statsUI = GetComponent<StatsUI>();
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        DecayNeeds(Time.deltaTime);
    }

    void DecayNeeds(float _deltaTime)
    {

        hunger = Mathf.Clamp(hunger - _deltaTime/(0.02f*hunger),0,100);
        energy = Mathf.Clamp(energy - _deltaTime/(0.06f*energy),0,100);
        fun = Mathf.Clamp(fun - _deltaTime/(0.01f*fun),0,100);
        CalculateHappiness();
        SetStats();
       
    }

    
    public void IncreaseHunger(float amount)
    {
        hunger += amount; 
    }
    public void IncreaseEnergy(float amount)
    {
        energy += amount; 
    }
    public void IncreaseFun(float amount)
    {
        fun += amount; 
    }

    public void ResetHunger()
    {
        hunger = 95; 
    }
    public void ResetEnergy()
    {
        energy = 95;  
    }
    public void ResetFun()
    {
        fun = 95; 
    }
    
     void CalculateHappiness()
    {

       happiness = (hunger + energy + fun)/3;
       
    }

    void SetStats(){
    statsUI.hungerValue.text = hunger.ToString();
    statsUI.energyValue.text = energy.ToString();
    statsUI.funValue.text = fun.ToString();
    statsUI.happinessValue.text = happiness.ToString();

    hungerBar.fillAmount = hunger/100;
   energyBar.fillAmount = energy/100;
   funBar.fillAmount = fun/100;
   happinessBar.fillAmount = happiness/100;



   }
}

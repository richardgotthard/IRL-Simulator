using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentStats : MonoBehaviour
{
   StatsUI statsUI;
   public float hunger = 100;
   public float energy = 100;
   public float fun = 100;

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
        switch (state)
        {
            // chilling
            case 0:
                //FollowPlayer(); // TODO: change to some idle animation
                //ActionSelection();
                //if (actions.Count > 0) ExitState();
                break;
            // performing action
            case 1:
                //timeSpentOnAction += Time.deltaTime;
                //if (timeSpentOnAction >= currentAction.timeToComplete) ExitState();
                break;

        }
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

   private void ExitState()
    {
        int _state = state;
        switch (_state)
        {
            // get next action and start walking to it
            case 0:
                //currentAction = actions.Peek();
                //state = 2;
                break;
            // complete action and start chilling
            case 1:
                //currentAction.CompleteAction(this);
                //state = 0;
                break;

        }
    }

   


}

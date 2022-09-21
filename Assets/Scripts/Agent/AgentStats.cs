using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStats : MonoBehaviour
{
   StatsUI statsUI;
   public float hunger = 50;
   public float energy = 90;
   public float fun = 80;

   private int state = 0;   
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

        hunger -= _deltaTime;
        energy -= _deltaTime;
        fun -= _deltaTime;
        SetStats();
       
    }

    void SetStats()
    {
    statsUI.hungerValue.text = hunger.ToString();
    statsUI.hungerValue.text = energy.ToString();
    statsUI.hungerValue.text = fun.ToString();
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

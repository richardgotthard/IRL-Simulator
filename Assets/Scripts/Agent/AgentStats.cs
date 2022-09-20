using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStats : MonoBehaviour
{
   StatsUI statsUI;
   public float hunger = 100;
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

        hunger -= _deltaTime;
        SetStats();
       
    }

    void SetStats(){
    statsUI.hungerValue.text = hunger.ToString();
   }
}

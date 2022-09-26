using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
   public Text hungerValue;
   public Text energyValue;
   public Text funValue;
   public Text happinessValue;

AgentStats agentStats;

   void Start()
   {
    agentStats = GetComponent<AgentStats>();
   }
   
}

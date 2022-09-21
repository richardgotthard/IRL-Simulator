using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Behavior : MonoBehaviour
{

    AgentStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<AgentStats>();
         InvokeRepeating("ActionSelection", 2.0f, 10f);
        
    }

    // Update is called once per frame
    void Update()
    {
           
        
    }
    void ActionSelection(){
        float minimumNeed = MinimumNeed();
        if( minimumNeed == stats.hunger){
            Debug.Log("EAT");
            stats.IncreaseHunger(1500/stats.hunger);
        }
        else if(minimumNeed == stats.energy){
            Debug.Log("SLEEP" + 1500/stats.energy);
            stats.IncreaseEnergy(1500/stats.energy);
        }
        else if(minimumNeed == stats.fun){
            Debug.Log("PLAY" + 1500/stats.fun); 
            stats.IncreaseFun(1500/stats.fun);
        }
        else {
            Debug.Log("NONE"); 
           
        }

    }

    float MinimumNeed(){
        return Mathf.Min(stats.hunger, stats.energy, stats.fun);
    }
   
}

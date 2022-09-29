using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BaseNavigation))]
public class Behavior : MonoBehaviour
{
    protected BaseNavigation Navigation;

    private Transform objectPos;

    AgentStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<AgentStats>();
       // agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
         InvokeRepeating("ActionSelection", 2.0f, 10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectPos){
             MoveTowardsTarget();
        }
        
    }
    void ActionSelection(){
        float minimumNeed = MinimumNeed();
        if( minimumNeed == stats.hunger){
            //Navigation.transform(0,0,-8.55f);
            objectPos = GameObject.FindGameObjectWithTag("Kitchen").transform;
            Debug.Log(objectPos);
            //Navigation.SetDestination(GameObject.FindGameObjectWithTag("Kitchen")transform.position);
            Debug.Log("EAT");
            stats.IncreaseHunger(1500/stats.hunger);
        }
        else if(minimumNeed == stats.energy){
            objectPos = GameObject.FindGameObjectWithTag("Bedroom").transform;
            Debug.Log(objectPos);
            Debug.Log("SLEEP" + 1500/stats.energy);
            stats.IncreaseEnergy(1500/stats.energy);
        }
        else if(minimumNeed == stats.fun){
            objectPos = GameObject.FindGameObjectWithTag("Livingroom").transform;
            Debug.Log(objectPos);
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


    private void MoveTowardsTarget()
    {
            transform.position = Vector3.MoveTowards(transform.position, objectPos.position, 2*Time.deltaTime);
    }
   
}

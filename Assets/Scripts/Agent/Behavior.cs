using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Behavior : MonoBehaviour
{

    
   
    AgentStats stats;
    public GameObject Kitchen;
    public GameObject Bedroom;
    public GameObject Livingroom;
    UnityEngine.AI.NavMeshAgent agent;

    //public enum ActionState { IDLE, WORKING}
    //ActionState state = ActionState.IDLE;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<AgentStats>();
       // agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
         InvokeRepeating("ActionSelection", 2.0f, 10f);
         agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
       float moveHorizontal = Input.GetAxis ("Horizontal");
       float moveVertical = Input.GetAxis ("Vertical");
        
    }
    void ActionSelection(){
        float minimumNeed = MinimumNeed();
        if( minimumNeed == stats.hunger){
            //Navigation.transform(0,0,-8.55f);
            //objectPos = GameObject.FindGameObjectWithTag("Kitchen").transform;
            agent.SetDestination(Kitchen.transform.position);
            Debug.Log(agent.SetDestination(Kitchen.transform.position));
            //Navigation.SetDestination(GameObject.FindGameObjectWithTag("Kitchen")transform.position);
            Debug.Log("EAT");
            stats.IncreaseHunger(1500/stats.hunger);
        }
        else if(minimumNeed == stats.energy){
            //objectPos = GameObject.FindGameObjectWithTag("Bedroom").transform;
            agent.SetDestination(Bedroom.transform.position);
            Debug.Log(agent.SetDestination(Bedroom.transform.position));
            Debug.Log("SLEEP" + 1500/stats.energy);
            stats.IncreaseEnergy(1500/stats.energy);
        }
        else if(minimumNeed == stats.fun){
            //objectPos = GameObject.FindGameObjectWithTag("Livingroom").transform;
            agent.SetDestination(Livingroom.transform.position);
            Debug.Log(agent.SetDestination(Livingroom.transform.position));
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

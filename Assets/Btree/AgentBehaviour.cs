using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    AgentStats stats;

    Alarm alarmscript;

    //Rigidbody m_Rigidbody;

    BehaviourTree tree;
    public GameObject kitchen; 
    public GameObject bedroom;
    public GameObject livingroom;
    public GameObject bathroom;
    public GameObject fridge;
    public GameObject workstation;
    public GameObject alarmclock;
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;
   
    float timer = 0.0f;
    bool timerStart = false;

    Node.Status treeStatus = Node.Status.RUNNING;


    void Start()
    {
        //m_Rigidbody = GetComponent<Rigidbody>();
        alarmscript = alarmclock.GetComponent<Alarm>();
        stats = GetComponent<AgentStats>();
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Selector root = new Selector("Check alarm");
        Selector doSomething = new Selector("Do an activity");

        Sequence emergency = new Sequence("Emergency");

        Leaf checkAlarm = new Leaf("Checks for alarm", alarmIsOn);
        Leaf goToAlarm = new Leaf("Is hungry", GoToAlarm);
        Leaf fixAlarm = new Leaf("Is hungry", TurnOffAlarm);

        Leaf needsBathroom = new Leaf("Needs bathroom", isInNeedOfBathroom);
        Leaf goToBathroom = new Leaf("Go to bathroom", GoToBathroom);
        Leaf useBathroomNow = new Leaf("Use bathroom", UseBathroom);

        Leaf isHungryNow = new Leaf("Is hungry", isHungry);
        Leaf goToKitchen = new Leaf("Prepare food", GoToKitchen);
        Leaf goToFridge = new Leaf("Go to fridge", GoToFridge);
        Leaf eat = new Leaf("Eat", Eat);

        Leaf isSleepyNow = new Leaf("Is hungry", isSleepy);
        Leaf goToBedroom = new Leaf("Go to bed", GoToBedroom);
        Leaf sleepNow = new Leaf("Sleep", Sleep);

        Leaf isBorednow = new Leaf("Is hungry", isBored);
        Leaf goToLivingroom = new Leaf("Go to TV", GoToLivingroom);
        Leaf watchTV = new Leaf("Watch TV", WatchTV);

        Leaf goToWork = new Leaf("Do some work", GoToWorkstation);

        Sequence eatsomething = new Sequence("Eat something");
        Sequence sleep = new Sequence("Go and sleep");
        Sequence haveFun = new Sequence("Go and have fun");
        Sequence useBathroom = new Sequence("Use bathroom");

        //for Hunger
        eatsomething.AddChild(isHungryNow);
        eatsomething.AddChild(goToFridge);
        eatsomething.AddChild(goToKitchen);
        eatsomething.AddChild(eat);

        //for Sleep
        sleep.AddChild(isSleepyNow);
        sleep.AddChild(goToBedroom);
        sleep.AddChild(sleepNow);

        //for Fun
        haveFun.AddChild(isBorednow);
        haveFun.AddChild(goToLivingroom);
        haveFun.AddChild(watchTV);

         //for Bladder
        useBathroom.AddChild(needsBathroom);
        useBathroom.AddChild(goToBathroom);
        useBathroom.AddChild(useBathroomNow);

        //add all needs
        doSomething.AddChild(eatsomething);
        doSomething.AddChild(sleep);
        doSomething.AddChild(haveFun);
        doSomething.AddChild(useBathroom);
        doSomething.AddChild(goToWork);

        //add dependency for alarm
        emergency.AddChild(checkAlarm);
        emergency.AddChild(goToAlarm);
        emergency.AddChild(fixAlarm);

        //Check for alarm
        root.AddChild(emergency);
        root.AddChild(doSomething);
        
        //add tree structure to tree
        tree.AddChild(root);
        tree.PrintTree();   
    }

    // Condition nodes
    public Node.Status isHungry()
    {
        if(stats.hunger <= 90)
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

    public Node.Status isSleepy()
    {
        if(stats.energy <= 30 )
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

    public Node.Status isBored()    
    {
        if(stats.fun <= 70)
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

     public Node.Status isInNeedOfBathroom()    
    {
        if(stats.hygiene <= 70)
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

     public Node.Status alarmIsOn()    
    {
        if(alarmscript.alarm == true){
        
            return Node.Status.SUCCESS;
        } else{
        
        return Node.Status.FAILURE;
        }
    }
    /////////////////////////////////////////


    // Location nodes
    public Node.Status GoToKitchen()
    {
       return GoToLocation(kitchen.transform.position);  
    }

    public Node.Status GoToFridge()
    {
       return GoToLocation(fridge.transform.position);
    }

    public Node.Status GoToBedroom()
    {
       return GoToLocation(bedroom.transform.position);   
    }

    public Node.Status GoToLivingroom()
    {
       return  GoToLocation(livingroom.transform.position);
    }

     public Node.Status GoToBathroom()
    {
       return  GoToLocation(bathroom.transform.position);
    }

    public Node.Status GoToWorkstation()
    {
       return GoToLocation(workstation.transform.position);
    }

      public Node.Status GoToAlarm()
    {
       return GoToLocationAlarm(alarmclock.transform.position);
    }


    // Activity nodes

    public Node.Status Eat()
    {  
       Node.Status s = DoActivity(5f);
       if(s == Node.Status.SUCCESS){
         //stats.IncreaseHunger(1500/stats.hunger);
         stats.ResetHunger();
       }
       return s;
    }

    public Node.Status Sleep()
    {      
       Node.Status s = DoActivity(10f);
       if(s == Node.Status.SUCCESS){
         //stats.IncreaseEnergy(1500/stats.energy);
         stats.ResetEnergy();
       }
       return s;
    }

    public Node.Status WatchTV()
    {     
       Node.Status s = DoActivity(5f);

       if(s == Node.Status.SUCCESS){
         //stats.IncreaseFun(1500/stats.fun);
         stats.ResetFun();
       }
       return s;
    }

     public Node.Status UseBathroom()
    {     
       Node.Status s = DoActivity(5f);

       if(s == Node.Status.SUCCESS){
         //stats.IncreaseFun(1500/stats.fun);
         stats.ResetBladder();
       }
       return s;
    }

    public Node.Status TurnOffAlarm()
    {     
       Node.Status s = DoActivity(3f);

       if(s == Node.Status.SUCCESS)
       {
            alarmscript.alarm = false;
       }
       return s;
    }

    ///////////////////////////////////////////////////


    // Reusable functions to return node status

    Node.Status DoActivity(float duration)
    {
       
        timer += Time.deltaTime;
        state = ActionState.WORKING;

    //    if(alarmscript.alarm == true)
    //     {
            if(state == ActionState.WORKING )
            {       
                if(timer >= duration)
                {
                    timer = 0;
                    state = ActionState.IDLE;
                    return Node.Status.SUCCESS;
                }
            
            }
            return Node.Status.RUNNING;
    //     }
    //    else
    //     {
    //         return Node.Status.RUNNING;
    //     }
    }

   

    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (alarmscript.alarm == false)
       {
            if (state == ActionState.IDLE)
            {
                agent.SetDestination(destination);
                state = ActionState.WORKING;
            }
            else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2)
            {
                state = ActionState.IDLE;
                return Node.Status.FAILURE;
            }
            else if (distanceToTarget < 2)
            {
                state = ActionState.IDLE;
                return Node.Status.SUCCESS;
            }
            return Node.Status.RUNNING;
        }
        else{
            return Node.Status.FAILURE;
        }
    }

    Node.Status GoToLocationAlarm(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
     
            if (state == ActionState.IDLE)
            {
                agent.SetDestination(destination);
                state = ActionState.WORKING;
            }
            else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2)
            {
                state = ActionState.IDLE;
                return Node.Status.FAILURE;
            }
            else if (distanceToTarget < 2)
            {
                state = ActionState.IDLE;
                return Node.Status.SUCCESS;
            }
            return Node.Status.RUNNING;
  
    }


    // Update is called once per frame
    void Update()
    {
       // treeStatus = tree.Process();
        if(treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();
            //Debug.Log(tree);
        }
        else{
           
            treeStatus = Node.Status.RUNNING;
        }
    }
}

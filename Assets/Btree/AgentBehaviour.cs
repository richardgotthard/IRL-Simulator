using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    AgentStats stats;


    BehaviourTree tree;
    public GameObject kitchen; 
    public GameObject bedroom;
    public GameObject livingroom;
    public GameObject fridge;
    public GameObject workstation;
    public GameObject alarmclock;
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;

   // public enum AlarmState { ON, OFF};
   // AlarmState alarm;
    //bool _alarm = false;
    Alarm alarmscript;

    float timer = 0.0f;
    bool timerStart = false;

    Node.Status treeStatus = Node.Status.RUNNING;


    void Start()
    {
        Debug.Log(alarmclock.GetComponent<Alarm>().alarm);

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

        //doSomething.AddChild(goToKitchen);
        doSomething.AddChild(eatsomething);
        //sleep.AddChild(goToBedroom);
        //entertain.AddChild(goToLivingroom);
        doSomething.AddChild(sleep);
       // tree.AddChild(sleep);
       // tree.AddChild(entertain);
        doSomething.AddChild(haveFun);

        doSomething.AddChild(goToWork);

        emergency.AddChild(checkAlarm);
        emergency.AddChild(goToAlarm);
        emergency.AddChild(fixAlarm);
        //Check for alarm
        root.AddChild(emergency);
        root.AddChild(doSomething);
        

        tree.AddChild(root);
        tree.PrintTree();   
    }

    // Condition nodes
    public Node.Status isHungry()
    {
        if(stats.hunger <= 60)
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

     public Node.Status alarmIsOn()    
    {
        if(alarmscript.alarm == true){
           // Debug.Log("true");
            return Node.Status.SUCCESS;
        } else{
        //Debug.Log("false");
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

    public Node.Status GoToWorkstation()
    {
       return GoToLocation(workstation.transform.position);
    }

      public Node.Status GoToAlarm()
    {
       return GoToLocation(alarmclock.transform.position);
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

    public Node.Status TurnOffAlarm()
    {     
       Node.Status s = DoActivity(5f);

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

        if(state == ActionState.WORKING)
        {       
            if(timer >= duration){
                timer = 0;
                state = ActionState.IDLE;
                return Node.Status.SUCCESS;
            }
        
        }
        return Node.Status.RUNNING;
    }

   

    Node.Status GoToLocation(Vector3 destination)
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

        }
        else{
           
            treeStatus = Node.Status.RUNNING;
        }
    }
}

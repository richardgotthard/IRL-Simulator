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
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;

    Node.Status treeStatus = Node.Status.RUNNING;



    void Start()
    {
        stats = GetComponent<AgentStats>();
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Selector doSomething = new Selector("Do an activity");
        Leaf isHungryNow = new Leaf("Is hungry", isHungry);
        Leaf goToKitchen = new Leaf("Eat from microwave", GoToKitchen);
        Leaf goToFridge = new Leaf("Go to fridge", GoToFridge);
        Leaf isSleepyNow = new Leaf("Is hungry", isSleepy);
        Leaf goToBedroom = new Leaf("Sleep at bed", GoToBedroom);
        Leaf isBorednow = new Leaf("Is hungry", isBored);
        Leaf goToLivingroom = new Leaf("Watch some TV", GoToLivingroom);
        Leaf goToWork = new Leaf("Do some work", GoToWorkstation);

        Sequence eatsomething = new Sequence("Eat something");
        Sequence sleep = new Sequence("Go and sleep");
        Sequence haveFun = new Sequence("Go and have fun");

        //for Hunger
        eatsomething.AddChild(isHungryNow);
        eatsomething.AddChild(goToFridge);
        eatsomething.AddChild(goToKitchen);

        //for Sleep
        sleep.AddChild(isSleepyNow);
        sleep.AddChild(goToBedroom);

        //for Fun
        haveFun.AddChild(isBorednow);
        haveFun.AddChild(goToLivingroom);

        //doSomething.AddChild(goToKitchen);
        doSomething.AddChild(eatsomething);   
        //sleep.AddChild(goToBedroom);
        //entertain.AddChild(goToLivingroom);
        doSomething.AddChild(sleep);
       // tree.AddChild(sleep);
       // tree.AddChild(entertain);
        doSomething.AddChild(haveFun);

        doSomething.AddChild(goToWork);

        tree.AddChild(doSomething);

        tree.PrintTree();   
    }

    public Node.Status isHungry()
    {
        if(stats.hunger <= 90 )
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

    public Node.Status isSleepy()
    {
        if(stats.energy <= 90 )
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

    public Node.Status isBored()    
    {
        if(stats.fun <= 90 )
            return Node.Status.SUCCESS;
        return Node.Status.FAILURE;
    }

    public Node.Status GoToKitchen()
    {
        Node.Status s = GoToLocation(kitchen.transform.position);

        if(s == Node.Status.SUCCESS)
        {
           // agent.sleep(5000);
            stats.IncreaseHunger(1500/stats.hunger);

        }

       return s;
    }

    public Node.Status GoToFridge()
    {
       return GoToLocation(fridge.transform.position);
    }

     public Node.Status GoToBedroom()
    {
        Node.Status s = GoToLocation(bedroom.transform.position);
        //yield new WaitForSeconds(3);

        if(s == Node.Status.SUCCESS)
        {
             
            stats.IncreaseEnergy(1500/stats.energy);

        }

       return s;
    }

      public Node.Status GoToLivingroom()
    {
       Node.Status s = GoToLocation(livingroom.transform.position);

        if(s == Node.Status.SUCCESS)
        {
           // agent.sleep(5000);
            stats.IncreaseFun(1500/stats.fun);

        }

       return s;
    }

      public Node.Status GoToWorkstation()
    {
       return GoToLocation(workstation.transform.position);

    
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
        if(treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();

        }
        else{
           
            treeStatus = Node.Status.RUNNING;
        }
    }
}

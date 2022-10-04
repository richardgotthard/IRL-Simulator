using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    BehaviourTree tree;
    public GameObject kitchen; 
    public GameObject bedroom;
    public GameObject livingroom;
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;

    Node.Status treeStatus = Node.Status.RUNNING;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence doSomething = new Sequence("Do an activity");
        Leaf goToKitchen = new Leaf("Eat from microwave", GoToKitchen);
        Leaf goToBedroom = new Leaf("Sleep at bed", GoToBedroom);
        Leaf goToLivingroom = new Leaf("Watch some TV", GoToLivingroom);

        doSomething.AddChild(goToKitchen);
        //sleep.AddChild(goToBedroom);
        //entertain.AddChild(goToLivingroom);
        doSomething.AddChild(goToBedroom);
       // tree.AddChild(sleep);
       // tree.AddChild(entertain);
        doSomething.AddChild(goToLivingroom);
        tree.AddChild(doSomething);

        tree.PrintTree();   
    }

    public Node.Status GoToKitchen()
    {
       return GoToLocation(kitchen.transform.position);
    }

     public Node.Status GoToBedroom()
    {
       return GoToLocation(bedroom.transform.position);
    }

      public Node.Status GoToLivingroom()
    {
       return GoToLocation(livingroom.transform.position);
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
    }
}

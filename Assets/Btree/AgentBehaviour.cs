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


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Node doSomething = new Node("Do an activity");
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

        tree.Process();
    }

    public Node.Status GoToKitchen()
    {
        agent.SetDestination(kitchen.transform.position);
        return Node.Status.SUCCESS;
    }

     public Node.Status GoToBedroom()
    {
        agent.SetDestination(bedroom.transform.position);
        return Node.Status.SUCCESS;
    }

      public Node.Status GoToLivingroom()
    {
        agent.SetDestination(livingroom.transform.position);
        return Node.Status.SUCCESS;
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
        
    }
}

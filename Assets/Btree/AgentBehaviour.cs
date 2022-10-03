using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    BehaviourTree tree;

    void Start()
    {
        tree = new BehaviourTree();
        Node eat = new Node("Eat from microwave");
        Node sleep = new Node("Sleep at bed");
        Node entertain = new Node("Watch some TV");

        eat.AddChild(goToKitchen);
        sleep.AddChild(goToBedroom);
        entertain.AddChild(goToLivingroom);
        tree.AddChild(eat);
        tree.AddChild(sleep);
        tree.AddChild(entertain);

        tree.PrintTree();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

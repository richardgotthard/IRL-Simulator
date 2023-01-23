# IRL-Simulator

This paper regards the implementation of a game in Unity called ”IRL-Simulator”. It is developed as a project in
the course ”Artificial Intelligence for interactive media” [TNM095] at Linköping University. The main aim for the
game is to see how realistic an AI will behave when given the same ”needs” as a real human being,
similar to how the ”The Sims”-games work. The actions of the AI are actualized through a ”Behavior tree”, where
the needs decides the traversal of the tree. The visualization of both the needs and scene is done through Unity.
A* pathfinding is utilized to navigate a NavMesh around different interactable objects in the scene. Both the
tree and the pathfinder are based around states which symbolize how and what the agent is doing. Specifically,
”Success/Running/Failure” for the tree and ”Idle/ Working” for the pathfinder. A random, user initiated, event is
also implemented to both allow user interaction and test the AI’s ability to adapt. 

Game: https://simmer.io/@AdamGT/irl-simulator

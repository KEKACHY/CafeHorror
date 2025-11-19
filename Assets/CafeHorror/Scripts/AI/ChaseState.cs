using UnityEngine;

public class ChaseState : IState
{
    private readonly AIController _controller;
    public ChaseState(AIController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.Agent.speed = _controller.ChaseSpeed;
        _controller.Agent.stoppingDistance = _controller.StoppingDistance;
        _controller.GetComponent<Collider>().isTrigger = true;
    }

    public void Update()
    {
        _controller.Agent.SetDestination(_controller.Player.position);
    }

    public void Exit()
    {
        
    }
}
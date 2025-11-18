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
    }

    public void Update()
    {
        if (PlayerInRange())
        {
            //TODO сделать убийство
            return;
        }
        _controller.Agent.SetDestination(_controller.Player.position);
    }

    public void Exit()
    {
        
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(_controller.transform.position, _controller.Player.position) <= _controller.Agent.stoppingDistance;
    }
}
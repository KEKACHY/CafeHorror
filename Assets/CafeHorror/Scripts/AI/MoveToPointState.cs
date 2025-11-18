using UnityEngine;

public class MoveToPointState : IState
{
    private readonly AIController _controller;
    public MoveToPointState(AIController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.Agent.speed = _controller.MoveSpeed;
        _controller.Agent.stoppingDistance = _controller.StoppingDistance;
        _controller.Agent.SetDestination(_controller.Target.position);
    }

    public void Update()
    {
        if(_controller.Agent.pathPending)
            return;

        if(_controller.Agent.remainingDistance <= _controller.StoppingDistance)
        {
            _controller.StateMachine.ChangeState(new WaitingState(_controller));
        }
    }

    public void Exit()
    {}
}
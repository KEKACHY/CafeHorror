using UnityEngine;

public class WaitingState : IState
{
    private readonly AIController _controller;
    private float _timer;
    public WaitingState(AIController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _timer = _controller.WaitTime;
        _controller.Agent.ResetPath();
    }

    public void Update()
    {
        if(_controller.TakedItem || _timer <= 0f)
        {
            _controller.StateMachine.ChangeState(new ChaseState(_controller));
            Debug.Log(_timer);
            return;
        }
        _timer -= Time.deltaTime;
    }

    public void Exit()
    {
        
    }
}
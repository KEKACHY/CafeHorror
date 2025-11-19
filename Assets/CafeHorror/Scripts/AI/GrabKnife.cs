using UnityEngine;

public class GrabKnife : IState
{
    private readonly AIController _controller;
    public GrabKnife(AIController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.Knife.SetActive(true);
    }

    public void Update()
    {
        AnimatorStateInfo stateInfo = _controller.Animator.GetCurrentAnimatorStateInfo(0);   
        if (stateInfo.IsName("Grab") && stateInfo.normalizedTime >= 1f)
        {
            _controller.Animator.SetBool(_controller.HasKnife, true);
            _controller.StateMachine.ChangeState(new ChaseState(_controller));
        } 
    }

    public void Exit()
    {
        
    }

}
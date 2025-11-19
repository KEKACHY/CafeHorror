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
        DialogueManager.Instance.StartDialogue(_controller.Dialogues[0]);
    }

    public void Update()
    {
        if(_controller.TakedItem || _timer <= 0f)
        {
            if(_controller.TakedItem)
                DialogueManager.Instance.StartDialogue(_controller.Dialogues[1]);
            else
                DialogueManager.Instance.StartDialogue(_controller.Dialogues[2]);

            _controller.Animator.SetBool(_controller.WantKill, true);
            _controller.StateMachine.ChangeState(new GrabKnife(_controller));
            return;
        }
        _timer -= Time.deltaTime;
    }

    public void Exit()
    {
        
    }
}
using Godot;
using System;

public partial class IdleState : BaseState
{
    public Player player;

    public IdleState(Player player) {
        this.player = player;
    }
    public override void OnEnterState() { }
    public override void Update(double delta) {
        if (player != null) {
            if (player.GetFrontBackValue() != 0 && player.GetSideValue() != 0) {
                player.GetStateMachine().ChangeState("Run");
            }
        }
    }
    public override void PhysicsUpdate(double delta) { 
    
    }
    public override void OnExitState() {

    }
}

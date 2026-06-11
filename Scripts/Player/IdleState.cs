using Godot;
using System;

public partial class IdleState : BaseState
{
    public Player player;

    public IdleState(Player player) {
        this.player = player;
    }
    public override void OnEnterState() {
        GD.Print("Yo, this is the Idle State");
    }
    public override void Update(double delta) {

        if (player != null) {
            if (!player.PlayerBody3D.IsOnFloor()) {
                player.GetStateMachine().ChangeState("Airborne");
            }

            if (player.GetFrontValue() != 0 || player.GetBackValue() != 0 || player.GetLeftValue() != 0 || player.GetRightValue() != 0) {
                player.GetStateMachine().ChangeState("Run");
            }
        }


    }
    public override void PhysicsUpdate(double delta) { 
    
    }
    public override void OnExitState() {
        GD.Print("Leaving the Idle state");
    }
}

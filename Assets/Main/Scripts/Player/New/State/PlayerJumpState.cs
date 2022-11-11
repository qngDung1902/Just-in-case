using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {

    int amountOfjumps;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) {
        amountOfjumps = player.Stat.amountOfjumps;
    }

    public override void Enter() {
        base.Enter();

        core.Input.UseJumpInput();
        core.Movement.SetVelocityY(player.Stat.jumpVelocity);
        isAbilityDone = true;

        DecreaseAmountOfJumps();
        player.InAirState.SetIsJumping();

    }

    public bool CanJump() {
        return amountOfjumps > 0;
    }

    public void ResetAmountOfJumps() => amountOfjumps = player.Stat.amountOfjumps;
    public void DecreaseAmountOfJumps() => amountOfjumps -= 1;
}

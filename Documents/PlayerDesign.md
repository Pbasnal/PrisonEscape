### Using the state machine architecture
It has been designed to be extensible. This state machine is concerned with only specifying the states that it has and the transition rules. States do not contain logic. Consider them as empty blocks.
The state machine is meant to be used by another component. In case of player, it is PlayerController. PlayerController attaches functionality to different states. 

This design helps in implementing different game mechanics separately which then a controller can attach to a state machine.
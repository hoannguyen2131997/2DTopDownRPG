public interface IMovementState
{
    void EnterState(StateController controller);
    void FixedUpdateState(StateController controller);
    void ExitState(StateController controller);
}

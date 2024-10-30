public interface IAttackState
{
    void EnterState(StateController controller);
    void UpdateState(StateController controller);
    void ExitState(StateController controller);
}
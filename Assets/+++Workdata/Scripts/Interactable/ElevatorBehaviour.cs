using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{
    public static int HashUp = Animator.StringToHash("Up");
    public static int HashCall = Animator.StringToHash("Call");

    [SerializeField] private Animator _anim;
    [SerializeField] private bool _elevatorIsUp;

    public void CallElevator(bool isUp)
    {
        if ((_elevatorIsUp && isUp) || (!_elevatorIsUp && !isUp)) return;
        
        _anim.SetBool(HashUp, isUp);
        _anim.SetTrigger(HashCall);
    }

    public void SwitchDirection()
    {
        _elevatorIsUp = !_elevatorIsUp;
        _anim.SetBool(HashUp, _elevatorIsUp);
        _anim.SetTrigger(HashCall);
    }
}
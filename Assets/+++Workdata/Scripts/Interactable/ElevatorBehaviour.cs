using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{
    public static int HashUp = Animator.StringToHash("Up");
    public static int HashCall = Animator.StringToHash("Call");

    
    [SerializeField] private Animator anim;

    [SerializeField] private bool _elevatorIsUp;

    public void CallElevator(bool isUp)
    {
        if ((_elevatorIsUp && isUp) || (!_elevatorIsUp && !isUp)) return;
        
        anim.SetBool(HashUp, isUp);
        anim.SetTrigger(HashCall);
    }

    public void SwitchDirection()
    {
        _elevatorIsUp = !_elevatorIsUp;
        anim.SetBool(HashUp, _elevatorIsUp);
        anim.SetTrigger(HashCall);
    }
}
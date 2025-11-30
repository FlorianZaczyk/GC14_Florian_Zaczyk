using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public static readonly int HashMovementValue = Animator.StringToHash("MovementValue");
    public static readonly int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int HashActionID = Animator.StringToHash("ActionID");

    
    
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    public void AnimationEnemyDeath()
    {
        AnimationSetActionID(100);
    }

    public void AnimationSetAttack()
    {
        AnimationSetActionID(10);
    }

    private void AnimationSetMovementValue(float value)
    {
        _anim.SetFloat(HashMovementValue, value);
    }
    
    private void AnimationSetActionID(int id)
    {
        _anim.SetTrigger(HashActionTrigger);
        _anim.SetInteger(HashActionID, id);
    }
   
    
}

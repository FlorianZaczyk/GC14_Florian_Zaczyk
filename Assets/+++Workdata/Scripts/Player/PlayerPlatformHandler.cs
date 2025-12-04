using UnityEngine;

public class PlayerPlatformHandler : MonoBehaviour
{
    private OneWayPlatformBehaviour _oneWayPlatformBehaviour;

    public void SetOneWayEffector(OneWayPlatformBehaviour currentOneWayPlatformBehaviour)
    {
        _oneWayPlatformBehaviour = currentOneWayPlatformBehaviour;
    }

    public void TryDisableOneWayEffector()
    {
        if (_oneWayPlatformBehaviour == null) return;
        
        //Befehl um Effector auszuschalten
        _oneWayPlatformBehaviour.DisableEffector();
        _oneWayPlatformBehaviour = null;
    }
    
}
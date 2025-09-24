using UnityEngine;

public class TeleportController 
{
    public void Teleport(ITeleportDestination location, Transform target) => 
        target.position = location.EntryPoint.position;
}

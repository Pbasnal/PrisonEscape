using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IExampleMessage : IEventSystemHandler
{
    void Message1();
}

public class EventListener : MonoBehaviour, IExampleMessage
{
    public void Message1()
    {
        Debug.Log("Recieved message1");
    }
}

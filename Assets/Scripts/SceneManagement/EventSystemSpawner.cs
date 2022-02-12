using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// This class exists to remove multiple event systems spawning in each scene as only one can persist
public class EventSystemSpawner : MonoBehaviour 
{

    [SerializeField] private GameObject eventSystemprefab;
    [SerializeField] private GameObject firstSelectedInvObject;
    
    void Start()
    {
        EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
        if (sceneEventSystem == null)
        {
            GameObject newEventSystem = Instantiate(eventSystemprefab);
            newEventSystem.transform.parent = transform;
            newEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedInvObject);
            FindObjectOfType<InventoryManager>().SetEventSystem(newEventSystem.GetComponent<EventSystem>());
        }

        
    }
}
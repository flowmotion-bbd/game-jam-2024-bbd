using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSingleton : MonoBehaviour
{
    public static EventSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<EventSystem>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

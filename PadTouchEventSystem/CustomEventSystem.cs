using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CustomEventSystem : SingletonMonobehaviour<CustomEventSystem>
{
    static EventSystem _current;
    static GameObject _selectedGameObject;

    public enum Dir
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public GameObject CurrentSelected
    {
        get { return _selectedGameObject; }
        set { _selectedGameObject = value; }
    }

    public EventSystem current
    {
        get {
            return _current;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _current = GetComponent<EventSystem>();
        gameObject.AddComponent<StandaloneInputModule>();
    }

    public void Enter(GameObject go)
    {
        ExecuteEvents.Execute<IPointerEnterHandler>(go, new PointerEventData(_current), ExecuteEvents.pointerEnterHandler);
    }

    public void Select(Dir dir)
    {
        if(CurrentSelected == null) { return; }

        Find(CurrentSelected, dir);
    }

    void Find(GameObject go, Dir dir)
    {
        var select = go.GetComponent<Selectable>();
        if (select == null) { return; }
        Selectable find = null;
        switch (dir)
        {
            case Dir.LEFT:
                find = select.FindSelectableOnLeft();
                break;
            case Dir.RIGHT:
                find = select.FindSelectableOnRight();
                break;
            case Dir.UP:
                find = select.FindSelectableOnUp();
                break;
            case Dir.DOWN:
                find = select.FindSelectableOnDown();
                break;
            default:
                break;
        }
        if (find != null)
        {
            Enter(find.gameObject);
        }
    }
}

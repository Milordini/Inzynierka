using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SelectMenager SLinstance;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        SLinstance = SelectMenager.GetInstance();
        SLinstance.SetSelected(this.gameObject, spriteRenderer);
    }
    
}

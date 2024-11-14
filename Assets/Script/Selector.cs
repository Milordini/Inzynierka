using UnityEngine;

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
        SLinstance = SelectMenager.GetInstance();
        SLinstance.SetSelected(this.gameObject, spriteRenderer);
    }
    
}

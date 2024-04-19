using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTrigger : MonoBehaviour
{
    [SerializeField] private string _prompt;
    [SerializeField] private Prompt _promptPanel;
    [SerializeField] private string _justPrompt;
    [SerializeField] private Vector3 _sizeBox;
    [SerializeField] private LayerMask _player;
    [SerializeField] private Color _color = new Color(0, 1, 0, 0.5f);
    private bool _Setprompt;
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(transform.position, _sizeBox);
    }
    void Update()
    {
        if(Physics2D.OverlapBox(transform.position, _sizeBox, 0, _player))
        {
            _promptPanel.SetPrompt(_prompt);
            _Setprompt = true;
        }
        else 
        {
            SetJustPrompt();
            _Setprompt = false;
        }
        
    }

    private void SetJustPrompt()
    {
        if(_Setprompt == true)
        {    
        _promptPanel.SetPrompt(_justPrompt);
        }
    }




    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.TryGetComponent(out Player player))
    //    {
    //        _promptPanel.SetPrompt(_prompt);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out Player player))
    //    {

    //        _promptPanel.SetPrompt(_justPrompt);

    //    }
    //}
}


//Доделать подсказки. Убираются после выхода из триггера или через какое-то время
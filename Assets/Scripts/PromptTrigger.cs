using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTrigger : MonoBehaviour
{
    [SerializeField] private string _prompt;
    [SerializeField] private Prompt _promptPanel;
    [SerializeField] private string _justPrompt;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _promptPanel.SetPrompt(_prompt);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {

            _promptPanel.SetPrompt(_justPrompt);

        }
    }
}


//Доделать подсказки. Убираются после выхода из триггера или через какое-то время
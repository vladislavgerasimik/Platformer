using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _enemies;
   public void SetPrompt(string prompt)
    {
        _enemies.text = prompt;
    }
}

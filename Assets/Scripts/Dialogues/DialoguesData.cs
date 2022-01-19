using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogues", fileName = "NewDialogueData", order = 0)]
public class DialoguesData : ScriptableObject
{
    [SerializeField] private string[] dialogue;
}

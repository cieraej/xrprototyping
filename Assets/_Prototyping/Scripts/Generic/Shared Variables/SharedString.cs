using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shared/String")]
public class SharedString : ScriptableObject {

    [SerializeField] private string sharedString;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShow : MonoBehaviour {

	public void OnPress(bool isPress)
    {
        if (!isPress)
        {
            StartmenuController.Instance.OnCharacterClick(transform.parent.gameObject);
        }
        
        
    }
}

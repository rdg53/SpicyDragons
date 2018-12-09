using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable {

    bool Identified { get; set; }

    void OnSelect();
    void OnHover();
    void OnDeselect();
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void takeInput(Controller.InputType input);
    void focus();
    void unfocus();
}

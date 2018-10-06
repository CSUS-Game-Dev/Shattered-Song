using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICursor
{
    void setActive(bool active);
    bool getActive();
    void takeAction(Controller.InputType input);
}

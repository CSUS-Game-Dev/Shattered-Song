using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICursor
{
    void processInput(InputType input);
    bool getActive();
    void setActive(bool active);
    IControllable getSubject();
    void setSubject(IControllable subject);
}

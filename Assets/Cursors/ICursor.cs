using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICursor
{
    void processInput(InputType input);
    bool getActive();
    void setActive(bool active);
    IControllable getSubject();
    //This last method is probably not necessary
    //void setSubject(IControllable subject);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Cursor
{

    void setActive(bool active);

    //void moveDirection(Direction input);

    void hover(MenuObject menuObject);

    void select(MenuObject menuObject);
}

//public enum Direction {UP, DOWN, LEFT, RIGHT}
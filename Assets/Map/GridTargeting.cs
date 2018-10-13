using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTargeting
{

    private BattleGrid grid;

    //CONSTRUCTOR
    public GridTargeting(BattleGrid grid)
    {
        this.grid = grid;
    }

    //Some targeting paradigms NEED a range or a direction.
    //If they are not passed the correct values, they will return empty
    public List<GridSpace> getSpaces
        (int targetX, int targetY, TargetType targetType,
        int range = 0, bool inclusive = true, GridDirection dir = GridDirection.NONE)
    {
        switch (targetType)
        {
            case TargetType.SPACE:
                List<GridSpace> sqr = new List<GridSpace>();
                sqr.Add(getSpace(targetX, targetY));
                return sqr;
            case TargetType.LINE:
                List<GridSpace> line = new List<GridSpace>();
                if (dir == GridDirection.NONE) { return line; }
                if (inclusive)
                {
                    return getSpacesInLine(targetX, targetY, range, dir);
                }
                else
                {
                    return getSpacesInLineExclusive(targetX, targetY, range, dir);
                }
            case TargetType.PLUS:
                if (inclusive)
                {
                    return getSpacesInPlus(targetX, targetY, range);
                }
                else
                {
                    return getSpacesInPlusExclusive(targetX, targetY, range);
                }
            case TargetType.CROSS:
                if (inclusive)
                {
                    return getSpacesInCross(targetX, targetY, range);
                }
                else
                {
                    return getSpacesInCrossExclusive(targetX, targetY, range);
                }
            case TargetType.CIRCLE:
                if (inclusive)
                {
                    return getSpacesWithinRange(targetX, targetY, range);
                }
                else
                {
                    return getSpacesWithinRangeExclusive(targetX, targetY, range);
                }
            case TargetType.SQUARE:
                if (inclusive)
                {
                    return getSpacesInSquare(targetX, targetY, range);
                }
                else
                {
                    return getSpacesInSquareExclusive(targetX, targetY, range);
                }
            default:
                return new List<GridSpace>();
        }
    }

    private GridSpace getSpace(int targetX, int targetY)
    {
        GridSpace result = null;

        if (grid.spaceExistsInGrid(targetX, targetY))
        {
            result = grid.grid[targetX, targetY];
        }

        return result;
    }

    //Gets all spaces in a line from a target point in a direction, including the target point
    // --x--
    // --x--
    // --x--
    // -----
    // -----
    private List<GridSpace> getSpacesInLine(int targetX, int targetY, int range, GridDirection d)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        if (range <= 0)
        {
            return spaces;
        }

        int currentX = targetX;
        int currentY = targetY;

        Vector2 dir = grid.gridDirToVec2(d);

        for (int i = 0; i <= range; i++)
        {
            if (!grid.spaceExistsInGrid(currentX, currentY))
            {
                break;
            }

            spaces.Add(grid.grid[currentX, currentY]);

            currentX += (int)dir.x;
            currentY += (int)dir.y;
        }

        return spaces;
    }

    // --x--
    // --x--
    // --o--
    // -----
    // -----
    private List<GridSpace> getSpacesInLineExclusive(int targetX, int targetY, int range, GridDirection d)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        if (range <= 0)
        {
            return spaces;
        }

        int currentX = targetX;
        int currentY = targetY;

        Vector2 dir = grid.gridDirToVec2(d);

        for (int i = 0; i <= range; i++)
        {
            if (!grid.spaceExistsInGrid(currentX, currentY))
            {
                break;
            }

            if (!(currentX == targetX && currentY == targetY))
            {
                spaces.Add(grid.grid[currentX, currentY]);
            }

            currentX += (int)dir.x;
            currentY += (int)dir.y;
        }

        return spaces;
    }

    //Gets all of the spaces in a box shape  around a point, including the point itself
    // -----
    // -xxx-
    // -xxx-
    // -xxx-
    // -----
    private List<GridSpace> getSpacesInSquare(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        int startPosX = targetX - range;
        int startPosY = targetY - range;

        for (int i = 0; i < range * 2 + 1; i++)
        {
            for (int j = 0; j < range * 2 + 1; j++)
            {
                if (grid.spaceExistsInGrid(startPosX + i, startPosY + j))
                {
                    spaces.Add(grid.grid[startPosX + i, startPosY + j]);
                }
            }
        }

        return spaces;
    }

    //Same as getSpacesInSquare but does not include the origin
    // -----
    // -xxx-
    // -xox-
    // -xxx-
    // -----
    private List<GridSpace> getSpacesInSquareExclusive(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        int startPosX = targetX - range;
        int startPosY = targetY - range;

        for (int i = 0; i < range * 2 + 1; i++)
        {
            for (int j = 0; j < range * 2 + 1; j++)
            {
                if (grid.spaceExistsInGrid(startPosX + i, startPosY + j) && !(startPosX + i == targetX && startPosY + j == targetY))
                {
                    spaces.Add(grid.grid[startPosX + i, startPosY + j]);
                }
            }
        }

        return spaces;
    }

    // --x--
    // -xxx-
    // xxxxx
    // -xxx-
    // --x--
    private List<GridSpace> getSpacesWithinRange(int targetX, int targetY, int range)
    {
        List<GridSpace> startList = new List<GridSpace>();
        List<GridSpace> finalList = new List<GridSpace>();

        //In case this is ever called with 0 as the range
        if (range == 0)
        {
            finalList.Add(getSpace(targetX, targetY));
            return finalList;
        }

        //Get the horizontal line first
        List<GridSpace> tempList;
        tempList = getSpacesInLine(targetX, targetY, range, GridDirection.LEFT);
        tempList.Reverse();
        tempList.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.RIGHT));

        //Add that line to the start list and the final list
        startList.AddRange(tempList);
        finalList.AddRange(tempList);

        for (int i = -range; i <= range; i++)
        {
            int currentRange = range - Mathf.Abs(i);
            finalList.AddRange(getSpacesInLineExclusive(targetX + i, targetY, currentRange, GridDirection.UP));
            finalList.AddRange(getSpacesInLineExclusive(targetX + i, targetY, currentRange, GridDirection.DOWN));
        }

        return finalList;
    }

    // --x--
    // -xxx-
    // xxoxx
    // -xxx-
    // --x--
    private List<GridSpace> getSpacesWithinRangeExclusive(int targetX, int targetY, int range)
    {
        List<GridSpace> startList = new List<GridSpace>();
        List<GridSpace> finalList = new List<GridSpace>();

        //In case this is ever called with 0 as the range
        if (range == 0)
        {
            finalList.Add(getSpace(targetX, targetY));
            return finalList;
        }

        GridSpace origin = getSpace(targetX, targetY);

        //Get the horizontal line first
        List<GridSpace> tempList;
        tempList = getSpacesInLine(targetX, targetY, range, GridDirection.LEFT);
        tempList.Reverse();
        tempList.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.RIGHT));

        //Add that line to the start list and the final list
        startList.AddRange(tempList);
        finalList.AddRange(tempList);

        for (int i = -range; i <= range; i++)
        {
            int currentRange = range - Mathf.Abs(i);
            finalList.AddRange(getSpacesInLineExclusive(targetX + i, targetY, currentRange, GridDirection.UP));
            finalList.AddRange(getSpacesInLineExclusive(targetX + i, targetY, currentRange, GridDirection.DOWN));
        }

        finalList.Remove(origin);
        return finalList;
    }

    // --x--
    // --x--
    // xxxxx
    // --x--
    // --x--
    private List<GridSpace> getSpacesInPlus(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        spaces.Add(getSpace(targetX, targetY));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UP));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWN));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.LEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.RIGHT));

        return spaces;
    }

    // --x--
    // --x--
    // xxoxx
    // --x--
    // --x--
    private List<GridSpace> getSpacesInPlusExclusive(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UP));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWN));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.LEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.RIGHT));

        return spaces;
    }

    // x---x
    // -x-x-
    // --x--
    // -x-x-
    // x---x
    private List<GridSpace> getSpacesInCross(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        spaces.Add(getSpace(targetX, targetY));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UPLEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWNLEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UPRIGHT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWNRIGHT));

        return spaces;
    }

    // x---x
    // -x-x-
    // --o--
    // -x-x-
    // x---x
    private List<GridSpace> getSpacesInCrossExclusive(int targetX, int targetY, int range)
    {
        List<GridSpace> spaces = new List<GridSpace>();

        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UPLEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWNLEFT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.UPRIGHT));
        spaces.AddRange(getSpacesInLineExclusive(targetX, targetY, range, GridDirection.DOWNRIGHT));

        return spaces;
    }

}

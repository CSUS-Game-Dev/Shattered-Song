using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathing {

	BattleGrid grid;

	public GridPathing(BattleGrid grid){
		this.grid = grid;
	}

	public List<MoveSpace> findMovePaths(int spaces, GridSpace startSpace){
		List<MoveSpace> result = new List<MoveSpace>();
		MoveSpace start = new MoveSpace(startSpace);

		depthFirstSearch(result, start, (int)startSpace.positionInGrid.x, (int)startSpace.positionInGrid.y, 0, spaces);

		return result;
	}

	public List<GridSpace> findMovePathsSimple(int spaces, GridSpace startSpace){
		List<GridSpace> result = grid.gridTargeting.getSpacesWithinRange((int)startSpace.positionInGrid.x, (int)startSpace.positionInGrid.y, spaces);
		List<GridSpace> toRemove = new List<GridSpace>();
		foreach(GridSpace gs in result){
			if(gs.isOccupied){toRemove.Add(gs);}
		}
		foreach(GridSpace gs in toRemove){
			result.Remove(gs);
		}
		return result;
	}

	// Recursively searches through all of the spaces within the grid until it does one of the following:
	// finds one that isn't in the grid - hits max depth - or finds a space that has already been added to the grid 
	private void depthFirstSearch(List<MoveSpace> currentList, MoveSpace lastSpace, int currentPosX, int currentPosY, int depth, int maxDepth){

		Debug.Log("Called at depth " + depth + " Current position is (" + currentPosX + ", " + currentPosY + ")");

		//If the space doesn't currently exist in the grid, just return the current list as it is
		//If the depth is greater than the max depth, return the current list as it is
		if(!grid.spaceExistsInGrid(currentPosX, currentPosY) || depth > maxDepth){
			Debug.Log("Failed - not in grid or past depth");
			return;
		}

		MoveSpace thisSpace = new MoveSpace(grid.grid[currentPosX, currentPosY], depth);

		//Inserts thisSpace into the list if there isn't already a better one
		// Returns whether or not the search should keep going on this branch
		bool keepGoing = insertIntoList(currentList, thisSpace);

		if(!keepGoing){ 
			Debug.Log("Failed - better option already in list");
			return; }

		//Now it calls itself for all of the adjacent children
		depthFirstSearch(currentList, thisSpace, currentPosX - 1, currentPosY, depth + 1, maxDepth);
		depthFirstSearch(currentList, thisSpace, currentPosX + 1, currentPosY, depth + 1, maxDepth);
		depthFirstSearch(currentList, thisSpace, currentPosX, currentPosY - 1, depth + 1, maxDepth);
		depthFirstSearch(currentList, thisSpace, currentPosX, currentPosY + 1, depth + 1, maxDepth);

		return;
	}

	private bool insertIntoList(List<MoveSpace> list, MoveSpace space){
		bool keepGoing = true;
		bool added = false;
		foreach(MoveSpace ms in list){
			if(ms.destination.positionInGrid == space.destination.positionInGrid){
				if(ms.dist > space.dist){
					list.Remove(ms);
					list.Add(space);
					added = true;
					break;
				}
				else{
					keepGoing = false;
					break;
				}
			}
		}

		if(keepGoing && !added){ list.Add(space);}

		return keepGoing;
	}

}

public class MoveSpace{
	//public List<GridSpace> path;
	public GridSpace destination;
	public int dist;
	
	public MoveSpace(GridSpace destination){
		//this.path = new List<GridSpace>();
		this.destination = destination;
		dist = 0;
	}

	public MoveSpace(GridSpace destination, int dist){
		this.destination = destination;
		this.dist = dist;
	}

	public MoveSpace(List<GridSpace> path, GridSpace destination, int dist){
		/*this.path = new List<GridSpace>();
		foreach(GridSpace gs in path){
			this.path.Add(gs);
		}*/
		this.destination = destination;
		this.dist = dist;
		/* 
		if(!(path.Contains(destination))){
			path.Add(destination);
		}*/
	}


}
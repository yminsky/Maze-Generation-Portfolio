# Maze-Generation-Portfolio

## Introduction
I wrote this code in 9th grade, and the project was tangential to the Game Development course I was taking at the time. All code is written in C# and for Unity. This repository contains a condensed version of the Unity Project, including all code related to maze generation and rendering. Since the code cannot be run in Unity in this format, pictures are included througout the explanations. (These pictures can also all be found in the [MazePhotos](https://github.com/sigalrmp/Maze-Generation-Portfolio/tree/main/MazePhotos) directory)

## Two Dimensional Mazes
I implimented 6 different algorithms for randomly generating **perfect mazes**.

### Aldous Broder

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/shapingCompatible/AldousBroder.cs)

The Aldous Broder algorithm utilizes random walks, adding each unvisited cell to the path until there are none left. This is incredibly inefficient, especially towards the end of the process when there are few unvisited cells left, but the mazes it produces are chosen completely randomly from all possible perfect mazes. (More information [here](https://weblog.jamisbuck.org/2011/1/17/maze-generation-aldous-broder-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/AldousBroderMaze.png width = 25%>

### Binary Tree

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/shapingCompatible/BinaryTreeMaze.cs)

The Binary Tree algorithm is very simple and efficient, but is strongly biased. The most glaring effect of this is that the mazes it produces will always have two edges that are completely empty. (More information [here](https://weblog.jamisbuck.org/2011/2/1/maze-generation-binary-tree-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/BinaryTreeMaze.png width = 25%>

### Hunt and Kill

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/shapingCompatible/HuntAndKill.cs)

The Hunt and Kill algorithm is similar to the Aldous Broder algorithm, but has an important difference that adds slight biases but make it significantly more efficient. Namely, when the algorithm reaches a dead end, it efficiently searches row by row for an unvisited cell, rather than continuing to randomly walk until it happens to find one. (More information [here](https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/HuntAndKillMaze.png width = 25%>

### Recursive Backtracker

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/shapingCompatible/3DCompatible/RecursiveBacktracker.cs)

The Recursive Backtracker algorithm uses random walks and backtracks recursively through the maze when it gets stuck. It is a fast and has a bias towards longer runs. (More information [here](https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/RecursiveBacktrackerMaze.png width = 25%>

### Sidewinder

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/nonShapingCompatible/Sidewinder.cs)

The Sidewinder algorithm is very similar to the Binary Tree algorithm, with two main directions that it chooses from as it works through the cells row by row. However, it keeps track of and revisits past cells in a way that decrease its efficiency only slightly, and it's biases slightly more. Unlike the Binary Tree algorithm, mazes produced using the Sidewinder algorithm have only one empty edge. (More information [here](https://weblog.jamisbuck.org/2011/2/3/maze-generation-sidewinder-algorithm.html#))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/SidewinderMaze.png width = 25%>

### Wilson's

[Implimentation](https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/Scripts/MazeGenAlgorithms/nonShapingCompatible/Wilsons.cs)

Wilson's algorithm is similar to the Aldous Broder algorithm, in that it uses random walks to inefficiently but effectively generate a completely random perfect maze. The main difference is that while Aldous Broder's algorithm starts at a random cell and walks randomly until all cells have been visited, Wilson's algorithm picks unvisited cells and walks randomly until a visited cell is reached. This process is repeated until the maze is finished. The result of this is that Wilson's algorithm takes longer at the beginning when few cells have been visited, rather than at the end.
(More information [here](https://weblog.jamisbuck.org/2011/1/20/maze-generation-wilson-s-algorithm))

## Three Dimensional Mazes
I also implimented three dimensional mazes, where paths utilize all three axis. Below is an image taken from inside of a three dimensional maze, generated using the Recursive Backtracker algorith.

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/3DMazes/RecursiveBacktracker3DMaze.png width = 50%>

## Shaped Mazes
The last major feature that I implimented is the ability to shape mazes. This is essentially done by removing cells from a grid, and then running algoritms in the constrained grid. This is compatable with my implimentation of all of the algorithms except for Sidewinder and Wilsons. Examples of shaped mazes are shown below:

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/Shaped2DMazes/AldousBroderShapedMaze.png width = 20%><img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/Shaped2DMazes/BinaryTreeShapedMaze.png width = 20%><img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/Shaped2DMazes/HuntAndKillShapedMaze.png width = 20%><img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/Shaped2DMazes/RecursiveBacktrackerShapedMaze.png width = 20%>

(From left to right: Aldous Broder, Binary Tree, Hunt and Kill, Recursive Backtracker)

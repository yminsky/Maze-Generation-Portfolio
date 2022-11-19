# Maze-Generation-Portfolio

## Introduction
I wrote this code in 9th grade, as a project tangential to the Game Development course I was taking at the time. All code is written in C# and for Unity. This repository contains a condensed version of the Unity Project, including all code related to maze generation and rendering. Since the code cannot be run in Unity in this format, pictures are included througout the explanations. (These pictures can also all be found in the [MazePhotos](https://github.com/sigalrmp/Maze-Generation-Portfolio/tree/main/MazePhotos) directory)

## Two Dimensional Mazes
I implimented 6 different algorithms for randomly generating **perfect mazes**.

### Aldous Broder
The Aldous Broder algorithm utilizes random walks, and while it is very inefficient, the mazes it produces are chosen completely randomly from all possible perfect mazes. (More information [here](https://weblog.jamisbuck.org/2011/1/17/maze-generation-aldous-broder-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/AldousBroderMaze.png width = 25%>

### Binary Tree
The Binary Tree algorithm is very simple and efficient, but is strongly biased. The most glaring effect of this is that the mazes it produces will always have two edges that are completely empty. (More information [here](https://weblog.jamisbuck.org/2011/2/1/maze-generation-binary-tree-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/BinaryTreeMaze.png width = 25%>

### Hunt and Kill
The Hunt and Kill algorithm is similar to the Aldous Broder algorithm, but has a few important differences that make it significantly more efficient, but also slightly biased. (More information [here](https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/HuntAndKillMaze.png width = 25%>

### Recursive Backtracker
The Recursive Backtracker algorithm uses random walks and backtracks recursively through the maze when it gets stuck. It is a fast and has a bias towards longer runs. (More information [here](https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm))

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/RecursiveBacktrackerMaze.png width = 25%>

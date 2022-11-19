# Maze-Generation-Portfolio

## Introduction
I wrote this code in 9th grade, as a project tangential to the Game Development course I was taking at the time. All code is written in C# and for Unity. This repository contains a condensed version of the Unity Project, including all code related to maze generation and rendering. Since the code cannot be run in Unity in this format, pictures are included througout the explanations. (These pictures can also all be found in the [MazePhotos](https://github.com/sigalrmp/Maze-Generation-Portfolio/tree/main/MazePhotos) directory)

## Two Dimensional Mazes
I implimented 6 different algorithms for randomly generating **perfect mazes**.

### [Aldous Broder](https://weblog.jamisbuck.org/2011/1/17/maze-generation-aldous-broder-algorithm)
The Aldous Broder algorithm is very inefficient, but the maze it produces is chosen completely randomly from all the possible perfect mazes.

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/AldousBroderMaze.png width = 25%>

### [Binary Tree](https://weblog.jamisbuck.org/2011/2/1/maze-generation-binary-tree-algorithm)
The Binary Tree algorithm is very simple and efficient, but is strongly biased. The most glaring effect of this is that the mazes it produces will always have two edges that are completely empty.

<img src = https://github.com/sigalrmp/Maze-Generation-Portfolio/blob/main/MazePhotos/2DMazes/BinaryTreeMaze.png width = 25%>

### [Hunt and Kill]

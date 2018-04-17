:warning: | __The content of this repository is explicitly NOT LICENSED for use, modification, or sharing. Per GitHub's terms of service it may only be viewed. See [No License] for more information.__ | :warning:
--------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------

[No License]: https://choosealicense.com/no-permission/

Introduction to C# - CIS297 Winter 2017
======
>As the final project for this course, our team was tasked with creating a version of the classic
>game Breakout for the Xbox 360. We were not able to use an existing game framework for this project
>so we were required to write our own collision system. The game features three types of blocks,
>each required one, two, or three strikes to destroy. There are four types of powerups that will
>randomly spawn from destroyed blocks: wide paddle, stack paddle, multi-ball, and explode. Wide
>paddle causes the paddle to double its width for a short time, stack paddle causes another paddle
>to spawn above the existing paddle for a short time, multi-ball spawns a number of new balls for
>the player to use, and explode causes existing balls to explode in a wide radius upon their next
>block strike.

### Details

__Students:__ Marc King, Rodney Lewis, Spencer Harrison, Holly Miller

__Professor:__ Eric Charnesky

__School:__ University of Michigan - Dearborn

__My Contributions:__ Design, implementation, logo, sprite artwork, collisons, movement, rendering

__Timeline:__ 2 weeks

### Technologies

* .NET Framework 4.6.2
* Universal Windows Platform
* Win2D

### Screenshots

[![MainMenu](Screenshots/MainMenu.png?raw=true "MainMenu")](Screenshots/MainMenu.png?raw=true)

[![Gameplay](Screenshots/Gameplay.gif?raw=true "Gameplay")](Screenshots/Gameplay.gif?raw=true)

[![GameOver](Screenshots/GameOver.png?raw=true "GameOver")](Screenshots/GameOver.png?raw=true)

*To test the efficiency of my collision and movement algorithms, a developer button was created that would
spawn balls for as long as it was held down. The algorithms were able to support more than 3,000 balls
on the screen before they were leaving the play area as fast as they could be spawned, and no framerate
drops or stuttering were observed.*

[![StressTest](Screenshots/StressTest.gif?raw=true "StressTest")](Screenshots/StressTest.gif?raw=true)


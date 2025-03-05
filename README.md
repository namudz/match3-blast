# Match3 Blast

![demo](/Demo/match-3-blast_showreel.gif)

Blast-type match 3 game prototype:
- Board of NxM tiles.
- Tap 2 or more adjacent cells to make a match.
- After a match, applied cascade: apply gravity to above pieces and refill board.

Enjoy and have fun! :)

## Editor
- In order to play in Editor, select "0_GameScene" as active scene.

## Config files
- Configuration files can be found under Assets > Prefabs > ScirptableObjects.

## Code info
- Gameplay logic is separated from view.
- Gameplay entry point is GameplayInstaller.Start().
- Logic side cascade is handled in GameFlowExecutor.
- View side of the cascade is handled in GameController.

## Packages
- [DOTween](https://dotween.demigiant.com/getstarted.php)
- [UniTasks](https://github.com/Cysharp/UniTask)

## Credits
- Credits:
	- Gameplay sprites by [ChessStudio](https://assetstore.unity.com/packages/2d/gui/icons/puzzle-blocks-icon-pack-278862)

## Unity Version
Unity 2022.3.19f1

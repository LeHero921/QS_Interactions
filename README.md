<h1 align="center">Interactions - v1</h1>

<h4 align="center">
QS Intersctions is still being developement.<br> And please make sure that the NEW INPUT SYSTEM is installed!
</h4>

---
<h2 align="center">
What is Interactions?
</h2>

QS Interactions is the framework we use to manage interactions in our games, such as doors, dialogues and more.

<br>
<h2 align="center">
How to Install
</h2>

There is currently no prebuild repo-package, so just download the zip (repo) or clone it, then import it into unity.

<br>
<h2 align="center">
How to Use it
</h2>

There are 3 main components:
- InteractionTarget
- InteractionTrigger
- InteractionModule

Plus we use a custom InputReader implementation that is used by this system... but I'm too lazy to explain it in detail, if it does not work for you, consider replacing the event calls with your own implementation. (can be found in "Input/InpuReaderSO.cs")

The Interaction Target script will be placed on all objects in the game that are desired to be interactable.

The Interaction Trigger gets added to the Player and triggers all interactions.

The Interaction Module is a script that other scripts should derive from, to create custom interactions.

<br>
<h2 align="center">
How to Use it - Developer
</h2>

Now here comes the *fun* part...

<h4> 1. Create new Modules</h4>
To create new Modules, you have to create a new Script inside Unity. <br>
This script then needs to derive from InteractionModule: <br>
For example:<br> public class newModule : InteractionModule <br>
You also need to implement the functions given by InteractionModule.

<h4> 2. Access Interactions in code</h4>
Interactions are trigegred through the OnInteract method of a InteractionTarget. You can either implement the Interact call in a custom script by referencing a InteractionTarget or by triggering it with the InteractionTrigger (uses raycasts).

<br><br>
<h4>More documentation coming soon™️</h4>
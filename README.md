# Unity3D-based-action-role-playing-game
The game is a role-playing game with the name "Cross Town". The game's main elements are the **backpack and trading system**, the **combat system**, **AI pathfinding system**, and **voice recognition system**. All items information are stored as JSON format, support real-time reading and modifying.
Before entering the game the player has to choose the character to use - there are two types of swordsman and mage with different combat mechanics and animation systems. Once the game starts, players can select the one they want to use from the panel of all available skills and drag and drop it to the skill shortcut bar at the bottom of the screen as a shortcut key. You can also open the backpack and shop and select items (including consumables, equipment and essentials) to buy and sell, with prices fluctuating depending on your location. Players have a certain amount of time to travel through the town, using strategy and positioning to dodge monsters or perform quick combos on them, exploring scenes and dropping random items in the process. If the player dies or does not reach the end of the game within the time limit, the player loses and the game is over. In addition to this, a 'spell chanting' encounter has been added to specific locations, allowing players to quickly teleport geographically by speaking into the microphone to give commands, adding to the game's fun.

# Architecture Design
# 1. MVC design pattern in Unity
The traditional classic MVC model separates the data, View and control scripts, but the coupling is still high.
The traditional classical MVC model separates the data, View and control scripts, but the dependency is still high, so we cannot directly follow the broader MVC model. It led to the development of the PureMVC framework, which is unique to Unity. In this framework, the various modules are implemented as an observer pattern, where notifications between classes are executed by reflecting methods. Furthermore, data can also be combined and passed as a type/structure, and the View's interface and the Model's data can be reused.
![图_page-0001](https://user-images.githubusercontent.com/62585203/131275069-57403726-6d53-4ed0-a421-42b226265d78.jpg)
![图_page-0003](https://user-images.githubusercontent.com/62585203/131275076-c68855af-8a93-440a-8264-48a1bce337d6.jpg)
![图_page-0004](https://user-images.githubusercontent.com/62585203/131275077-f11e92c5-03cb-42da-b6c3-640cced7d880.jpg)


# 2. Finite-State Machine
![图片8](https://user-images.githubusercontent.com/62585203/131274399-5e9c76c9-5371-4801-9938-e72e5da27525.png)

# 3. Pathfinding algorithms —— A* Search
![图片9](https://user-images.githubusercontent.com/62585203/131274433-9f785395-747a-4649-a53d-af6d61b7804f.png)



# User interface layer
1) Game start screen 
The camera moves smoothly from far to near, overlooking the whole scene model. Click on any area of the screen will appear a new game and exit the game button, select the latest game to enter the character selection interface. 
2) Character selection screen 
In this interface, you can view the animations of the different characters by clicking on the left and right arrow buttons, selecting the character you like, entering the name in the input box below, and clicking "Enter" to enter the main game scene. 
3) Main Game Interface 
The main game interface contains six different areas with different functions and displays: the skill shortcut bar, the backpack and trading area, the equipment and character panel, the skill introduction bar, the current blood and MP bar, and the system settings area. 
4) Backpack and Trading Window 
Players can automatically organise their backpacks and choose to work with different amounts of the same item—drag and drop items to move the backpack grid. Holding down the Ctrl key while dragging items in the grid with the mouse will move half the current items. When the mouse is placed over an item, the relevant information about it and the money information pops up, and the mouse becomes transparent for the player's experience. The right mouse button controls the buying and selling of items. 
Different items, such as food, medicine, tools, etc., have extra attribute bonuses for your character when used. Item information such as item categories, prices, attribute bonuses and descriptions are stored locally as JSON and can be read in the background after the game starts.
5) Equipment and Character Panel 
Drag and drop equipment items from your backpack into the equipment panel to put them on. Equipment with different attribute values will give different bonuses to the character's strength, power, attack and defence, and the attribute values will be displayed in the character panel. 
6) Skill Bar
Clicking on the skill button at the bottom right will open the skill window; scroll down to see the full list of skills. Then, drag and drop any skill icons to the skill shortcut bar below to release the skill in battle by clicking the corresponding shortcut key. 
7) Player Settings Window 
The player can adjust the background volume and brightness in this window.

# Demo
# All models are created by my own
![图片4](https://user-images.githubusercontent.com/62585203/131274184-81ca83b7-a4b2-41e2-a659-7214a17aeb65.png)
![图片5](https://user-images.githubusercontent.com/62585203/131274188-049baf2e-6241-4f3f-aabe-b90cd5d107e6.jpg)
![图片6](https://user-images.githubusercontent.com/62585203/131274195-b6ea4e5b-a7ef-49f2-b2cd-6d956581a6da.jpg)
![图片7](https://user-images.githubusercontent.com/62585203/131274199-da3c119a-85ef-499b-b5ab-1f9988a409ed.png)

# Backpack System
![图片1](https://user-images.githubusercontent.com/62585203/131274221-93bb12ae-125b-479c-a6c9-7a4907f3bab0.png)
![图片2](https://user-images.githubusercontent.com/62585203/131274223-0f493635-f837-4554-a058-bde393b09c94.png)
![图片3](https://user-images.githubusercontent.com/62585203/131274225-bca4e9f9-da18-4720-8887-ce0844b840ab.png)

# Combat System
![图片10](https://user-images.githubusercontent.com/62585203/131274648-9ee3ba02-a2ab-4e9c-b215-c5cca2d6dbc6.png)
![图片11](https://user-images.githubusercontent.com/62585203/131274652-ff3237cf-cb32-4975-a01d-e5bbe84b1899.png)
![图片12](https://user-images.githubusercontent.com/62585203/131274654-e77c331b-d115-4630-806a-e03efe666c1e.png)

# UI
![图片14](https://user-images.githubusercontent.com/62585203/131274706-6ab8f6d6-8976-4dab-9cc7-09e81d1176a2.png)

![图片15](https://user-images.githubusercontent.com/62585203/131274694-1c6b0a91-283e-40e5-8d60-18db7bb19ce7.png)
![图片13](https://user-images.githubusercontent.com/62585203/131274714-2a206ade-3de1-48b4-ac9d-200e87beee1d.png)

# Skills
The skill description is placed in a sliding text box with a scroll bar; it can be used quickly by dragging the icon to the shortcut bar below (just press the corresponding key).

![图片16](https://user-images.githubusercontent.com/62585203/131274782-48b61e77-0f1f-4d24-8724-916e4b09c9ff.png)
![图片17](https://user-images.githubusercontent.com/62585203/131274787-aeb883f7-2329-41c4-bea3-053bc3830e74.png)
![图片18](https://user-images.githubusercontent.com/62585203/131274789-cc0997ba-62ae-47f2-9a2a-b58fd7122c18.png)

# Voice Recognition
Using Tencent's dedicated gaming voice recognition platform - GVoice.
![图片19](https://user-images.githubusercontent.com/62585203/131274841-e85531b5-6e2f-47a5-86a6-3f6ac9e652ff.png)




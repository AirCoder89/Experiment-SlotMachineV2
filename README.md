# SlotMachineV2
Slot Machine Prototype v2

---
### Demo Video :
[![Demo Video](https://img.youtube.com/vi/o-fkzXQZ22M/0.jpg)](https://youtu.be/o-fkzXQZ22M)

---

### Walkthrough on sourceCode :

#### Settings Profile:
This prototype is based on setting profiles. A scriptable object holds all the game settings. This allows us to change settings, graphics, and behaviors easily, even in play mode.

To create new settings, right-click in the project, select Create, then Settings.

![SettingsSO](https://user-images.githubusercontent.com/62396712/83361190-241cc880-a387-11ea-932d-28b56f7f89ca.PNG)

#### Settings Resources:
The first tab in the settings profile is Resources. In this tab, we define our slot resources (type, background, and icon).

![Settings_Resources](https://user-images.githubusercontent.com/62396712/83361252-ac02d280-a387-11ea-99f7-8ab7e15a4ebd.PNG)

#### Spin Settings:
This tab is where we define the spinning behavior of each profile.

![Settings_Spin](https://user-images.githubusercontent.com/62396712/83361295-056b0180-a388-11ea-9d73-a8fb35da7155.PNG)

#### Paylines:
This is where we set paylines. We can also draw the payline on the screen and set an identity color for each one.

![Settings_Paylines](https://user-images.githubusercontent.com/62396712/83361463-6941fa00-a389-11ea-8138-dc941c58dec8.PNG)

#### Layout:
This tab allows us to control the slot machine layout by changing slots scale or adjusting the spacing on the vertical/horizontal axis.

![Settings_Layout](https://user-images.githubusercontent.com/62396712/83361499-cd64be00-a389-11ea-8976-0d6d0a46127f.PNG)

---
### How it works :

#### Spinning:
In this prototype, a different approach was used to make spinning smoother and more flexible. We translate the whole reel one step down and then return it to the start position after shifting the slot and generating a new one on the top, creating a full cycle.

![Spin](https://user-images.githubusercontent.com/62396712/83361691-3c8ee200-a38b-11ea-9956-45f33cca25f7.png)

#### Clamping:
After the spin is completed, we need to clamp the reel to the appropriate position. We compare the reel position with the top (start pos) and bottom (pos of the next step) and move to the shortest distance between them.

![Clamp](https://user-images.githubusercontent.com/62396712/83361806-68f72e00-a38c-11ea-8ee6-4b2b7d610838.png)

---
### Screenshots :
![Game](https://user-images.githubusercontent.com/62396712/83361041-f6834f80-a385-11ea-89e0-5b35cdbb8bdd.PNG)
![Coins Animation](https://user-images.githubusercontent.com/62396712/83361047-fdaa5d80-a385-11ea-90a1-168a224f5889.png)
![Paylines](https://user-images.githubusercontent.com/62396712/83361049-000cb780-a386-11ea-9780-0d153cc696d6.PNG)
![Draw Payline](https://user-images.githubusercontent.com/62396712/83361051-01d67b00-a386-11ea-8ca0-7cfacb060b12.png)

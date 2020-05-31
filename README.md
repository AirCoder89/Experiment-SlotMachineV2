# SlotMachineV2
Slot Machine Prototype v2

---
### Demo Video :

[![CLICK HERE](https://img.youtube.com/vi/o-fkzXQZ22M/0.jpg)](https://youtu.be/o-fkzXQZ22M)

---

### Walkthrough on sourceCode :

**- Settings Profile:**
The prototype based on settings profiles with a **scriptable object** hold all the settings of the game.
the benefits from that it's allow us to change settings, graphics and behaviors in one shot even in the play mode.

to create a new settings just **Right Click in project -> Create -> Settings**

![settingsSO](https://user-images.githubusercontent.com/62396712/83361190-241cc880-a387-11ea-932d-28b56f7f89ca.PNG)

**- Settings Resources:**
the first tab in the settings profile is **Resources**. in this tab we need to fill our slot resources (type / background and icon).

![settings_Resources](https://user-images.githubusercontent.com/62396712/83361252-ac02d280-a387-11ea-99f7-8ab7e15a4ebd.PNG)


**- Spin Settings:**
All the magic happen in this tab, from it we can create a lot of spinning variation for each profile.

![settings_spin](https://user-images.githubusercontent.com/62396712/83361295-056b0180-a388-11ea-9d73-a8fb35da7155.PNG)

**- Paylines:**
It's a cool method to set **paylines** into the machine. we can also draw the **payline** on the screen set an identity color for each one.

![settings_paylines](https://user-images.githubusercontent.com/62396712/83361463-6941fa00-a389-11ea-8138-dc941c58dec8.PNG)

**- Layout:**
This tab allow us to control the slot machine layout by changing slots scale or adjust the spacing on the vertical/horizontal axis.

![settings_layout](https://user-images.githubusercontent.com/62396712/83361499-cd64be00-a389-11ea-8976-0d6d0a46127f.PNG)
---
### How it works :

**- Spinning:**

In this prototype i used a different approach to make the spinning smooth and more flexibility to work with it.
the picture below appears that we translate the whole reel one step down. and then return it the start position after shifting the slot and generate new one on the top. to create a full cycle.

![spin](https://user-images.githubusercontent.com/62396712/83361691-3c8ee200-a38b-11ea-9956-45f33cca25f7.png)

**- Clamping:**

After the spin complete we need to clamp the reel to appropriate position. so we just compare the reel position with the top (start pos) and bottom (pos of the next step) and move to the shortest distance between them.

![clamp](https://user-images.githubusercontent.com/62396712/83361806-68f72e00-a38c-11ea-8ee6-4b2b7d610838.png)
---
### Screenshots :

![game](https://user-images.githubusercontent.com/62396712/83361041-f6834f80-a385-11ea-89e0-5b35cdbb8bdd.PNG)
![coinsAnim](https://user-images.githubusercontent.com/62396712/83361047-fdaa5d80-a385-11ea-90a1-168a224f5889.png)
![paylines](https://user-images.githubusercontent.com/62396712/83361049-000cb780-a386-11ea-9780-0d153cc696d6.PNG)
![drawPline](https://user-images.githubusercontent.com/62396712/83361051-01d67b00-a386-11ea-8ca0-7cfacb060b12.png)



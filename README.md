# Slot Machine Prototype 2


<table>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/628d98c6-0224-48a2-b3e3-321b5f48e681" alt="InspectMe Logo" width="100"></td>
    <td>
      🛠️ Boost your Unity workflows with <a href="https://divinitycodes.de/">InspectMe</a>! Our tool simplifies debugging with an intuitive tree view. Check it out! 👉 
      <a href="https://assetstore.unity.com/packages/tools/utilities/inspectme-lite-advanced-debugging-code-clarity-283366">InspectMe Lite</a> - 
      <a href="https://assetstore.unity.com/packages/tools/utilities/inspectme-pro-advanced-debugging-code-clarity-256329">InspectMe Pro</a>
    </td>
  </tr>
</table>

---

Welcome to the second version of the Slot Machine Prototype. This updated version introduces a more flexible and user-friendly approach to customizing slot machine settings and behaviors using scriptable objects.

## Demo Video

Watch the slot machine in action by clicking the image below:

[![Watch the Demo Video](https://img.youtube.com/vi/o-fkzXQZ22M/0.jpg)](https://youtu.be/o-fkzXQZ22M)

## Features Overview

### Dynamic Settings via Scriptable Objects

Easily configure and modify the game's settings, graphics, and behaviors on-the-fly. New settings can be created with just a few clicks, allowing for rapid testing and iteration.

#### How to Create New Settings
Right-click in the project window, navigate to Create, and select Settings to add a new settings profile.

![Settings Profile](https://user-images.githubusercontent.com/62396712/83361190-241cc880-a387-11ea-932d-28b56f7f89ca.PNG)

### Detailed Settings Tabs

#### Resources
Define slot resources like type, background, and icons in the Resources tab.

![Settings Resources](https://user-images.githubusercontent.com/62396712/83361252-ac02d280-a387-11ea-99f7-8ab7e15a4ebd.PNG)

#### Spin Settings
Adjust the spinning behavior of the reels to achieve the desired animation and mechanics.

![Spin Settings](https://user-images.githubusercontent.com/62396712/83361295-056b0180-a388-11ea-9d73-a8fb35da7155.PNG)

#### Paylines
Set up and customize paylines, draw them on the screen, and assign unique identity colors.

![Paylines Settings](https://user-images.githubusercontent.com/62396712/83361463-6941fa00-a389-11ea-8138-dc941c58dec8.PNG)

#### Layout
Control the slot machine layout by modifying slot scale and adjusting spacing along the vertical and horizontal axes.

![Layout Settings](https://user-images.githubusercontent.com/62396712/83361499-cd64be00-a389-11ea-8976-0d6d0a46127f.PNG)

## How It Works

### Spinning Mechanics

A novel approach was used in this prototype to make the spinning of reels smoother and more flexible. We translate the entire reel one step down and reset it to the starting position after refreshing the slots and generating new ones at the top, completing a full cycle.

![Spinning Mechanics](https://user-images.githubusercontent.com/62396712/83361691-3c8ee200-a38b-11ea-9956-45f33cca25f7.png)

### Clamping

After the spin completes, the reel is clamped to the nearest position, ensuring alignment and readiness for the next spin.

![Clamping Mechanics](https://user-images.githubusercontent.com/62396712/83361806-68f72e00-a38c-11ea-8ee6-4b2b7d610838.png)

## Screenshots

Take a look at some in-game screenshots illustrating the user interface and various features:

![Game Interface](https://user-images.githubusercontent.com/62396712/83361041-f6834f80-a385-11ea-89e0-5b35cdbb8bdd.PNG)
![Coins Animation](https://user-images.githubusercontent.com/62396712/83361047-fdaa5d80-a385-11ea-90a1-168a224f5889.png)
![Payline Visualization](https://user-images.githubusercontent.com/62396712/83361049-000cb780-a386-11ea-9780-0d153cc696d6.PNG)
![Draw Payline](https://user-images.githubusercontent.com/62396712/83361051-01d67b00-a386-11ea-8ca0-7cfacb060b12.png)

## Conclusion

SlotMachineV2 brings advanced customization capabilities to the realm of digital slot machines, providing a robust framework for creating and testing various slot machine configurations and visual styles. Dive into the settings, experiment with the options, and tailor the slot machine experience to fit your needs!

---

<table>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/628d98c6-0224-48a2-b3e3-321b5f48e681" alt="InspectMe Logo" width="100"></td>
    <td>
      🛠️ Boost your Unity workflows with <a href="https://divinitycodes.de/">InspectMe</a>! Our tool simplifies debugging with an intuitive tree view. Check it out! 👉 
      <a href="https://assetstore.unity.com/packages/tools/utilities/inspectme-lite-advanced-debugging-code-clarity-283366">InspectMe Lite</a> - 
      <a href="https://assetstore.unity.com/packages/tools/utilities/inspectme-pro-advanced-debugging-code-clarity-256329">InspectMe Pro</a>
    </td>
  </tr>
</table>

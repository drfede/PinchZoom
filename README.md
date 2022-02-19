# Pinch Zoom

# Dev Log

## First Analysis

I tried the Empire and Puzzle camera system for a couple of minutes.

**Main Idea:** The camera is a smooth camera that zooms in/out and moves to the borders of the map. The icons on the buildings stay in the correct spot while zooming in/out.

The project I’ll create will have an overlay ui for the buttons and an isometric map.

---

## First implementation

First created a small tilemap using external asset, I thought it was easier to control the overall quality of the camera implementation with a real map.

First thing I created is a simple panning system. After creating the panning system I sligthly changed the code to use a scriptable object for the settings.

---

## Pinch Zoom

For the pinch zoom, but also for the pan I used the *touch.deltaposition* to get the movement of the fingers on the screen. To understand if the player wants to zoom in or out I save the distance between the fingers during the last frame and that’s it.

---

## The Rubber Band Zoom

I tried again the game Empires & Puzzle and I noticed that the pinch zoom has a small rubberband effect that makes the player zoom in/out and if the zoom exceeds the limits it gets adjusted with a rubber band effect.

I implemented this effect by extending by a bit the limits of the pinch zoom that lerp back to the normal limits when the user stops adjusting the zoom.

---

## Sprites scaling with camera zoom

After the rubber band, I noticed that the UI in world space won’t scale with the camera zooming in/out, because of that I implemented a simple callback system that will fire a UnityEvent with the new camera orthographic size and all the sprites will rescale correctly.

---

## External Assets

Text Mesh Pro

UI:

[2D Casual UI HD | 2D Icons | Unity Asset Store](https://assetstore.unity.com/packages/2d/gui/icons/2d-casual-ui-hd-82080)

2D Art:

[Cute Isometric Town Starter Pack | 2D Environments | Unity Asset Store](https://assetstore.unity.com/packages/2d/environments/cute-isometric-town-starter-pack-134286)

---

## Final result

Realization time: ~4hours (map and all systems)

[https://drive.google.com/file/d/14bX6e8uxCF8-4YZpvdTPzAOy4xzsjV_i/view?usp=sharing](https://drive.google.com/file/d/14bX6e8uxCF8-4YZpvdTPzAOy4xzsjV_i/view?usp=sharing)
# AR Business Card with Vuforia

## Project Overview
The **AR Business Card** is an interactive augmented reality experience that brings your traditional business card into the digital realm. By scanning a custom image target, users can view animated buttons and text that display personal information such as name, title, and contact links. The AR interface features functional buttons that open relevant websites, social media profiles, or email clients, providing an engaging and dynamic way to interact with professional contact details. This project integrates Unity and Vuforia for real-time image recognition, with animations triggered upon target detection.

## Features
- **Augmented Reality**: The project uses Vuforia’s image recognition capabilities to detect a custom image target and display a corresponding AR interface.
- **Interactive UI**: The AR interface includes buttons that link to email, LinkedIn, GitHub, and Medium.
- **Smooth Animations**: The buttons and text animate smoothly into view once the image target is detected, enhancing the user experience.
- **Multi-platform Support**: The project is built for both Android and iOS platforms.

## Demo
You can download the demo builds of the project for Android and iOS from the following link:

[Download the Builds](https://drive.google.com/drive/folders/1Te3NRCXALZ43I3Jos1XEUXrkwMVqBVJS?usp=drive_link)

## Installation Instructions

### Android
1. Download the `ARBusinessCard.apk` from the link above.
2. Transfer the APK file to your Android device.
3. Open the APK file on your device to install the app.
4. Once installed, launch the app and point your camera at the provided image target (the image target can be viewed on another device or printed out).

### iOS
1. Download the `ARBusinessCard-iOS.zip` from the link above.
2. Unzip the file and open the project in Xcode.
3. Build and run the app on a physical iOS device (note that building for iOS requires a valid Apple developer account).
4. Launch the app and scan the image target.

## Technologies Used
- **Unity**: Game engine for creating 3D and AR experiences.
- **Vuforia SDK**: For AR image recognition and tracking.
- **C#**: Used for scripting interactions and animations within Unity.
- **Animator**: Used to handle smooth transitions and UI animations when the image target is detected.

## Usage
- When the image target is detected, the AR interface appears, displaying buttons for LinkedIn, GitHub, email, and Medium.
- Clicking on any button will open the respective webpage or email client.
- The animations play when the AR content is detected and are reset when the target is no longer in view.

## Image Target
You can use the following image target to experience the AR business card. Either print it out or display it on another device for scanning.

![Image Target Placeholder]

## Future Improvements
- Adding additional interactive features such as 3D models or more dynamic animations.
- Further customization of the AR experience based on user interactions.

## Author
- **Clay Jones** - UX Engineer || Software Developer

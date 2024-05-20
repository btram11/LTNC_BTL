# Fleet Management System

## Description

This WPF application is designed to manage vehicle fleets by automating task assignments and using Firestore and Google Maps API for data storage and calculating distance. This document provides instructions on how to set up and run the project.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Running the Application](#running-the-application)
7. [Contact Information](#contact-information)

## Prerequisites

Ensure you have the following installed on your machine:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other compatible IDE

## Installation

Follow these steps to get the project up and running:

1. **Clone the repository**:
    ```sh
    git clone https://github.com/btram11/LTNC_BTL.git
    ```
2. **Navigate to the project directory**:
    ```sh
    cd LTNC_BTL
    ```
3. **Open the solution file (.sln) in Visual Studio**.

## Configuration

Before running the application, you need to configure the API keys:

1. **Open the `appsettings.json` file located in the `MainViewCore` project**.
2. **Replace the placeholder API keys with your actual API keys**:
    ```json
    {
        "Firebase": "YOUR_FILE_PATH TO /service_account.json",
        "GoogleMapsAPI": "YOUR_GOOGLE_MAPS_API_KEY"
    }
    ```

## Running the Application

To run the application:

1. **Set `MainView` as the startup project** in Visual Studio:
    - Right-click on the `MainView` project in the Solution Explorer.
    - Select `Set as Startup Project`.
2. **Build the solution**:
    - Click on `Build` in the top menu and select `Build Solution`.
3. **Run the application**:
    - Press `F5` or click on the `Start` button in Visual Studio.


## Contact Information

For questions or feedback, please contact:

- **Email**: tram.nguyenbao2004@hcmut.edu.vn
- **GitHub**: [btram11](https://github.com/btram11)

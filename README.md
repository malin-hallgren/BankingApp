# BankingApp
A group project in "Programmering med C# och .NET" at Chas Academy 2025.
The project is focused on learning how to work in teams in an effective and modern way.

## Overview
BankingApp is a console-based application build in C#.

## Features
- Multiple User types (Inherits from the BasicUser class)
- Account management: Create, manage, apply for loans.
- Transfer money between accounts
- Data storage in JSON files (users, accounts, transfer logs)
- A simple UI menu for navigation

## Architecture

BankingApp
    - Accounts
        - Account.cs -base model for bank accounts
        
BankingApp
    |-- Accounts
    |   |-- Account.cs - Base model for bank accounts
    |   |-- Loan.cs - Represents a loan
    |   |-- SavingsAccount.cs - Account specialization with interest handling
    |   |-- Transfer.cs - Encapsulates a money transfer between accounts
    |
    |-- UI
    |   |-- AdminUI.cs - Admin menu flow
    |   |-- CustomerUI.cs - Customer menu flow
    |   |-- Login.cs - Authentication flow
    |   |-- CustomerAccount.cs - Account Type selection flow
    |
    |-- Users
    |   |-- BasicUser.cs - Base user entity
    |   |-- Admin.cs - Admin user
    |   |-- User.cs - Customer user
    |
    |-- Utilities
        |-- Enums
        |   |-- AccountType.cs - Supported account types
        |
        |-- AsciiHelper.cs - Renders Ascii art/logo
        |-- BankApp.cs - Handles global variables and lists for appwide data
        |-- ConvertCurrencies.cs - Handles currencies and conversion rates
        |-- InputHelpers.cs - Handles inputs (String, Decimal, Password)
        |-- JsonHelper.cs - Handles settings for saving and loading files
        |-- Logo.txt - Ascii art bank logo
        |-- Menu.cs - Renders and handles menu logic 
        |-- OutputHelpers.cs - Handles output and formatting 

## Setup & Run

### Needs and Dependencies
- .NET SDK (version 8)
- A code editor or IDE (Visual studio  recommended)
- MailKit (4.14.1)
- Microsoft.AspNetCore.Identity (2.3.1)
- System.Text.Json (9.0.10)

### Setup & Run
1. Clone the repository (https://github.com/malin-hallgren/BankingApp)
2. Open BankingApp.sln in your IDE
3. Run Program.cs
4. If it is the first time starting BankingApp, set an admin password and log in with it.
5. Follow the menus to try features in the app. (JSON files will be created/updated in application directory)

## Authors
- Malin Hallgren
- Andrés Llano Duran
- Geovanni Medina Herrera
- Anton Grönqvist


## usage


# GreenSlate-Coding-Challenge
Greenslate Coding Challenge - Check readme for architecture decisions


# Architecture

# This task was accomplished using ASP.NET Core (MVC template).

## models
      -Drinks
        Base class that all drinks can be derived from.
      -Coins
        Base class that all coins can be derived from.
       -Vending Machine
        Base class that all other types of machines can be derived from.
## Data
    -Implemented an In memory Repository pattern with Interfaces as a contract
    -Repositories are implemented as a Singleton for data persistency (Dependencies are specified in Startup.cs)
    
## Business Logic
    -Business logic is implemented in a class that handles all the required operations to meet the business requirements. 

## Fron end Code
    -Simple JS code to show client-side interactivity, while most heavy logic is on the server-side. 
  

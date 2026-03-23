# EquipmentRentalSystem

## Description
The project is a simple console application which allows creating users and equipment. It supports renting equipment, returning it, checking availability, displaying active and overdue rentals, generating reports and additionally saving/loading data from JSON.

## Design decision
The project uses the following structure:

`CLI -> services -> repository -> data.`

I chose this structure to separate responsibilities and follow basic SOLID principles. The CLI is responsible only for user interaction. Services contain business logic such as rental rules, limits, and penalties. The repository is responsible for storing and retrieving data.

The domain model is separated from the rest of the application and includes classes such as Equipment with Laptop, Camera, and Projector, User with Student and Employee, and Rent. Inheritance is used only where it makes sense in the domain. Laptop, Camera, and Projector inherit from Equipment, while Student and Employee inherit from User. This allows shared properties to stay in one place while each type still has its own specific fields.

## Cohesion, coupling, and class responsibilities

Cohesion is improved by keeping each part focused on one role: models represent the domain, repositories store data, services handle business rules, and MenuHandler manages console interaction. Coupling is reduced because the menu does not work directly with stored data, but uses services, and services use repositories instead of putting all logic in one class. Responsibilities are divided so that Program.cs only starts the application, services handle rules such as rental limits and penalties, and storage logic is separated from the console layer.

I chose this organization because it is simple enough for a basic console project, but still shows a clear separation of concerns and makes the code easier to read, maintain, and extend.

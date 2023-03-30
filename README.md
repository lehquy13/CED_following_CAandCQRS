# CED-ASP_NET_Core

DDD Layers & Clean Architecture
There are four fundamental layers of a Domain Driven Based Solution;

![image](https://user-images.githubusercontent.com/91002362/225193607-4002ccaf-8d84-454d-bbe6-67d4e1ff57cf.png)

Business Logic places into two layers, the Domain layer and
the Application Layer, while they contain different kinds of
business logic;

● Domain Layer implements the core, use-case independent business logic of the domain/system

● Application Layer implements the use cases of the application based on the domain. A use case can be thought as a user interaction on the User Interface (UI).

● Presentation Layer contains the UI elements (pages, components) of the application.

● Infrastructure Layer supports other layer by implementing the abstractions and integrations to 3rd-party library and systems.

The same layering can be shown as the diagram below and known as the Clean Architecture, or sometimes the Onion Architecture:
![image](https://user-images.githubusercontent.com/91002362/225193758-1be5bd9f-b955-4b81-bc53-93e77018c23b.png)

The diagram below shows the essential dependencies (project references) between the projects in the solution

![image](https://user-images.githubusercontent.com/91002362/225205924-8fd2341f-2dc1-4b71-9509-35c46b9ed6ea.png)

What i'm aiming for:
● Independent of Frameworks: The architecture does not depend on the existence of some library of feature laden software.

● Testable: The business rules can be tested without the UI, Database, Web Server, or any other external element

● Independent of Database: we can swap out Oracle or SQL Server, for Mongo, BigTable, CouchDB... or just changing ORM

● Independent of any external agency: The business rules don't know anything about interfaces to the outside world.

# References:
- Implementing Domain Driven Design - Halil İbrahim Kalkan
- ASP.NET 6 REST API Following CLEAN ARCHITECTURE & DDD - Amichai Mantinband

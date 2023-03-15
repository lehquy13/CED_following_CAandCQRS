# CED-ASP_NET_Core

DDD Layers & Clean Architecture
There are four fundamental layers of a Domain Driven Based Solution;

![image](https://user-images.githubusercontent.com/91002362/225193607-4002ccaf-8d84-454d-bbe6-67d4e1ff57cf.png)

Business Logic places into two layers, the Domain layer and
the Application Layer, while they contain different kinds of
business logic;

● Domain Layer implements the core, use-case
independent business logic of the domain/system

● Application Layer implements the use cases of the
application based on the domain. A use case can be
thought as a user interaction on the User Interface (UI).

● Presentation Layer contains the UI elements (pages,
components) of the application.

● Infrastructure Layer supports other layer by
implementing the abstractions and integrations to
3rd-party library and systems.

The same layering can be shown as the diagram below and
known as the Clean Architecture, or sometimes the Onion
Architecture:
![image](https://user-images.githubusercontent.com/91002362/225193758-1be5bd9f-b955-4b81-bc53-93e77018c23b.png)

# References:
- Implementing Domain Driven Design - Halil İbrahim Kalkan
- ASP.NET 6 REST API Following CLEAN ARCHITECTURE & DDD - Amichai Mantinband

# Zoolandia Razor

For this exercise you will create a trival web application that will Read entries stored within a database.

The goal of this project is to help students learn how to develop a web application using a tested Repository Pattern while implmenting Views in Razor. Students are tasked with completing a [Zoolandia ERD](./DBS_ZOOLANDIA_ERD.md) as it is to be used for their models.

This solution contains:
1. ASP.NET Web Application Project
2. Unit Test Project


## Rules

- Complete [Zoolandia ERD](./DBS_ZOOLANDIA_ERD.md). Save the resultant diagram file in root directory of this assignment's repository.
- Using the ERD you created in [draw.io](https://www.draw.io/),
- Create a model and matching migration for the `ZoolandiaRazor` project.
- Implement a fully unit tested Repository.
- Implement Fully implement the Razor Views for the supplied `Animal`, `Habitat` and `Employee` controllers using the Specifications below.
should only be responsible for receiving user input and printing output.


## Speicifications

You'll need to write produce the following HTML responses about Zoolandia.

1. `/Animal` - The View should display a simple HTML list of all animals in your database. You should display the following information about each animal.
    1. Animal name (this information will be an anchor to take the user to the specific animal view)
    1. Current habitat in the zoo
1. `/Animal/Details/1` - When the `id` of an animal is in the URL, display information about that specific animal.
    1. Name
    1. Species common name (if exists)
    1. Species scientific name
    1. Current habitat
    1. Age of animal
1. `/Habitat` - This will list all habitats open for public visitation.
    1. Habitat name
    1. How many animals currently in the habitat
1. `/Habitat/Details/1` - Display information about this specific habitat.
    1. Habitat name
    1. Habitat type
    1. List the names of the animals currently in the habitat
    1. List the employees currently assigned to maintenance of the habitat
1. `/Employee/` - List all employees
1. `/Employee/Details/1` - Show employees name, age, and habitats currently assigned to

---
_layout: landing
---


# Welcome to **Calories Counter** documentation

This application should help you in your fitness journey, making counting calories simplier. 
You can menage your everyday calories intake, add new products to your database and even use **AI** features to get to know how many calories you ate.

# How to run? 

There is two ways to run this application fist is using docker, second is running manually. 
* **Using docker** 
    If you want to run **Calories Counter** using docker you just have to run ```docker-compose up``` in main directory. It will create three containers 
    + db - for database (Postgre running on port 5432)
    + backend - for ASP.NET API service running on port 5287
    + frontend - for React.js frontend application running on port 5173

    You should make sure that every of this port is avaible.

* **Maually** 
    1. You have to create Postgre database running on port 5432 with this type of configuration:
    - POSTGRES_USER=admin
    - POSTGRES_PASSWORD=admin
    2. Now you can run backend service using command ```dotnet run``` in ```CaloriesCounterAPI```  directory. You should have ```.NET SDK 8.0``` installed. You can make sure if service is running correctly by accesing localhost:5287/swagger/index.html. 
    3. Lastly to acces frontend you should run command ```npm install``` and ```npm run dev``` in ```Fronted``` directory. You should have ```node:lts``` installed. You can acces frontend on port 5173

# Technical documentation
Most important definitions are contained in two subsites:
* [Controlles documentation](api/CaloriesCounterAPI.Controllers.html)
* [Models documentation](api/CaloriesCounterAPI.Models.html)

After reading those you should get basic knowlage on how backend service works.
<!-- # This is the **HOMEPAGE**.

Refer to [Markdown](http://daringfireball.net/projects/markdown/) for how to write markdown files.

## Quick Start Notes:

1. Add images to the *images* folder if the file is referencing an image. -->
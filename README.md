# DEMO API 

## Instructions

This Project is a DEMO Web API Project. This Project Create a CRUD (Create-Read-Update-Delete) API Controller from a Customer Model. 

When the Application Run for the First Time, it will be created a Dummy MySQL Database with the name Customers.

This Application is a .NET Web API targeted at .NET Framework 4.5.2 
The API use 3rd party libraries like Swagger and log4net.
Unit tests created with NUnit Framework.

This project developed in Visual Studio 2017 and MySQL. 

## Requirements

MySQL.Data and MySql.Data.Entity.EF6 libraries are face issues at the latest 6.10.6 version, So you need to download the previous version which is the 6.9.11.

When we haven't installed mysql-connector-net-6.9.11.msi and the mysql-for-visualstudio-1.2.7.msi then the Entity Framework is not working properly and especially the Entity Designer Tool can't open and also the Update and Delete Methods did not pass the parameter ID.

It is recommended to install these tools to use MySQL.

links:
Go at the following links and click at Looking for previous GA versions? We need the 6.9.11 version

https://dev.mysql.com/downloads/connector/net/

https://dev.mysql.com/downloads/windows/visualstudio/

## Configuration

It is mandatory to use your relevant settings at the Web.Config file. Change Only the server,uid and password variables.

'''
  <connectionStrings>
    <add name="EncodeContext" providerName="MySql.Data.MySqlClient" connectionString="server=localhost;port=3306;database=customers;uid=tomchavakis;password=******" />
  </connectionStrings>
'''

## Logging

The Location for the Logs exist at the App_Data\Logs folder.
The Logging mechanism based on the Web.Config file. Every Day it will be created new Logging Files.
Moreover Logs are integrated with the Entity Framework. You can observe the timings and the SQL Queries that occured. This mechanism is very important in case that
you suspect that there is something wrong with the MySQL Queries. 


## Unit Tests
 
 You can use the NUnit Test Explorer or the MS Test Explorer in order to run all or partially the Unit Tests.

 ## Client Tests

If you use POSTMAN then at the App_Data folder you will find the Encode.postman_collection.json 
Try to import this collection at your POSTMAN client for easy client testing.  

After you import the collection then select and run one by one the functions and observe the results of them.

You will find GET, POST, PUT and DELETE API Calls.
At POST and PUT calls don't forget to check the body of each function in case that you like to change the variables.    
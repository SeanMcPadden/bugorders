# bugorders
This is a web application that customers can visit to see if the store 
has any live food before making the trip. 

This application takes advantage of ASP.NET Core Identity for authentication and authorization.

On the update page, the image is sent to Azure Blob Storage 
and the image details are sent to an Azure SQL Database.

On the newtownabbey page, the latest image is loaded from Azure Blob Storage 
and the details are shown from the Azure SQL Database.

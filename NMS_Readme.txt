Please see below change before you run application:

Open solutions in Visual Studio and change your SQL Data source connection string for SQL SERVER in appsettings.json

Only one Change in appsettings.json file
============================================================================
"DefaultConnection": "Server=LAPTOP-5E637HRI\\SQLEXPRESS;Database=NotesDB;Trusted_Connection=True;MultipleActiveResultSets=true"
============================================================================

Once you change build & run web application. It will create database automatically with 3 categories on your SQL Server.

For your information
==========================
Created "DbInitializer,IDbInitializer" for creating database on first time.So you don't need to create any database your side manually. But I am attaching Database .bak file with this root folder.

Created custom audit log "NMS.Logger" to a text file each time an endpoint is accessed along with a date/time stamp of
the access. It will write log file in log folder to your web application root folder.




Coading structure

Created simple repository pattern for this project with EF. I was thinking to create some UnitOfWork. But we don't need for this small project. Even we don't need repository pattern. 

File Structure

───NMS
    ├───Controllers
    │   └───CategoriesController.cs
    │   └───NotesController.cs
    ├───Data
    │   └───ApplicationDbContext.cs
    ├───Initializer
    │   └───DbInitializer.cs
    │   └───IDbInitializer.cs
    ├───Logger
    │   └───NMSFileLogger.cs
    │   └───NMSFileLoggerExtenstions.cs
    │   └───NMSFileLoggerOptions.cs
    │   └───NMSFileLoggerProvider.cs
    ├───logs
    │   └───log_20220822.log (For Audit)
    ├───Migrations - For database - Code First Approach
    ├───Models
    │   └───Category.cs
    │   └───Note.cs
    ├───Core
  	   │   
  	   │   
    	   ├───IRepository
    		  └───ICategorysRepository.cs
    		  └───INoteRepository.cs
    	   ├───Repository
    		  └───CategorysRepository.cs
    		  └───NoteRepository.cs
    ├───ViewModels
    │   └───NotesVM.cs
    ├───Views
    │   ├───Home
    │   ├───Notes
    │   └───Index.cshtml
    │   └───Upsert.cshtml
    │   └───_viewAll.cshtml
    │
	└───wwwroot
    		└───CSS
			└───site.css
    		└───js
			└───site.js
			└───notify.min.js
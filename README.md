# ToDoApp

This application is designed to allow users to keep track of their day to day tasks

## Description

ToDoApp is a desktop task management application built with C# and Windows Forms on .NET 8.0, designed to provide users with an intuitive and efficient way to organize their daily, weekly, and long-term tasks. The application features a modern, clean user interface with custom-styled components including task cards with accent stripes, smooth hover effects, and animated task completion transitions. At its core, the application utilizes a DataTable-based architecture for in-memory task management, offering real-time task filtering across three distinct view modes: All Tasks, Daily, and Weekly. Tasks are organized with comprehensive metadata including title, description, category assignment, creation timestamp, and completion status, enabling users to track their productivity across different time horizons. The application implements persistent data storage through SQLite database integration using Microsoft.Data.Sqlite, ensuring that all uncompleted tasks are automatically saved when the application closes and seamlessly restored upon reopening. The database architecture employs a straightforward yet robust schema with proper parameterized queries to prevent SQL injection vulnerabilities, while maintaining efficient CRUD operations for task management.

### Dependencies

The following NuGet packages are required to run this application:
* Microsoft.Data.Sqlite (latest version) - Provides SQLite database functionality for .NET
* SQLitePCLRaw.bundle_e_sqlite3 (latest version) - Native SQLite library required by Microsoft.Data.Sqlite

### Executing program

When you run the application for the first time:

* The app will automatically create a database file in your AppData folder:

**Location: C:\Users\[YourUsername]\AppData\Roaming\ToDoApp\tasks.db


*No additional configuration is needed - start adding tasks immediately!

## Authors
Contributors
* Nikolaus Phillips
* Owen Pell


Inspiration, code snippets, etc.
* [awesome-readme](https://github.com/matiassingers/awesome-readme)
* [PurpleBooth](https://gist.github.com/PurpleBooth/109311bb0361f32d87a2)
* [dbader](https://github.com/dbader/readme-template)
* [zenorocha](https://gist.github.com/zenorocha/4526327)
* [fvcproductions](https://gist.github.com/fvcproductions/1bfc2d4aecb01a834b46)

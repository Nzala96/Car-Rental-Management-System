Step 1: Double Click on CarRental.sln
Step2 : In the shared folder, copy the database file called "CarRentaldb.mdf" and paste it in your documents
Step 3: Once The Solution is open in visual studio, click on server explorer
Step 4: Then Click on "Connect to Database"
Step 5: Browse to the location of the database file(which you copied into "your documents")
Step6: Once the database is successfully added, Right-click on it and click on property
Step7 : Copy the connection string and replace it in all the sqlconnection objects.

SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nuwak\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

Step8 : Run the project and enjoy this wonderfull application
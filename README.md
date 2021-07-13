NOTE: This project was created in early 2020, and was the first time I used Github.  I couldn't figure out how folders worked at the time and that is why everything is in the root directory.  ew.  In my other project I uploaded tonight 7/12 (the simple game), you can see that the project is properly structured.  

To run on Windows: Download the ZIP, extract all, and run the executable. 

To run on MacOS: 
1) Download the .NET SDK for MacOS here: https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install 
2) Run the package to install 
3) Download the ZIP from github at: https://github.com/FireCodeJohn/ExportFireData 
4) Open the terminal and navigate to the downloaded project folder (for example ~/Downloads/ExportFireData-master) 
5) run the command "dotnet ExportFireData.dll" to run the program in MacOS terminal 

You can export all responses or export all incidents to an output file for a date range. 
Time of day is not considered, the program will export each response/incident for an entire day or a range of days. 

If you export all responses, each row in the data table will have its own JSON file with unique file path in the output.  
For example, if there are two rows with the call_number "1000013352", there will be two JSON files in the output directory with the names:  
"call_1000013352", and 
"call_1000013352_2". 

If you export all incidents there will be only 1 file for each unique incident number. 
Each row in the table with the same incident number will be exported to the same Json file. 

I used simple HTTPS GET Requests with the SoQL parameters: where, limit, and offset.  
I make as many HTTPS requests as needed to get all the data I need.  

I spent about 1 hours researching the prompt and database, 
2 hours building my BusinessObjects and BusinessLogic, and
5 hours researching SODA.NET, implementing it, and trying to fix an exception that was caused by SODA.NET.  
At that point I gave up with SODA.NET. 
I spent about 1 hour finishing the app with simple HTTPS requests and handling the responses.  
I spent another hour adding the option to export all Incidents.   
I also spent another hour figuring out how to run the program on MacOS, and fixing bugs unique to MacOS.  

4 pictures are available in the github commit, as well as an example response json file and an example incident json file.  

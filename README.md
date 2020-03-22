To run the code: Download on Windows, and run the executable.

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

I spent about 8 hours researching the prompt and database, building my BusinessObjects and BusinessLogic, and fumbling with SODA.NET.
After giving up with SODA.NET, I spent about 2 hours finishing the app with simple HTTPS requests and handling the responses.

4 pictures are available in the github commit, as well as an example response json file and an example incident json file.
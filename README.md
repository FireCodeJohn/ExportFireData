To run the code: Download on Windows, and run the executable.

In the event there are multiple rows with the same call_number, each row will have its own JSON file with unique file path in the output.  

I used a simple HTTPS Request or requests with the SoQL parameters where, limit, and offset.  I make as many HTTPS requests as needed to get all the data I need.  

I spent about 70 minutes building the simple HTTPS request(s) and handling the response(s), after scrapping my attempt to use SODA.NET.

2 pictures are available in the github commit.
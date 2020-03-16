To run the code: Download on Windows, and run the executable.

In the event there are multiple rows with the same call_number, each row will have its own JSON file with unique file path in the output.  

I was able to export all responses into a specified directory as per the prompt.
However, I was not able to figure out how to use the Resource.Query<T>(SoqlQuery query) function in SODA.NET.  I keep on getting an exception involving Json serialization that I believe might be a bug in SODA.NET.
Because of this, I applied the filter after getting rows with the Resource.GetRows(int limit), which means the application can be very slow. 

I spent about 8 hours on this including research on SODA.NET, and (unsuccessfully) attempting to resolve the Resource.Query(soql) problem.

2 pictures are available in the github commit.
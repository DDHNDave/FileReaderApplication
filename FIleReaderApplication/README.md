

# Overview solution
A console applications that can read a file and count the number of times the filename is repeated in the file
The file name without file name extension will be matched for every line and between file lines

The application can take a single file path by:
- Sending a commandline argument without name or = before the file path, example --D:\Codejams\TestFil.txt

- Sending a commandline argument with name that contains the word path and = before the file path, example filePath=D:\Codejams\TestFil.txt

- Sending a commandline argument without name or = before a directory path, example D:\Codejams
  It will scan all files in that directory and its subdirectorys for how many times the file contains the file name without extension name
  
- Sending a commandline argument with name that contains the word path and = before a directory path, example filePath=D:\Codejams
  It will scan all files in that directory and its subdirectorys for how many times the file contains the file name without extension name
  
- not  sending a file path as commandline argument. The user will be prompted to enter a file or directory path through the console

The soluton also includes unit tests written in xunit 

The solution uses dependency injection and a generic .NET Generic Host to run the program. 
The main program is not implemented as a IHostedService 

# How to run the solution

Option one:
1 - Clone the repos and open the solution file in visual studio
2 - Build the solution.
3 - Navigate to the folder {YOUR_FILEPATH_TO_PROJECT}\FileProjects\FIleReaderApplication\FileReader\bin\Debug\netcoreapp3.1>
4 - From the path from step 3, type filereader. Examples 
	- {YOUR_FILEPATH_TO_PROJECT}\FileProjects\FIleReaderApplication\FileReader\bin\Debug\netcoreapp3.1>filereader
	  (above will let you enter a filepath trough concole)
	- {YOUR_FILEPATH_TO_PROJECT}\FileProjects\FIleReaderApplication\FileReader\bin\Debug\netcoreapp3.1>filereader filereader {PATH_TO_YOUR_FILE_DIRECTORY}\{FILENAME}.{EXTENSION}
	  (above will scan a specific file)
	- {YOUR_FILEPATH_TO_PROJECT}\FileProjects\FIleReaderApplication\FileReader\bin\Debug\netcoreapp3.1>filereader filereader {PATH_TO_YOUR_FILE_DIRECTORY}
	  (above will scan a whole directory)
	

Option two
1 - Clone the repos and open the solution file in visual studio
2 - Debug the solution with visual studio


# Programming Test  description
- This test takes already existing code and asks you to improve it. 
- After the code has been submitted, we would like to go through it together with you and discuss your solution. 
- There is no time limit, and you may use online resources for help, as long as you write the code yourself. 

Specification:
- Write a console program that takes one argument, a path to a file. 
- Open that file and count how many times its filename (minus the file extension) occurs in the file's contents. 
- Example: If the argument is "myfile.txt" it should count how many times the string "myfile" occurs in it. 

- The existing code The program works, mostly. 
- There are some bugs. The code is not well structured. 
- There are no tests. 
- It can be compiled as a .NET Core or a .NET Framework program. 

Your job Rewrite this program to fulfill the specification correctly. Make the code beautiful. 
Think about everything that could go wrong, and handle that with good error messages. 
Write tests that you think are needed. 
If the specification isn't clear enough about what to do in situations, make an assumption and document it. 
Technology You may use .NET Framework or .NET Core, C# or F#, and all libraries and tools you need. 
As long as you send us all files and instructions needed to run the program.
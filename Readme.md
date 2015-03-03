Commandline arguments parsing - F# Kata
----------------------------

Example:
SomeApp.exe -L -P 3456 -D c:\temp\

1. -L - enable logging (if arg missing - logging disabled)
1. -P - set port value (if arg missing - port disabled), port has to be int that is > 1024
1. -D - set working directory (if arg missing - no directory should be set), path has to be of exsiting directory

Order of arguments is not important.
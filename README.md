# Learning
Code level changes ,in application add your Ilogger<classname> and log Information , errors according to need.
Every Azure app has log system > App service log 
you have to activate it on azure portal (file & blob )
Through Application you can add AzureFileLoggerOptions & AzureBlobLoggerOptions, here you can customize file names and size etc 
in my case i added fileloogeroption and mentined file name so my logs are created with this name , you can check it on App> advance tool > Kudo> Debug console > CMD > logFiles> application > "myfile".txt
Logging:AzureAppServicesFile:LogLevel:Microsoft added this value as "Information" in Env variable of app to override appsetting.json key to see all the errors.

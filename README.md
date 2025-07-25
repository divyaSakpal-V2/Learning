# Learning
Create Key vault and add secrets in key value pair , based on key we will fetch value. 
After creation you will get Azure key vault url.
In your App, Added Azure key vault url in applicationsetting.json and used that configuration in program.cs to fetch secrets from azure portal with provided URL 
Once secrets in configuration object now you use them in application Iconfiguration
access on keyvault - your ID or Application ID need to be register to key vault then only they can use keyvault resource 
keyvault > access policies (enabled if you select 'vault access policies' in 'access configuration')
You can search Object ID of your application >Identity > status as ON > Object /principle ID 
Create access policy for your manage identity of Aplication and grant permissions to app to use key vault 

# Learning
Create Cosmos-DB with permissions if needed 
every no-sql db has primary key and secondary key and DB url , you need this information to acess it 
you can create DB and container throgh application - cosmos client is object that needed DB name and container name with (URL and primary key= connection string ) to open connection. in first time it will create DB & container for you if it is not exists on cosmos DB.
once you open connection you can CRUD on items or document same as other DBs.
I have saved Primary key only in azure key vault and all other configuration in appsetting.json >cosmosDb 

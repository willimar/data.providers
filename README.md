# data.providers
Provider .Net Core to DataBase Connection

data.provider.core
Domain to providers

IDataProvider -> Has contract to data provider
	GetDataSet<TEntity>
		TEntity is the representation from data table.
		Get dataset with representation from data structure.

IDataSet -> Has contract to data transactions
	void Append(IEnumerable<TEntity> entity);
		Append one or more records in data table 
		entity represent a list from the entity
		
    long DeleteRecords(Expression<Func<TEntity, bool>> predicate);
		Delete one or more records in data table
		predicate is the lambda to identify records in data table
		
    long UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity);
		Change data in data table. Only one recor will be modified
		predicate identify the record
		entity with data to change
		
    IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, int limit = 0);
		Get a list with all records found in data.
		predicate is the rule to find data in data table
		limit will be limit max records returned
		
    long Count(Expression<Func<TEntity, bool>> predicate);
		Get the count from the records found
		predicate is role to find records
		
To see a example use test project

Use docker container to tests, bellow has code to run the containers

	docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=<password key> -d mysql:8
	docker run -d -p 27017-27019:27017-27019 --name mongodb mongo
	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<password key>" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04

### Setup docker container locally

```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

1. Open "..\T-SQL Fundamentals by Itzik Ben-Gan\resources\TSQLV6.sql" file
2. Connect to database
3. Execute
4. Open T-SQL Fundamentals by Itzik Ben-Gan directory as Notebook in Azure Data Studio
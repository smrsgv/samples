### Setup docker container locally

```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

Connect and execute ".\samples\database\tsql\T-SQL Fundamentals by Itzik Ben-Gan\resources\TSQLV6.sql"
 docker-compose up -d
 
 docker exec -it warhammer_database mkdir /var/opt/mssql/backup
   
 docker cp WarhammerDb.bak warhammer_database:/var/opt/mssql/backup
   
docker exec -it warhammer_database /opt/mssql-tools/bin/sqlcmd -S localhost `
   -U SA -P "<YourStrong!Passw0rd>" `
   -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/WarhammerDb.bak'"
   
   
   docker exec -it warhammer_database /opt/mssql-tools/bin/sqlcmd `
   -S localhost -U SA -P "<YourStrong!Passw0rd>" `
   -Q "RESTORE DATABASE WarhammerDb FROM DISK = '/var/opt/mssql/backup/WarhammerDb.bak' WITH MOVE 'WarhammerDb' TO '/var/opt/mssql/data/WarhammerDb.mdf', MOVE 'WarhammerDb_log' TO '/var/opt/mssql/data/WarhammerDb_log.ldf'"
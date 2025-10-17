create docker database container
```bash
 docker run -d --name cellsync-db -e POSTGRES_PASSWORD=@Password123 -e PGDATA=/var/lib/postgresql/data/pgdata -v /custom/mount:/var/lib/postgresql/data -p 5432:5432 postgres
```
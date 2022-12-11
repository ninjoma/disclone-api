# DISCLONE - API

Disclone - api is the api used by the famous app DISCLONE to expose data

## Instalation

run this command in a terminal
```bash
dotnet run
```

if you can't connect to the amazon rds database you must install the [postgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
and you must run this commands
```bash
dotnet tool install --global dotnet-ef

dotnet ef database update
```

you can also run the app with docker by using this commands
```bash
docker build -t disclone-api .

docker run -it --rm -p 5610:5610 --name disclone-api disclone-api
```


## Usage

just run the front end in this link [disclone - web](https://github.com/ninjoma/disclone-web).

## License

open - source free to use in all scenarios 


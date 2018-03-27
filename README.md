## f1wm
Extracting web API of [f1wm.pl](https://f1wm.pl), piece by piece. That is a rewrite from PHP-based site to .NET core web API.

## setting up local environment
In order to run the API locally or run integration tests, you need to setup [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=visual-studio) first.
User secrets include connection string information. To populate it, run (in F1WM project directory):
`dotnet user-secrets set ConnectionStrings:DefaultConnectionString "server=<server>;user id=<username>;password=<password>;port=3306;database=<database>;charset=<charset>"`

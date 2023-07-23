# Different Databases Per User

How can we fetch information from different databases based on a parameter? How do we do this with an ORM like Entity Framework?

## Description

The main idea is to direct each user to the assigned database to obtain the information they need, for this, we have made this example in which we implement a factory pattern to get the context at runtime.

## How to execute it?

The first one is to lift the two containers that we find in the docker compose with the PostgreSQL database. 
To do this we can run F5 from Visual Studio with the docker compose project as the main one. Then we stop the execution and we have to launch the migrations in both databases (this step only needs to be done once, as we save a volume with the databases information).

```console

dotnet ef database update --connection "Host=localhost:5432;Database=Catalog;Username=postgres;Password=postgres"
dotnet ef database update --connection "Host=localhost:5433;Database=Catalog;Username=postgres;Password=postgres"

```
> If you have modified the docker compose, take into account the port of both databases.

In the code is hardcoded the two users that can be used:

```csharp
var connectionStrings = new Dictionary<string, string>
            {
                { "User1", "Host=pgdb1:5432;Database=Catalog;Username=postgres;Password=postgres" },
                { "User2", "Host=pgdb2:5432;Database=Catalog;Username=postgres;Password=postgres" }
            };
```

## Contributions

Contributions are highly appreciated! Should you encounter any issues or wish to improve the project, please submit a pull request. To ensure effective collaboration, kindly adhere to our contribution guidelines.

## License

This project is licensed under the MIT License. For more information, please refer to the [LICENSE file](LICENSE).

## Contact

For any questions or comments, feel free to reach out to us via Discord.

Enjoy the development experience!


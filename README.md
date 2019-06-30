## README

### FluentMigrator Set Up
1. Create Folder at the route of your C: Drive named `Scripts`
1. Add `Scripts` to your PATH
1. Create file `migrateTriviaDrunks.bat`
1. Insert the following into the above file `dotnet-fm migrate -p SqlServer2016 -c "Server=(local);Database=TriviaDrunks;user={askAdam};password={askAdam};Trusted_Connection=True;" -a C:\{AddRestOfYourPath}\MigrationHelper\bin\Debug\netcoreapp2.2\MigrationHelper.dll`
1. `dotnet tool install --global FluentMigrator.DotNet.Cli --version 3.2.1`
1. Build the `MigrationHelper` project
1. Run `migrateTriviaDrunks.bat` from powershell with admin priv
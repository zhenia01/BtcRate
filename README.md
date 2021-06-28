# BtcRate
Minimalistic API to view BTC to UAH exchange rate

Written using C#/.NET 5

## Usage guide: 
1. Download [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Open terminal in a **BtcRate/WebAPI** folder
3. Run _dotnet run_ command
4. Open https://localhost:5001/swagger/index.html to see Swagger UI docs or use https://localhost:5001/{endpoint} in your favorite API client

### Additional info
- API uses JWT auth, token in returned from the _/user/login_ endpoint
- You can find CSV file in a folder where executable file is being created (for example _BtcRate\WebAPI\bin\Debug\net5.0_)

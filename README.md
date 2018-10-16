# Lob.Net [![Build Status](https://travis-ci.org/jsgoupil/lob.net.svg?branch=master)](https://travis-ci.org/jsgoupil/lob.net) #

Lob.Net is a wrapper for the Lob.com API. It follows the latest technology regarding dependency injection and strong typing.

**This project is actively maintained and is in its early alpha stage. Many breaks will be introduced until stability is reached.**

## Installation ##

Install package:

```
PM> Install-Package Lob.Net
```

## Setup ##

Load the Lob interfaces in your Startup.cs as such:

```C#
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

private IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
    services
        .AddLob(options => options.ApiKey = Configuration["Lob:ApiKey"]);
}
```

Include your API key in the appsettings.json

```
{
  "Lob": {
    "ApiKey": "test_api_key"
  }
}
```

## Usage ##

Inject the appropriate interface in your controller in order to send postcards, letters, etc.

### Postcards ###

```C#
[Route("api/[controller]")]
public class PostcardsController : Controller
{
    private readonly ILobPostcards lobPostcards;

    public PostcardsController(
        ILobPostcards lobPostcards
    )
    {
        this.lobPostcards = lobPostcards;
    }

    [HttpPost]
    [Route("")]
    public async Task SendPostcard()
    {
        try
        {
            var result1 = await lobPostcards.CreateAsync(new PostcardRequest
            {
                From = new AddressReference(new Address
                {
                    Name = "Jean-S�bastien Goupil",
                    Company = "JSGoupil, LLC",
                    AddressLine1 = "123 Main Street",
                    AddressCity = "Seattle",
                    AddressState = "WA",
                    AddressZip = "98103",
                    AddressCountry = "US"
                }),
                To = new AddressReference("adr_738379e5622a9f04"), // Saved address in LOB
                Front = "tmpl_7e7fdb7d1cb261d", // Saved template in LOB
                Back = "tmpl_7e7fdb7d1cb261d", // Saved template in LOB
                MergeVariables = new Dictionary<string, string>
                {
                    {"variable_name", "Jean-S�bastien" }
                }
            });
        }
        catch (LobException ex)
        {
            throw ex;
        }
    }
}
```

Postcards APIs:
```C#
public interface ILobPostcards
{
    Task<PostcardResponse> CreateAsync(PostcardRequest postcard, string idempotencyKey = null);
    Task<PostcardResponse> RetrieveAsync(string id);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<ListResponse<PostcardResponse>> ListAsync(PostcardFilter filter = null);
}
```

### Letters ###

```C#
public interface ILobLetters
{
    Task<LetterResponse> CreateAsync(LetterRequest letter, string idempotencyKey = null);
    Task<LetterResponse> RetrieveAsync(string id);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<ListResponse<LetterResponse>> ListAsync(LetterFilter filter = null);
}
```

### Bank Accounts ###

```C#
public interface ILobBankAccounts
{
    Task<BankAccountResponse> CreateAsync(BankAccountRequest bankAccount, string idempotencyKey = null);
    Task<BankAccountResponse> RetrieveAsync(string id);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<BankAccountResponse> VerifyAsync(string id, int amountInCents1, int amountInCents2);
    Task<ListResponse<BankAccountResponse>> ListAsync(BankAccountFilter filter = null);
}
```

### Checks ###

```C#
public interface ILobChecks
{
    Task<CheckResponse> CreateAsync(CheckRequest check, string idempotencyKey = null);
    Task<CheckResponse> RetrieveAsync(string id);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<ListResponse<CheckResponse>> ListAsync(CheckFilter filter = null);
}
```

### Addresses ###

```C#
public interface ILobAddresses
{
    Task<CheckResponse> CreateAsync(CheckRequest check, string idempotencyKey = null);
    Task<CheckResponse> RetrieveAsync(string id);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<ListResponse<CheckResponse>> ListAsync(CheckFilter filter = null);
}
```

Follow the other examples in the sample folder.

## Contributing

Contributions are welcome. Code or documentation!

1. Fork this project
2. Create a feature/bug fix branch
3. Push your branch up to your fork
4. Submit a pull request

## License

Lob.Net is under the MIT license.

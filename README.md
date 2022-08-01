# Unofficial generated IPinfo C# Client Library based on OpenAPI spec

This is the unofficial C# client library for the IPinfo.io IP address API, allowing you to lookup your own IP address, or get any of the following details for an IP:

 - [IP geolocation / geoIP data](https://ipinfo.io/ip-geolocation-api) (city, region, country, postal code, latitude and longitude)
 - [ASN details](https://ipinfo.io/asn-api) (ISP or network operator, associated domain name, and type, such as business, hosting or company)
 - [Firmographics data](https://ipinfo.io/ip-company-api) (the name and domain of the business that uses the IP address)
 - [Carrier information](https://ipinfo.io/ip-carrier-api) (the name of the mobile carrier and MNC and MCC for that carrier if the IP is used exclusively for mobile traffic)

Pros compared to the new official version:
- Almost completely generated based on the OpenAPI specification, which eliminates errors when writing boiler plate code
- The OpenAPI specification can be used to generate a client for your needs, for any language
- All APIs are implemented
- There is no synchronous API here which is prone to Task.Wait() errors

Cons:
- No built-in caching

## Getting Started

You'll need an IPinfo API access token, which you can get by singing up for a free account at [https://ipinfo.io/signup](https://ipinfo.io/signup).

The free plan is limited to 50,000 requests per month, and doesn't include some of the data fields such as IP type and company data. To enable all the data fields and additional request volumes see [https://ipinfo.io/pricing](https://ipinfo.io/pricing)

## Nuget

[![NuGet](https://img.shields.io/nuget/dt/H.IpInfo.svg?style=flat-square&label=H.IpInfo)](https://www.nuget.org/packages/H.IpInfo/)

```
Install-Package H.IpInfo
```

## Usage

```cs
using IpInfo;

using var client = new HttpClient();
// Some methods work without a token, for this case there is a constructor without a token.
var api = new IpInfoApi("your-token", client);

var response = await api.GetCurrentInformationAsync();

Console.WriteLine($"City: {response.City}");
```

## Batch API

```cs
// WARNING: Token required.
var dictionary = await api.GetInformationByIpsAsync(new[]
{
    "8.8.8.8",
    "8.8.4.4",
}, cancellationToken);

foreach (var pair in dictionary)
{
    Console.WriteLine($"{pair.Key} City: {pair.Value.City}");
}

// 8.8.4.4 City: Amstelveen
// 8.8.8.8 City: Mountain View


// WARNING: Token required.
var dictionary = await api.BatchAsync(new []
{
    "8.8.4.4/city",
    "8.8.8.8/city",
}, cancellationToken);

foreach (var pair in dictionary)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// 8.8.4.4: Amstelveen
// 8.8.8.8: Mountain View
```

## Privacy Detection API

```cs
// WARNING: Token required. The token must have at least permissions of the Business Plan.
// Otherwise, you'll get a response with an HTTP 403 status code. 
var privacy = await api.GetPrivacyInformationByIpAsync("8.8.8.8", cancellationToken);

Console.WriteLine($"Vpn: {privacy.Vpn}");
Console.WriteLine($"Proxy: {privacy.Proxy}");
Console.WriteLine($"Tor: {privacy.Tor}");
Console.WriteLine($"Hosting: {privacy.Hosting}");

// Vpn: False
// Proxy: False
// Tor: False
// Hosting: False
```

## Contacts
* [mail](mailto:havendv@gmail.com)

## Other Libraries

There are official [IPinfo client libraries](https://ipinfo.io/developers/libraries) available for many languages including 
PHP, Go, Java, Ruby, [C#](https://github.com/ipinfo/csharp/) and many popular frameworks such as Django, Rails and Laravel. 
There are also many third party libraries and integrations available for our API.
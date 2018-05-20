# My.Jdownloader.Api .NET Wrapper by Cr1TiKa7

This is an simple wrapper for the official My.JDownloader.Api written in c#.
It's an rewritten and extended version of https://github.com/noone2407/MyJDownloaderApi.

Currently it supports: .NET 4.0, .NET 4.5, .NET 4.6 and .NETCore 2.0

> Feel free to request missing calls, bugs or suggestions for improvements.

#### Usage

First of all you need an instance of the JDownloaderHandler
``` c#
JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
_jdownloaderHandler.Connect("YOURMYJDOWNLOADERUSERNAME", "YOURMYJDOWNLOADERPASSWORD");
//Alternaitve
JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler("YOURMYJDOWNLOADERUSERNAME", "YOURMYJDOWNLOADERPASSWORD");
```

The JDownloaderHandler contains the following methods:
- Connect(email, password) : boolean
- Reconnect() : boolean
- Disconnect() : boolean
- GetDevices() : List<DeviceObject>

#### Namespaces

For a better overview, the calls are split into different namespaces.
So far the library contains the following namespaces and methods:

- AccountsV2 | 14/14 calls done
    - AddAccount(device, email, password) : boolean
	- AddBasicAuth(device, BasicAuthObject) : boolean
    - DisableAccounts(device, accountIds) : boolean
    - EnableAccounts(device, accountIds) : boolean
	- GetPremiumHosterUrl(device, hoster) : string
    - ListAccounts(device, ListAccountRequestObject) : ListAccountResponseObject[]
	- ListBasicAuth(device) : ListBasicAuthResponseObject[]
	- ListPremiumHoster(device) : string[]
    - ListPremiumHosterUrls(device) : Dictionary<string, string>
    - RefreshAccounts(device, accountIds) : boolean
    - RemoveAccounts(device, accountIds) : boolean
	- RemoveBasicAuths(device, basicAuthIds) : boolean
    - SetUsernameAndPassword(device, accountId, email, password) : boolean
	- UpdateBasicAuth(device, BasicAuthObject) : boolean
	
- DownloadController | 6/6 calls done
	- ForceDownload(device, linkIds[], packageIds[]) : boolean
    - Start(device) : boolean
    - Stop(device) : boolean
    - Pause(device, pause) : boolean
    - GetCurrentState(device) : string
	- GetSpeedInBps(device) : long
- LinkgrabberV2 | 8/30 calls done
    - AddLinks(device, AddLinkRequestObject) : boolean
    - AddContainer(device, containerType, content) : boolean
    - ClearList(Device) : boolean
    - GetPackageCount(Device) : int
    - IsCollecting(device) : boolean
    - QueryLinks(device, maxResults) : QueryLinksResponseObject
	- QueryPackages(device, QueryPackagesRequestObject) : List<QueryPackagesResponseObject>
	- SetDownloadDirectory(device, directory, packageIds[]) : boolean

#### Samples

To get all devices that are currently connected to your My.JDownloader account:
``` c#
var devices = _jdownloaderHandler.GetDevices();
``` 

To add links to the linkcollector:
``` c#
_jdownloaderHandler.LinkgrabberV2.AddLinks(
    devices[0],
    new AddLinkRequestObject
    {
        Links = "www.testurl.com;www.testurl2.com",
        PackageName = "Test Package"
    });
``` 

To start downloading:
``` c#
_jdownloaderHandler.DownloadController.Start(devices[0]);
``` 
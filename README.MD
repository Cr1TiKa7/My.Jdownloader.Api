# My.Jdownloader.Api .NET Wrapper by Cr1TiKa7

This is an simple wrapper for the official My.JDownloader.Api written in c#.
It's an rewritten and extended version of https://github.com/noone2407/MyJDownloaderApi.

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
- GetDevices() : List<DeviceObject)

#### Namespaces

For a better overview, the calls are split into different namespaces.
So far the library contains the following namespaces and methods:

- AccountsV2
    - AddAccount(device, email, password) : boolean
    - EnableAccounts(device, accountIds) : boolean
    - DisableAccounts(device, accountIds) : boolean
    - ListAccounts(device, ListAccountRequestObject) : ListAccountResponseObject[]
    - ListPremiumHosterUrls(device) : Dictionary<string, string>
    - RefreshAccounts(device, accountIds) : boolean
    - RemoveAccounts(device, accountIds) : boolean
    - SetUsernameAndPassword(device, accountId, email, password) : boolean
- DownloadController
    - Start(device) : boolean
    - Stop(device) : boolean
    - Pause(device, pause) : boolean
    - GetCurrentState(device) : string
- LinkgrabberV2
    - AddLinks(device, AddLinkRequestObject) : boolean
    - AddContainer(device, containerType, content) : boolean
    - ClearList(Device) : boolean
    - GetPackageCount(Device) : int
    - IsCollecting(device) : boolean
    - QueryLinks(device, maxResults) : CrawledLinkDataObject

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
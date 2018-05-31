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
    - AddAccount(email, password) : boolean
	- AddBasicAuth(BasicAuthObject) : boolean
    - DisableAccounts(accountIds) : boolean
    - EnableAccounts(accountIds) : boolean
	- GetPremiumHosterUrl(hoster) : string
    - ListAccounts(ListAccountRequestObject) : ListAccountResponseObject[]
	- ListBasicAuth() : ListBasicAuthResponseObject[]
	- ListPremiumHoster() : string[]
    - ListPremiumHosterUrls() : Dictionary<string, string>
    - RefreshAccounts(accountIds) : boolean
    - RemoveAccounts(accountIds) : boolean
	- RemoveBasicAuths(basicAuthIds) : boolean
    - SetUsernameAndPassword(accountId, email, password) : boolean
	- UpdateBasicAuth(BasicAuthObject) : boolean
	
- DownloadController | 6/6 calls done
	- ForceDownload(linkIds[], packageIds[]) : boolean
    - Start() : boolean
    - Stop() : boolean
    - Pause(pause) : boolean
    - GetCurrentState() : string
	- GetSpeedInBps() : long

- DownloadsV2 | 2/x calls done
	- GetStopMark() : long
	- GetStopMarkedLink() : StopMarkedLinkReturnObject
	
- Extensions | 5/5 calls done
	- Install(extensionId) : boolean
	- IsEnabled(className) : boolean
	- IsInstalled(extensionId) : boolean
	- List(ExtensionRequestObject) : ExtensionResponseObject[]
	- SetEnabled(className, enabled) : boolean

- Extraction | 2/7 calls done
	- AddArchivePassword(password) : boolean
	- CancelExtraction(controllerId) : boolean

- JD | 6/6 calls done
	- DoSomethingCool()
	- GetCoreRevision() : int
	- RefreshPlugins() : boolean
	- Sum() : int
	- Uptime() : long
	- Version() : long
	
- LinkCrawler | 1/1 calls done
    - IsCrawling() : boolean
    
- LinkGrabberV2 | 19/30 calls done
	- Abort() : boolean
	- Abort(jobId) : boolean
    - AddLinks(AddLinkRequestObject) : boolean
    - AddContainer(containerType, content) : boolean
    - ClearList() : boolean
	- CleanUp(linkIds, packageIds, action, mode, selection) : boolean
	- GetChildrenChanged(structureWatermark) : long
	- GetDownloadFolderHistorySelectionBase() : string[]
	- GetDownloadUrls(linkIds, afterLinkId, destPackageId) : Dictionary<string, List<long>>
    - GetPackageCount() : int
	- GetVariants(linkId) : GetVariantsReturnObject[]
    - IsCollecting() : boolean
	- MoveLinks(linkIds, afterLinkId, destPackageId) : boolean
	- MovePackages(packageIds, afterDestPackageId) : boolean
	- MoveToDownloadList(linkIds, packageIds) : boolean
	- MoveToNewPackage(linkIds, packageIds, newPackageName, downloadPath) : boolean
    - QueryLinks(maxResults) : QueryLinksResponseObject
	- QueryPackages(QueryPackagesRequestObject) : List<QueryPackagesResponseObject>
	- SetDownloadDirectory(directory, packageIds[]) : boolean
	
- System | 7/7 calls done
    - ExitJd()
	- GetStorageInfos(path) : StorageInfoReturnObject[]
	- GetSystemInfos() : SystemInfoReturnObject
	- HibernateOS()
	- RestartJd()
	- ShutdownOS()
	- StandbyOS()

- Update | 3/3 calls done
	- IsUpdateAvailable() : boolean
	- RestartAndUpdate()
	- RunUpdateCheck()

#### Samples

Here is a simple sample code which shows you how to connect, get a device handler and how to call a method:

``` c#
JDownloaderHandler _JDownloaderHandler = new JDownloaderHandler("YOUREMAIL","YOURPASSWORD");
if (_JDownloaderHandler.IsConnected)
{
    var devices = _JDownloaderHandler.GetDevices();
    if (devices.Count = 0)
    return;
    
    var deviceHandler = _JDownloaderHandler.GetDeviceInstance(devices[0]);
    deviceHandler.AccountV2.AddAccount("mega.co.nz", "test123", "test123");
}
``` 

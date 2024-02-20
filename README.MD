# My.Jdownloader.Api .NET Wrapper by Cr1TiKa7

This is an simple wrapper for the official My.JDownloader.Api written in c#.
It's an rewritten and extended version of https://github.com/noone2407/MyJDownloaderApi.

Currently it supports: .NET 8

> Feel free to request missing calls, bugs or suggestions for improvements.

## Usage

First of all you need an instance of the JDownloaderHandler
``` c#
JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler("YOURAPPKEY");
_jdownloaderHandler.Connect("YOURMYJDOWNLOADERUSERNAME", "YOURMYJDOWNLOADERPASSWORD");
//Alternaitve
JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler("YOURMYJDOWNLOADERUSERNAME", "YOURMYJDOWNLOADERPASSWORD", "YOURAPPKEY");
```

> The JDownloaderHandler contains the following methods:
> - Connect(email, password) : boolean
> - Reconnect() : boolean
> - Disconnect() : boolean
> - GetDevices() : List<DeviceObject>

After you successfully connected to the api you can get your active devices like this:

``` c#
var devices = jdownloaderHandler.GetDevices();
```

Now that you got your devices you can call the methods of the namespaces on the device:

``` c#
var device = devices[0];
//device.<namespace>.<method>();
device.Jd.DoSomethingCool();
```

## Changelog

### __Version 2.4.5__
Added:
- setComment call to the DownloadsV2 and LinkgrabberV2 namespace (Thanks to jeffward01)

Fixed:
- Cleanup endpoint

### __Version 2.4.3__
Added:
- OverridePackagizer Property to the AddLinkRequest class (Thanks to devocalypse)
- SaveTo Property to the LinkQueryRequest class (Thanks to tprivaterep)  

Fixed:
- Wrong endpoint for LinkGrabberV2.IsCollecting (Thanks to devocalypse)


#### Namespaces

For a better overview, the calls are split into different namespaces.
So far the library contains the following namespaces and methods:

- Accounts | 11/12 calls done
	
- AccountsV2 | 14/14 calls done
	
- Captcha | 7/7 calls done
	
- CaptchaForward | 2/2 calls done

- Config | 8/8 calls done

- DownloadController | 6/6 calls done

- DownloadsV2 | 4/26 calls done

- Extensions | 5/5 calls done

- Extraction | 7/7 calls done

- JD | 6/6 calls done
	
- LinkCrawler | 1/1 calls done
    
- LinkGrabberV2 | 19/30 calls done
	
- System | 7/7 calls done

- Update | 3/3 calls done

#### Samples

Here is a simple sample code which shows you how to connect, get a device handler and how to call a method:

``` c#
JDownloaderHandler _JDownloaderHandler = new JDownloaderHandler("YOUREMAIL","YOURPASSWORD", "YOURAPPKEY");
if (_JDownloaderHandler.IsConnected)
{
    var devices = _JDownloaderHandler.GetDevices();
    if (devices.Count = 0)
    return;
    
    var deviceHandler = _JDownloaderHandler.GetDeviceHandler(devices[0]);
	//If you want to use the JDownloader API endpoint to connect to your device then you can call the GetDeviceHandler method like this:
    var deviceHandler = _JDownloaderHandler.GetDeviceHandler(devices[0], true);
    deviceHandler.AccountV2.AddAccount("mega.co.nz", "test123", "test123");
}
``` 

[![Build Status](https://travis-ci.com/gcastellov/scheduler.appservice.svg?branch=master)](https://travis-ci.com/gcastellov/scheduler.appservice)

# Scheduler.AppService

Scheduler.AppService is a Linux daemon that based on a scheduler configuration executes Web API tasks even when these require authentication.

The project is made by using .NET Core and Quartz.

## Supported authentication

Currently the project supports the following:
- JWT
- OAuth2 (Resource Owner Password Credentials Grant)
- No authentication at all

## Configuration

To add new jobs update the app.config file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="credentialStorage" type="Scheduler.Core.Configuration.CredentialsConfiguration, Scheduler.Core" />
    <section name="jobSettings" type="Scheduler.Core.Configuration.JobsConfiguration, Scheduler.Core" />
  </configSections>
  <credentialStorage>
    <credentials>
      <credential 
        id="api" 
        type="jwt" 
        username="[USERNAME]" 
        password="[PASSWORD]" 
        endpoint="[TOKEN_ENDPOINT]"
        responseReader="[RESPONSE_READER_FULL_TYPE]"
        />
    </credentials>
  </credentialStorage>
  <jobSettings>
    <jobs>
      <job name="SuggestedNews" expression="0 0/1 * 1/1 * ? *" credentials="api" endpoint="[JOB_ENDPOINT]" />
    </jobs>
  </jobSettings>
</configuration>
```

Each job configuration can use a credential configuration. If the job does not require authentication, then leave the job's credentials attribute empty.

Set the **responseReader** attribute with the full type of the IResponseReader implementation to  read the authentication token. The built-in **Scheduler.Core.Communication.ResponseReaders.RawResponseReader** type reads the token from the body content as plain text, while the **Scheduler.Core.Communication.ResponseReaders.DtoResponseReader<T>** can apply some logic to read the token from a certain Dto. The solution provides the *Scheduler.Core.Communication.Sample.AuthorizationDtoReader* as a sample to show this last case.

To choose between different authentication types, update the credential type attribute:
- Use the keyword **jwt** for JWT.
- Use the keyword **oauth** for OAuth.


To configure the Serilog logging implementation, update the appsettings.json by setting the pattern to use or where to store the log files with the desired name format.

```json
{
  "Logging": {
    "PathFormat": "Logs/{Date}.log",
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "RollingFile",
        "Args": {
          "logDirectory": ".\\Logs",
          "fileSizeLimitBytes": 1024,
          "pathFormat": "Logs/{Date}.log",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```


You might have to manually create Logs folder in and set write permission to it with chmod 777 
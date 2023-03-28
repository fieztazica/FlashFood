# FlashFood

Add 2 more files to run correctly:

- `connectionStrings.config`
```xml
<connectionStrings>
	<clear/>
	<add name="DefaultConnection" connectionString="Data Source=(Your Data Source);Initial Catalog=(Your Db Name);Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

- `secrets.config`
```xml
<appSettings>
	<add key ="TokenKey" value="(Random JWT Key Value)"/>
	<add key ="AdminEmail" value="(Your Admin Email)"/>
	<add key ="AdminPwd" value="(Your Admin Password)"/>
</appSettings>
```
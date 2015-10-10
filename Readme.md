This is the code for [dotnetmashup.azurewebsites.net](dotnetmashup.azurewebsites.net) a mashup of news feeds for the `.net` community.


## Why?

Right now ASP.NET 5 has a multitude of news from tweets, blog posts, [announcements repo](https://github.com/aspnet/Announcements), and the community standup.

This app is designed to try to aggregate them in one continuous scroll web page.



## How do I add my blog?

Add your blog information to src/DotNetMashup.Web/blogfeed.json submit a PR


## Contributing

Contributions are welcome, submit an issue with the feature you would like to add so we can discuss it ahead of time. To get the various feeds working you must add api keys to the `startup.cs` file.


You will need twitter, and github keys (or you can turn off said feeds in RepositoryFactory.cs)

You will need to do something like the following in `startup.cs` (please remember to not commit this). Usually these keys come from environment variables, you could also place them there, and not worry about altering `startup.cs`

```csharp
public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
       {
           config = new ConfigurationBuilder()
           .AddEnvironmentVariables()
           .Build();
           config["github"] = "githubkeyhere";
           config["twitterkey"] = "Consumer Key";
           config["twittersecret"] = "Consumer Secret ";
           config["twittertokenKey"] = "Access Token";
           config["twittertokenSecret"] = "Access Token Secret";
       }

```

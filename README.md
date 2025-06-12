# MauiSentryTest

A MAUI test app to demonstrate and test [Sentry](https://sentry.io/) integration with the
[.NET MAUI framework](https://dotnet.microsoft.com/en-us/apps/maui).

<!-- omit from toc -->
## Contents
- [Project Structure](#project-structure)
- [Project Configuration](#project-configuration)
  - [Create a Sentry Project](#create-a-sentry-project)
  - [Authenticate sentry-cli](#authenticate-sentry-cli)
  - [Create a Sentry Release](#create-a-sentry-release)
  - [Update Directory.Build.props](#update-directorybuildprops)
  - [Update MauiProgram.cs](#update-mauiprogramcs)
- [Build and Deploy](#build-and-deploy)

## Project Structure

This project has been structured to mimic a typical project layout for larger MAUI apps, that may
also be part of projects with other frontends. Typically, shared logic may be broken out into
separate projects and decoupled with dependency injection:

```
 📂 src
 ├─ 📄 Directory.Build.props           # Props file used to manage properties of all projects.
 ├─ 📂 MauiSentryTest                  # Main app project file.
 │  ├─ 📄 App.xaml
 │  ├─ 📄 App.xaml.cs
 │  ├─ 📄 AppShell.xaml
 │  ├─ 📄 AppShell.xaml.cs
 │  ├─ 📄 MauiProgram.cs
 │  ├─ 📄 MauiSentryTest.App.csproj
 │  ├─ 📂 Platforms
 │  ├─ 📂 Properties
 │  ├─ 📂 Resources
 │  ├─ 📂 Services                     # Services internal to the app itself.
 │  │  └─ 📄 InternalService.cs        # An example service within the app project.
 │  ├─ 📂 ViewModels                   # ViewModels to demonstrate Sentry with MVVM pattern.
 │  │  ├─ 📄 HomePageViewModel.cs
 │  │  └─ 📄 IViewModel.cs             # Interface to register all ViewModels with DI container.
 │  └─ 📂 Views                        # MAUI views.
 │     ├─ 📄 HomePage.xaml
 │     └─ 📄 HomePage.xaml.cs
 ├─ 📂 MauiSentryTest.Common           # Example library project shared across app frameworks.
 │  ├─ 📄 MauiSentryTest.Common.csproj
 │  └─ 📂 Services                     # Services shared between multiple frameworks.
 │     ├─ 📄 CommonService.cs          # Example service in a separate project.
 │     └─ 📄 ISingletonService.cs      # Common service interface used to register DI'd services.
 └─ 📄 MauiSentryTest.sln              # App solution file.
```

In an actual app, there may be many more separated projects, or the ViewModels may be separated into
a separate project and shared between UI frameworks.

## Project Configuration

This section will detail the steps you'll need to make in order to use this app to test / evaluate
[Sentry](https://sentry.io/).

### Create a Sentry Project

Go to [Sentry](https://sentry.io/) and create a developer account if you don't have one already.
Then, click `Projects` -> `Create Project`.

Select `.NET MAUI` and configure the other options. Note down your `organisation`, `project name`,
and the `DSN`.

### Authenticate sentry-cli

Install [sentry-cli](https://docs.sentry.io/cli/installation/). Then run `sentry-cli login`. This
will guide you through making a new
[auth token](https://nathanieljs.sentry.io/settings/auth-tokens/), or allow you to enter an existing
one if you have it already.

### Create a Sentry Release

Create a [sentry release with sentry-cli](https://docs.sentry.io/cli/releases/) by running something
like `sentry-cli releases -o your-org -p your-project new "your-project@1.0.0"`, replacing
`your-org` and `your-project` with the ones created in the
[create a sentry project](#create-a-sentry-project) step.

### Update Directory.Build.props

Open the [Directory.Build.props](./src/Directory.Build.props) file, and update the `SentryOrg` and
`SentryProject` with the ones created in the [create a sentry project](#create-a-sentry-project)
step:

```xml
<!-- Configure Sentry organisation and project slugs. -->
<SentryOrg>your-org</SentryOrg>
<SentryProject>your-project</SentryProject>
```

### Update MauiProgram.cs

Open the [MauiProgram.cs] and insert the DSN from the
[create a sentry project](#create-a-sentry-project) step into the Sentry configuration:

```csharp
 var builder = MauiApp.CreateBuilder();
 builder
     .UseMauiApp<App>()
     .UseSentry(options => {
         // TODO: Add your DSN here.
         options.Dsn = "";
```

Finally, you can [build and deploy](#build-and-deploy) the app.

## Build and Deploy

You should now be able to build the app, and deploy it to Windows or Android devices as usual. iOS
will take some more work on a physical device, as you'll need to generate a provisioning profile
etc. However, if you deploy to a simulator you don't need one.

Try building the app in `Debug` and `Release`, and use the buttons in the app to throw unhandled
exceptions which should show up in Sentry.

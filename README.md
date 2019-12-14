# CSV Validator App

This app takes a CSV file and validate the data according to the validation provided to it in an xml file and generate the validated csv file at the location of the source CSV file. Both files path are configured in the app configuration file. Change the existing sample files with your own files.

## Prerequisites

In order to run this app, you need .Net CLI tools and .Net Core Runtime installed on our machine.
.Net CLI tools, .Net Core Runtime comes with the .Net Core SDK so download and install the latest version of .Net Core SDK available from this link https://dotnet.microsoft.com/download

## Installing

After downloading the .Net Core SDK installer file, just double click on it and follow the instructions with default settings.
After installation is complete, open the command window and run the following command.

```
dotnet --version
```

It will return the version number of the .Net Core.

## Getting Started

Clone or Download the zip file of this repository.
Open the command window in the folder where you have cloned or unzipped the repository.

### Running the App

Build the app by running followind command in the command window.

```
dotnet build CsvValidatorApp.sln
```

Now move to Client project folder and run the app with following commands.

```
cd CsvValidator.Client
dotnet run
```
The app will opperate on sample CSV and validation files. Change these files with your own files by updating path of these files in the app.config file.

### Running the Tests

Open the command window in the folder where the source code is unzipped and run following commands.

```
cd CsvValidator.Test
dotnet test
```

## Deployment

The best thing about developing app in .net core is that they can be deployed across the plateform.

#### Deployment for Windows

Run following command to generate the release package in the Win folder. The Win folder will contain framework dependent executables only that means The target windows plateform must have same framework(.net core runtime) installed already which is used in this app.

```
dotnet publish -c Release -o Win
```

#### Deployment for Linux

Run following command to generate the release package in the Linux folder. The Linux folder will have self contained executables for Linux environment thats means the target Linux system doesn't require anything else to run this app.

```
dotnet publish -c Release -o Linux -r linux-x64 --self-contained true
```

## Built With

- [.Net Core SDK 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) - The .Net Core framework used
- [VS Code](https://code.visualstudio.com/download) - The Code editor used

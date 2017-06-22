# Handlebars CLI
Self Contained Command Line Interface for working with handlebars templates 

Handlebars CLI takes Handlebars Templates , and the Handlebars.net Rendering Engine to create a CLI tool for rendering your templates to the console or output files 

## Usage 
You can either replace the templates in {ApplicationRoot}\Templates and {ApplicationRoot}\Templates\Views  
or you can privide your own directories at the commandline 

| Short       | Long            | Default                       | Description                                                       |
|-------------|-----------------|-------------------------------|-------------------------------------------------------------------|
| -d DATA     | --data=DATA     |                               | Json Input for binding template, supports json only at this time  |
| -b Path     | --basedir=Path  | CWD                           | Base directory to load templates from                             |
| -v PATH,    | --views=PATH    | {{TemplateDirectory}}\Views   | Directory to find view templates                                  |
| -i,         | --input=PATH    |                               | Input data file, supports json only at this time                  |
| -v,         | --verbose       | false                         | Display detailed output                                           |
| -o,         | --output        | Console.Out                   | File path to output rendered text                                 |
| -h          | --help          |                               | Display this help screen.                                         |
|             | --version       |                               | Display version information.                                      |
| pos[0] NAME |                 | Required                      | Template Name to use for output, ie pscomment.hbs                 |  

## Syntax 

### Templates 
>For Handlebars Syntax support please see the handlebars website [Here](http://handlebarsjs.com)

### Plugins 
Nuget Packaging is coming for the pugins sdk soon!

To create an external plugin create 
1. Create a class library project 
2. Install the Digitalparadox.HandlebarsCli.Plugins package 
3. Implement the IHandlebarsPlugin interface 
```csharp
public class MyPlugin : HandlebarsCli.Plugins.IHandlebarsHelper
{
    public string Name { get; set; } = "HelloWorldHelperExample";
    public HelperType Type { get; set; } = HelperType.Inline;

    public string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args)
    {
        return "Hello World!!";
    }
}
```

4. Build and add .dll to .\Plugins Directory 

5. Profit


## Roadmap 

### 1.0  
* Fix Existing Bugs 
* Stabilize /optimize  rendering engine
* Build msi installer
* Add Chocolatey Support 

### 2.0 
* Add Support for writing helpers in .cs & .csx
* Add Support for directory initialization / project creation 
* Add Support for adding views / templates / partials via command line 
* Helper installation support, Likely nuget based 

## Contributing 
I am open to contributions from the community 

if you would like to see a feature added fork me and submit a pull request or open an issue 

if you have built a plugin and would like to see it listed here open an issue with the title Add Plugin {Name} to listing  







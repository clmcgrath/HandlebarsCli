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

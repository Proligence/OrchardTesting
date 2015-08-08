# Proligence.Orchard.Testing
Proligence.Orchard.Testing is a library which aids in unit testing Orchard modules by providing implementation of mocks for many components commonly used in Orchard.

The library supports two unit testing frameworks - [NUnit](http://www.nunit.org/) and [XUnit](http://xunit.github.io/). Mocking functionality depends on the [Moq](https://github.com/Moq/moq4) library.

# Getting Started

To best way to use Proligence.Orchard.Testing library in your Orchard projects is to check out the source code to some directory and map that directory to your Orchard source code directory using a hard link. This can be easily done using the [MapToOrchard.cmd](MapToOrchard.cmd) script. After cloning the repository for Proligence.Orchard.Testing, execute the script from cmd.exe and specify the path to the root directory of your Orchard project.

For example, let's say that Proligence.Orchard.Testing is located in `C:\Projects\OrchardTesting` and your Orchard solution is located in `C:\Projects\Orchard`. Just open `cmd.exe` and run:

    cd C:\Projects\OrchardTesting
    MapToOrchard.cmd C:\Projects\Orchard
  
This will create a hardlink in C:\Projects\Orchard\src\Tools\Proligence.Orchard.Testing pointing to C:\Projects\OrchardTesting\Proligence.Orchard.Testing. Next, you need to add the Proligence.Orchard.Testing project to your Orchard solution. Add `Proligence.Orchard.Testing.NUnit.csproj` if you are using NUNit or add `Proligence.Orchard.Testing.XUnit.csproj` if you are using XUnit.

## Mocking the Content Definition Manager and Content Manager

TODO

## Mocking ILogger

TODO

## Mocking INotifier

TODO

## Mocking IAuthorizer

TODO

## Mocking Orchard Services

TODO

## Mocking the Transaction Manager

TODO

## Mocking Shapes

TODO

## Testing Content Drivers

TODO

## Testing Content Handlers

TODO

## Testing Tokens

TODO

## Creating Mock Content Items

TODO

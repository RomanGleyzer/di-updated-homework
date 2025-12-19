using Autofac;
using CommandLine;
using System.Drawing;
using TagsCloud.ConsoleApp;
using TagsCloud.Core.Cloud.Interfaces;

Parser.Default.ParseArguments<Options>(args).MapResult(Run, _ => 1);

static int Run(Options o)
{
    var container = CompositionRoot.Build(o);

    var generator = container.Resolve<IMultiCloudGenerator>();
    var imageSize = new Size(o.Width, o.Height);

    generator.GenerateAll(imageSize, name =>
    {
        var path = o.OutputPattern.Replace("{name}", name);
        return File.Create(path);
    });

    return 0;
}
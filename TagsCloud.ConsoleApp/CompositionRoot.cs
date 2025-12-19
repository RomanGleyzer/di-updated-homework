using Autofac;
using System.Drawing;
using TagsCloud.Core.Cloud;
using TagsCloud.Core.Cloud.Fonts;
using TagsCloud.Core.Cloud.Interfaces;
using TagsCloud.Core.Cloud.Rendering;
using TagsCloud.Core.Layouters;
using TagsCloud.Core.Words;
using TagsCloud.Core.Words.Interfaces;
using TagsCloud.Core.Words.Steps;

namespace TagsCloud.ConsoleApp;

public static class CompositionRoot
{
    public static IContainer Build(Options o)
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(new FileWordsSource(o.InputPath)).As<IWordsSource>();

        builder.Register(c =>
        {
            return o.BoringWordsPath is null
                ? (IBoringWordsProvider)new RussianBoringWordsProvider()
                : new FileBoringWordsProvider(o.BoringWordsPath);
        }).As<IBoringWordsProvider>().SingleInstance();

        builder.RegisterType<LowercaseStep>().As<IWordProcessingStep>();
        builder.RegisterType<BoringWordsFilterStep>().As<IWordProcessingStep>();

        builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();

        builder.Register(ctx =>
        {
            return new LinearFontSizeScaler(1, 1000, o.MinFont, o.MaxFont);
        }).As<IFontSizeScaler>();

        builder.Register(ctx => new SystemDrawingTextMeasurer(o.FontName, ctx.Resolve<IFontSizeScaler>())).As<IFontSizeMeasurer>();

        builder.RegisterInstance(PaletteParser.Parse(o.Colors)).As<Color[]>();

        builder.RegisterType<FrequencyColorSelector>().As<ITagColorSelector>();
        builder.RegisterType<CenteringOffsetCalculator>().As<ITagsOffsetCalculator>();

        builder.Register(ctx => new SystemDrawingPngCloudRenderer(
                o.FontName,
                ctx.Resolve<IFontSizeScaler>(),
                ctx.Resolve<Color[]>(),
                ctx.Resolve<ITagColorSelector>(),
                ctx.Resolve<ITagsOffsetCalculator>()))
            .As<ICloudRenderer>();

        builder.RegisterType<CircularCenterLayouterFactory>().As<ICloudLayouterFactory>();

        builder.RegisterType<MultiCloudGenerator>().As<IMultiCloudGenerator>();

        return builder.Build();
    }
}

using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharpIssue.Models;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SkiaSharpIssue.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
        public Action RefreshViewAction { get; set; }

        public void Paint(Assembly assembly, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.White);
            float svgMaxWidth = 242;
            float svgMaxHeight = 310;
            float scaleX = (info.Width / svgMaxWidth) * 0.90f;
            float scaleY = (info.Height / svgMaxHeight) * 0.90f;
            float ratio = Math.Min(scaleX, scaleY);
            canvas.Translate(30, -45);
            // Drawing working svg
            var buffer = Resources.working;
            var xml = Encoding.UTF8.GetString(buffer);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var node = xmlDoc.GetElementsByTagName("a:svg")[0];
            var monitoringGraphicsSVG = new MonitoringGraphicsSVG(node, 116.967F, 164.981F, 0, 0);
            int svgFrameIndex = 0;
            var svgXPosition = (16F + monitoringGraphicsSVG.GetFrameXValue(svgFrameIndex) + monitoringGraphicsSVG.GetXValue()) * ratio;
            var svgYPosition = info.Height - (243F - monitoringGraphicsSVG.GetFrameYValue(svgFrameIndex) - monitoringGraphicsSVG.GetYValue() + 45) * ratio;
            var stream = monitoringGraphicsSVG.GetStream(svgFrameIndex: svgFrameIndex);
            SkiaSharp.Extended.Svg.SKSvg svg = new SkiaSharp.Extended.Svg.SKSvg();
            svg.Load(stream);
            var matrix = SKMatrix.CreateScaleTranslation(ratio, ratio, svgXPosition, svgYPosition);
            canvas.DrawPicture(svg.Picture, ref matrix);
            // Drawing not working svg
            buffer = Resources.not_working;
            xml = Encoding.UTF8.GetString(buffer);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            node = xmlDoc.GetElementsByTagName("a:svg")[0];
            monitoringGraphicsSVG = new MonitoringGraphicsSVG(node, 151.956F, 197.611F, 0, 0);
            svgFrameIndex = 0;
            svgXPosition = (17F + monitoringGraphicsSVG.GetFrameXValue(svgFrameIndex) + monitoringGraphicsSVG.GetXValue()) * ratio;
            svgYPosition = info.Height - (225F - monitoringGraphicsSVG.GetFrameYValue(svgFrameIndex) - monitoringGraphicsSVG.GetYValue() + 45) * ratio;
            stream = monitoringGraphicsSVG.GetStream(svgFrameIndex: svgFrameIndex);
            svg = new SkiaSharp.Extended.Svg.SKSvg();
            svg.Load(stream);
            matrix = SKMatrix.CreateScaleTranslation(ratio, ratio, svgXPosition, svgYPosition);
            canvas.DrawPicture(svg.Picture, ref matrix);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Xamarin.Forms;

namespace SkiaSharpIssue.Models
{
    internal class MonitoringGraphicsSVG
    {
        private XmlNode _frames;
        private float _width;
        private float _height;
        private float _x;
        private float _y;

        public MonitoringGraphicsSVG(XmlNode frames, float width, float height, float x, float y)
        {
            _frames = frames;
            _width = width;
            _height = height;
            _x = x;
            _y = y;
        }

        public XmlNode GetFrame(int index)
        {
            return _frames.ChildNodes[index];
        }

        public float GetFrameXValue(int svgFrameIndex)
        {
            XmlNode frame = GetFrame(svgFrameIndex);
            float x = 0;
            if (frame.Attributes["x"] != null)
            {
                var isXValueParsable = float.TryParse(frame.Attributes["x"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out x);
                if (isXValueParsable)
                    return x;
            }
            return x;
        }

        public float GetFrameYValue(int svgFrameIndex)
        {
            XmlNode frame = GetFrame(svgFrameIndex);
            float y = 0;
            if (frame.Attributes["y"] != null)
            {
                var isYValueParsable = float.TryParse(frame.Attributes["y"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out y);
                if (isYValueParsable)
                    return y;
            }
            return y;
        }

        public Stream GetStream(int svgFrameIndex)
        {
            try
            {
                XmlNode frame = GetFrame(svgFrameIndex);
                string frameString = frame.InnerXml.ToString();
                try
                {
                    frameString = Regex.Replace(frameString, "(<a:svg.*?>)|(</a:svg>)", "");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get stream for svg element:" + ex.Message);
                }
                string frameStringWithStartRoot = frameString.Insert(0, "<root>");
                string frameStringWithEndRoot = frameStringWithStartRoot.Insert(frameStringWithStartRoot.Length, "</root>");
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(frameStringWithEndRoot);
                writer.Flush();
                stream.Position = 0;
                return stream;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stream for svg element:" + ex.Message);
            }
        }
        public float GetHeight()
        {
            return _height;
        }

        public float GetWidth()
        {
            return _width;
        }

        public float GetXValue()
        {
            return _x;
        }

        public float GetYValue()
        {
            return _y;
        }
    }
}

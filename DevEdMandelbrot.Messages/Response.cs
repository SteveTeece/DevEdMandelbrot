using System;

namespace DevEdMandelbrot.Messages
{
    public class Response
    {
        public Guid CorrelationId;

        /// <summary>
        /// The binary data of the bitmap you rendered. See the ImageConverter class, function ConvertTo.
        /// </summary>
        public byte[] ImageData;

        /// <summary>
        /// Who rendered this?
        /// </summary>
        public string ServerName;

        /// <summary>
        /// How long did it take you to render this?
        /// </summary>
        public long RenderTimeMs;
    }
}

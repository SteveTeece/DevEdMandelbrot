using System;
using System.Drawing;

namespace DevEdMandelbrot.Messages
{
    public class Request
    {
        public Guid CorrelationId;

        /// <summary>
        /// The bounding box of the mandelbrot space
        /// </summary>
        public Viewport Viewport;

        /// <summary>
        /// The region of the bitmap required to be rendered
        /// </summary>
        public Rectangle RenderArea;

        /// <summary>
        /// The width of the full bitmap
        /// </summary>
        public int TotalWidth;

        /// <summary>
        /// The height of the full bitmap
        /// </summary>
        public int TotalHeight;
    }

    public class Viewport
    {
        public double StartX;
        public double StartY;
        public double EndX;
        public double EndY;
    }
}

#region User/License
// Copyright (c) 2005-2013 tfwroble
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Drawing.Drawing2D;

/* User: oIo * Date: 9/21/2010 * Time: 10:22 AM */

namespace System.Drawing
{
	public class RoundURectRenderer
	{
		FloatRectCorners corners;
		public FloatRectCorners Corners { get { return corners; } set { corners = value; } }
		
		RectangleDoubleUnit rectangle;
		public RectangleF RoundRect { get { return rectangle; } set { rectangle = value; } }
		//		RoundEdges edges = RoundEdges.All;
		
		#region Point Positions
		UPointD PMidTopLeft { get { return new UPointD(RoundRect.X,RoundRect.Top+corners.TopLeft); } }
		UPointD PTopTopLeft { get { return new UPointD(RoundRect.X+corners.TopLeft,RoundRect.Top); } }
		UPointD cPMidTopLeft { get { return new UPointD(RoundRect.X,RoundRect.Top+(corners.TopLeft*0.5f)); } }
		UPointD cPTopTopLeft { get { return new UPointD(RoundRect.X+(corners.TopLeft*0.5f),RoundRect.Top); } }
		
		UPointD PTopTopRight { get { return new UPointD(RoundRect.Width-corners.TopRight,RoundRect.Top); } }
		UPointD PMidTopRight { get { return new UPointD(RoundRect.Width,RoundRect.Top+corners.TopRight); } }
		UPointD cPTopTopRight { get { return new UPointD(RoundRect.Width-(corners.TopRight*0.5f),RoundRect.Top); } }
		UPointD cPMidTopRight { get { return new UPointD(RoundRect.Width,RoundRect.Top+(corners.TopRight*0.5f)); } }
		
		UPointD PMidBtmRight { get { return new UPointD(RoundRect.Width,RoundRect.Bottom-corners.BottomRight); } }
		UPointD PBtmBtmRight { get { return new UPointD(RoundRect.Width-corners.BottomRight,RoundRect.Bottom); } }
		UPointD cPMidBtmRight { get { return new UPointD(RoundRect.Width,RoundRect.Bottom-(corners.BottomRight*0.5f)); } }
		UPointD cPBtmBtmRight { get { return new UPointD(RoundRect.Width-(corners.BottomRight*0.5f),RoundRect.Bottom); } }
		
		UPointD PBtmBtmLeft { get { return new UPointD(RoundRect.X+corners.BottomLeft,RoundRect.Bottom); } }
		UPointD PMidBtmLeft { get { return new UPointD(RoundRect.X,RoundRect.Bottom-corners.BottomLeft); } }
		UPointD cPBtmBtmLeft { get { return new UPointD(RoundRect.X+(corners.BottomLeft*0.5f),RoundRect.Bottom); } }
		UPointD cPMidBtmLeft { get { return new UPointD(RoundRect.X,RoundRect.Bottom-(corners.BottomLeft*0.5f)); } }
		float tension = 0.5f;
		
		PointF[] cTopLeft { get { return new PointF[]{PMidTopLeft.FPoint,cPMidTopLeft.FPoint,cPTopTopLeft.FPoint,PTopTopLeft.FPoint}; } }
		PointF[] cTopRight { get { return new PointF[]{PTopTopRight.FPoint,cPTopTopRight.FPoint,cPMidTopRight.FPoint,PMidTopRight.FPoint}; } }
		PointF[] cBtmRight { get { return new PointF[]{PMidBtmRight.FPoint,cPMidBtmRight.FPoint,cPBtmBtmRight.FPoint,PBtmBtmRight.FPoint}; } }
		PointF[] cBtmLeft { get { return new PointF[]{PBtmBtmLeft.FPoint,cPBtmBtmLeft.FPoint,cPMidBtmLeft.FPoint,PMidBtmLeft.FPoint}; } }
		#endregion
		#region GraphicsPath
		GraphicsPath basePath;
		public GraphicsPath BasePath { get { return basePath; } set { basePath = value; } }
		public GraphicsPath CurvePath
		{
			get
			{
				GraphicsPath gp = EmptyPath;
				gp.AddCurve(cTopLeft,1,1,tension);
				gp.AddLine(PTopTopLeft.FPoint,PTopTopRight.FPoint);
				gp.AddCurve(cTopRight,1,1,tension);
				gp.AddLine(PMidTopRight.FPoint,PMidBtmRight.FPoint);
				gp.AddCurve(cBtmRight,1,1,tension);
				gp.AddLine(PBtmBtmRight.FPoint,PBtmBtmLeft.FPoint);
				gp.AddCurve(cBtmLeft,1,1,tension);
				gp.AddLine(PMidBtmLeft.FPoint,PMidTopLeft.FPoint);
				return gp;
			}
		}
		#region Obsolete Comments
		//		internal bool HasTopLeft { get { return CheckEnum(RoundEdges.TopLeft); } }
		//		internal bool HasTopRight { get { return CheckEnum(RoundEdges.TopRight); } }
		//		internal bool HasBottomLeft { get { return CheckEnum(RoundEdges.BottomLeft); } }
		//		internal bool HasBottomRight { get { return CheckEnum(RoundEdges.BottomRight); } }
		//		public bool CheckEnum(RoundEdges value) { return Enum.IsDefined(edges.GetType(),value); }
		#endregion
		#endregion

		public GraphicsPath Path
		{
			get
			{
				GraphicsPath gp = EmptyPath;
				gp.AddBezier(cTopLeft[0],cTopLeft[1],cTopLeft[2],cTopLeft[3]);
				gp.AddLine(PTopTopLeft.FPoint,PTopTopRight.FPoint);
				gp.AddBezier(cTopRight[0],cTopRight[1],cTopRight[2],cTopRight[3]);
				gp.AddLine(PMidTopRight.FPoint,PMidBtmRight.FPoint);
				gp.AddBezier(cBtmRight[0],cBtmRight[1],cBtmRight[2],cBtmRight[3]);
				gp.AddLine(PBtmBtmRight.FPoint,PBtmBtmLeft.FPoint);
				gp.AddBezier(cBtmLeft[0],cBtmLeft[1],cBtmLeft[2],cBtmLeft[3]);
				gp.AddLine(PMidBtmLeft.FPoint,PMidTopLeft.FPoint);
				return gp;
			}
		}
	
		public RoundURectRenderer(RectangleDoubleUnit rect, FloatRectCorners radii, float tens)
		{
			rectangle = rect;
			corners = radii;
			tension = tens;
		}
		static protected GraphicsPath EmptyPath { get { return new GraphicsPath(); } }
	}
}

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
using System.Windows.Forms;

/* User: oIo * Date: 9/21/2010 * Time: 10:22 AM */

namespace System.Drawing
{
	/// <summary>
	/// Something here is very off.
	/// <para>• fact here is that points are all indexed by ‘this[int Index]’ where you can also set values which will never be used!</para>
	/// <para>• note that any Point reference is generated as it is called.</para>
	/// <para>• only corner values are saved (during component serialization)</para>
	/// </summary>
	public class RoundRectRenderer
	{
		static public void Fill(RoundRectRenderer roundrect, Graphics g, Brush fill1)
		{
			using (GraphicsPath path = roundrect.Path)
			{
				g.FillPath(fill1,path);
			}
		}
		static public void Stroke(RoundRectRenderer roundrect, Graphics g, Pen pen)
		{
			using (GraphicsPath path = roundrect.Path)
			{
				g.DrawPath(pen,path);
			}
		}
		
		#region Gradient 1, 2 Padding (none is used)
		Padding paddG1,paddG2;
		public Padding Gradient1Padding
		{
			get { return paddG1; } set { paddG1 = value; }
		}
		public Padding Gradient2Padding
		{
			get { return paddG2; } set { paddG2 = value; }
		}
		#endregion

		#region Corners
		CORNERS corners;
		public CORNERS Corners { get { return corners; } set { corners = value; } }
		#endregion
		#region Rect
		FloatRect rectangle;
		public RectangleF RoundRect { get { return rectangle; } set { rectangle = value; } }
		#endregion
		#region Tension
		//RoundEdges edges = RoundEdges.All;
		float tension = 0.5f;
		#endregion
		#region ENUM
		[Flags] public enum POINTS
		{
			EdgePoint	=0x0001,
			CornerPoint	=0x0002,
			Top			=0x0010,
			Right		=0x0020,
			Bottom		=0x0040,
			Left		=0x0080,
			Middle		=0x0100,
			Center		=0x0200,
			
			TopLeft = EdgePoint|Top|Left,				TopCenter=EdgePoint|Top|Center,				TopRight=EdgePoint|Top|Right,
			MiddleLeft=EdgePoint|Left|Middle,			MiddleCenter=EdgePoint|Middle|Center,		MiddleRight=EdgePoint|Middle|Right,
			BottomLeft=EdgePoint|Bottom|Left,			BottomCenter=EdgePoint|Bottom|Center,		BottomRight = EdgePoint|Bottom|Right,
			
			CornerMiddleTopLeft		= CornerPoint|TopLeft|Middle,
			CornerTopLeft			=CornerPoint|TopLeft,
			CornerTopRight			=CornerPoint|TopRight,
			CornerMiddleTopRight	=CornerPoint|TopRight|Middle,
			CornerMiddleBottomRight	=CornerPoint|Middle|Right,
			CornerBottomRight		=CornerPoint|Bottom|Left,
			CornerBottomLeft		=CornerPoint|Bottom|Center,
			CornerMiddleBottomLeft	=CornerPoint|Bottom|Right,
		}
		#endregion
		#region Property: Points
		// not super but done
		public PointF[] Points { get { return new PointF[]{this[0],this[1],this[2],this[3],this[4],this[5],this[6],this[7],this[8],this[9],this[10],this[11],this[12],this[13],this[14],this[15]}; } }
		public FloatPoint this[int Index]
		{
			get
			{
				switch (Index)
				{
						case 0: return new FloatPoint( RoundRect.X, RoundRect.Top+corners.TopLeft );
						case 1: return new FloatPoint( RoundRect.X+corners.TopLeft,RoundRect.Top );
						case 2: return new FloatPoint( RoundRect.Width-Corners.TopRight,RoundRect.Y );
						case 3: return new FloatPoint( RoundRect.Width,RoundRect.Y+Corners.TopRight );
						case 4: return new FloatPoint( RoundRect.Width,RoundRect.Height-Corners.BottomRight );
						case 5: return new FloatPoint( RoundRect.Width-Corners.BottomRight,RoundRect.Height );
						case 6: return new FloatPoint( RoundRect.X+Corners.BottomLeft,RoundRect.Height );
						case 7: return new FloatPoint( RoundRect.X,RoundRect.Height-Corners.BottomLeft );
						//
						case 8: return new FloatPoint( RoundRect.X,RoundRect.Y+(corners.TopLeft*0.5f) );//cPMidTopLeft
						case 9: return new FloatPoint( RoundRect.X+(corners.TopLeft*0.5f),RoundRect.Top );//cPTopTopLeft
						case 10: return new FloatPoint( RoundRect.Width-(corners.TopRight*0.5f),RoundRect.Y );
						case 11: return new FloatPoint( RoundRect.Width,RoundRect.Y+(corners.TopRight*0.5f) );
						case 12: return new FloatPoint( RoundRect.Width,RoundRect.Height-(corners.BottomRight*0.5f) );
						case 13: return new FloatPoint( RoundRect.Width-(corners.BottomRight*0.5f),RoundRect.Height );
						case 14: return new FloatPoint( RoundRect.X+(corners.BottomLeft*0.5f),RoundRect.Height );
						case 15: return new FloatPoint( RoundRect.X,RoundRect.Height-(corners.BottomLeft*0.5f) );
						default : throw new ArgumentOutOfRangeException();
				}
				
			}
			set
			{
				
				switch (Index)
				{
						case 0: this[0] = value; break;
						case 1: this[1] = value; break;
						case 2: this[2] = value; break;
						case 3: this[3] = value; break;
						case 4: this[4] = value; break;
						case 5: this[5] = value; break;
						case 6: this[6] = value; break;
						case 7: this[7] = value; break;
//						//
//						case 8:
//						case 9:
//						case 10:
//						case 11:
//						case 12:
//						case 13: S
//						case 14:
//						case 15:
						default : throw new ArgumentOutOfRangeException();
				}
			}
		}
		public PointF this[POINTS Key]
		{
			get
			{
				switch (Key) {
						case POINTS.TopLeft: return this[0];
						case POINTS.TopCenter: return this[1];
						case POINTS.TopRight: return this[2];
						case POINTS.MiddleLeft: return this[3];
						case POINTS.MiddleCenter: return this[4];
						case POINTS.MiddleRight: return this[5];
						case POINTS.BottomLeft: return this[6];
						case POINTS.BottomCenter: return this[7];
						
						case POINTS.CornerMiddleTopLeft: return this[8];
						case POINTS.CornerTopLeft: return this[9];
						case POINTS.CornerTopRight: return this[10];
						case POINTS.CornerMiddleTopRight: return this[11];
						case POINTS.CornerMiddleBottomRight: return this[12];
						case POINTS.CornerBottomRight: return this[13];
						case POINTS.CornerBottomLeft: return this[14];
						case POINTS.CornerMiddleBottomLeft: return this[15];
						default: throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				switch (Key) {
						case POINTS.TopLeft: this[0] = value; break;
						case POINTS.TopCenter: this[1] = value; break;
						case POINTS.TopRight: this[2] = value; break;
						case POINTS.MiddleLeft: this[3] = value; break;
						case POINTS.MiddleCenter: this[4] = value; break;
						case POINTS.MiddleRight: this[5] = value; break;
						case POINTS.BottomLeft: this[6] = value; break;
						case POINTS.BottomCenter: this[7] = value; break;
						default: throw new ArgumentOutOfRangeException();
				}
			}
		}
		#endregion
		#region Point
		// midtopleft
		FloatPoint PointMiddleTopLeft		{ get { return this[0]; } set { this[0]=value; } }
		FloatPoint PointTopLeft				{ get { return this[1]; } set { this[1]=value; } }
		FloatPoint PointTopRight			{ get { return this[2]; } set { this[2]=value; } }
		FloatPoint PointMiddleTopRight		{ get { return this[3]; } set { this[3]=value; } }
		FloatPoint PointMiddleBottomRight	{ get { return this[4]; } set { this[4]=value; } }
		FloatPoint PointBottomRight			{ get { return this[5]; } set { this[5]=value; } }
		FloatPoint PointBottomLeft			{ get { return this[6]; } set { this[6]=value; } }
		FloatPoint PointMiddleBottomLeft	{ get { return this[7]; } set { this[7]=value; } }
		#endregion
		#region CornerPoint
		FloatPoint CornerMiddleTopLeft		{ get { return  this[8]; } /*set {  this[8]=value; }*/ }
		FloatPoint CornerTopLeft			{ get { return  this[9]; } /*set {  this[9]=value; }*/ }
		FloatPoint CornerTopRight			{ get { return this[10]; } /*set { this[10]=value; }*/ }
		FloatPoint CornerMiddleTopRight		{ get { return this[11]; } /*set { this[11]=value; }*/ }
		FloatPoint CornerMiddleBottomRight	{ get { return this[12]; } /*set { this[12]=value; }*/ }
		FloatPoint CornerBottomRight		{ get { return this[13]; } /*set { this[13]=value; }*/ }
		FloatPoint CornerBottomLeft			{ get { return this[14]; } /*set { this[14]=value; }*/ }
		FloatPoint CornerMiddleBottomLeft	{ get { return this[15]; } /*set { this[15]=value; }*/ }
		#endregion
		
		#region Corners (PointF[])
		PointF[] PathTopLeft { get { return new PointF[]{PointMiddleTopLeft,CornerMiddleTopLeft,CornerTopLeft,PointTopLeft}; } }
		PointF[] PathTopRight { get { return new PointF[]{PointTopRight,CornerTopRight,CornerMiddleTopRight,PointMiddleTopRight}; } }
		PointF[] PathBottomRight { get { return new PointF[]{PointMiddleBottomRight,CornerMiddleBottomRight,CornerBottomRight,PointBottomRight}; } }
		PointF[] PathBottomLeft { get { return new PointF[]{PointBottomLeft,CornerBottomLeft,CornerMiddleBottomLeft,PointMiddleBottomLeft}; } }
		#endregion

		#region GraphicsPath
		GraphicsPath basePath;
		public GraphicsPath BasePath { get { return basePath; } set { basePath = value; } }
		public GraphicsPath CurvePath
		{
			get
			{
				GraphicsPath gp = EmptyPath;
				gp.AddCurve(PathTopLeft,1,1,tension);
				gp.AddLine(PointTopLeft,PointTopRight);
				gp.AddCurve(PathTopRight,1,1,tension);
				gp.AddLine(PointMiddleTopRight,PointMiddleBottomRight);
				gp.AddCurve(PathBottomRight,1,1,tension);
				gp.AddLine(PointBottomRight,PointBottomLeft);
				gp.AddCurve(PathBottomLeft,1,1,tension);
				gp.AddLine(PointMiddleBottomLeft,PointMiddleTopLeft);
				return gp;
			}
		}
		#endregion

		/// <summary>
		/// Creates no more then the empty GraphicsPath.
		/// </summary>
		static protected GraphicsPath EmptyPath { get { return new GraphicsPath(); } }

		/// <summary>
		/// Provide Bezier path
		/// </summary>
		public GraphicsPath Path
		{
			get
			{
				GraphicsPath gp = EmptyPath;
				gp.AddBezier(PointMiddleTopLeft,CornerMiddleTopLeft,CornerTopLeft,PointTopLeft);
				gp.AddLine(PointTopLeft,PointTopRight);
				gp.AddBezier(PointTopRight,CornerTopRight,CornerMiddleTopRight,PointMiddleTopRight);
				gp.AddLine(PointMiddleTopRight,PointMiddleBottomRight);
				gp.AddBezier(PointMiddleBottomRight,CornerMiddleBottomRight,CornerBottomRight,PointBottomRight);
				gp.AddLine(PointBottomRight,PointBottomLeft);
				gp.AddBezier(PointBottomLeft,CornerBottomLeft,CornerMiddleBottomLeft,PointMiddleBottomLeft);
				gp.AddLine(PointMiddleBottomLeft,PointMiddleTopLeft);
				return gp;
			}
		}

		public RoundRectRenderer(FloatRect rect, CORNERS radii, float tens)
		{
			rectangle = rect;
			corners = radii;
			tension = tens;
		}
		public RoundRectRenderer(FloatRect rect, float radii, float tens)
		{
			rectangle = rect;
			corners = new CORNERS(radii);
			tension = tens;
		}
	}
}

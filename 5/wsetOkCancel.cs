#pragma warning disable 219

//
#define PANEL
//#define DEBUG
//#define LAYOUT
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms; 
using System.Text.RegularExpressions; 


namespace ut
{

  public class OkCancel : System.Windows.Forms.Form
  {
    const string _nm = "OkCancel";
    public TableLayoutPanel tPL;
    Padding  _pd ;
    public int _h = ut.SZ.X_FLD;                // the second column
                                                // label:      field
    public int _v = ut.SZ.Y_FLD;                // it is the next row
    public _Button OK_but;  //ok
    public _Button ESC_but;  //cancel
    public System.Drawing.Size bSz;
    public System.Drawing.Size bSz2;

    protected int     xSize = 0; // эти поля видят наследники
    protected int     ySize = 0; // в табуляциях
    protected         SizeF sizef;

//    protected System.ComponentModel.Container components = null;
    public static int xtab (int i){
      return    SZ.X_SPC*2 +(3+ SZ.X_BUTTON) *i ;
    }
    public static int ytab (int i){
      return    SZ.Y_SPC*2  +(3+ SZ.Y) * i;
    }



    public OkCancel(string q = "question for OkCancel window", int www=1){
//    
      Text        = q;

      MinimizeBox = false;
      MaximizeBox = false;
      ControlBox = false;
      AutoScroll  = false;
  //
      FormBorderStyle = FormBorderStyle.FixedDialog;
//
      AutoSize = true;
//      _pd =  new Padding(SZ.X_SPC*3, SZ.Y_SPC*3,SZ.X_SPC*3, SZ.Y_SPC*3);
      _pd =  new Padding(3, 3,3, 3);
      Padding  = _pd;

//

//      _pd =  new Padding(1);
      Graphics g = CreateGraphics();
      StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
      sizef = g.MeasureString(q, this.Font, Int32.MaxValue, sf);


      DialogResult = DialogResult.Cancel;

      bSz = new  System.Drawing.Size(  ut.SZ.X_BUTTON, ut.SZ.Y);
      bSz2 = new  System.Drawing.Size(  ut.SZ.X_BUTTON*2, ut.SZ.Y);
///// 
  Size    = bSz;
 
      OK_but = new _Button();
      OK_but.Name = "OK_but";
      OK_but.Text = CNST.BTTN_OK;
      OK_but.DialogResult = DialogResult.OK;
      OK_but.Click    += new System.EventHandler  (_OK_but);
      OK_but.Size = bSz;

      ESC_but = new _Button();
      ESC_but.Name     = "ESC_but";
      ESC_but.Size     = bSz;
      ESC_but.Text     = CNST.BTTN_ESC;
      ESC_but.DialogResult = DialogResult.Cancel;
    ///  
#if LAYOUT
      tPL = new TableLayoutPanel();
      tPL.ColumnCount = 4;  // две колонки;
      tPL.RowCount  = 1;
      tPL.Padding  = _pd;
      tPL.CellBorderStyle =  TableLayoutPanelCellBorderStyle.Inset;
      tPL.AutoSize = true;
#endif
  //

      ////if (Size.Width < sizef.Width+SZ.X_SPC*2 )
      ////  ClientSize =  new Size((int)(sizef.Width+SZ.X_SPC*2), Size.Height);
     ///// this.MinimumSize = Size;

   /// 
//
    this.AcceptButton = OK_but;       // нажатие ентера как на ок
 // 
    this.CancelButton = ESC_but;        //нажатие esc
//      ESC_but.Dock = DockStyle.Fill;
//      ESC_but.Dock = DockStyle.Left;
                                // x , y
//      OK_but.Dock = DockStyle.Left;
  //
#if PANEL
//    Button p =new Button();
//    Button p0 =new Button();
    Panel p0 =new Panel();
    Panel p =new Panel();
#if DEBUG
    Panel p1 =new Panel();
    Panel p2 =new Panel();
    Panel p3 =new Panel();
    p.BorderStyle = BorderStyle.FixedSingle;
    p2.BorderStyle = BorderStyle.FixedSingle;
#endif
#else 
    Button p =new Button();
    Button p1 = new Button();
    Button p0 = new Button();
    Button p2 = new Button();
    Button p3 = new Button();
#endif


      p0.Text = "p0";
      p0.Size = bSz2;
      p0.Dock   = DockStyle.Top;
 //     Panel p = new Panel ();
      p.Text = "p";
  //  p.BorderStyle = BorderStyle.None;
      p.Size = new Size (bSz.Width*www, bSz.Height) ;
  //    p.Size = new Size ( bSz.Width*2, bSz.Height);
   
//      p.Dock   = DockStyle.Top;
      p.Dock   = DockStyle.Right;;

#if DEBUG
      p1.Text = "p1";
      p1.Size = bSz;
      p1.Dock   = DockStyle.Top;
      p2.Text = "p2";
      p2.Size = bSz;
      p2.Dock   = DockStyle.Top;
      p3.Text = "p3";
      p3.Size = bSz;
      p3.Dock   = DockStyle.Top;
#endif
  //    p1.Dock   = DockStyle.Bottom;
#if LAYOUT
      tPL.Controls.Add (OK_but, 0,0);
      tPL.Controls.Add (p, 1,0);
//
      tPL.Controls.Add (p1, 2,0);
      tPL.Controls.Add (ESC_but, 3, 0);


      Controls.Add (tPL);
      ClientSize =  tPL.Size;
//      OK_but.Size = bSz;
//      ESC_but.Size     = bSz;
//
      tPL.Size = new Size ( tPL.Size.Width, ytab(ySize = 1)+ SZ.Y_SPC*2);
#else
   // 
   //  Controls.Add (p1);
    //
      Controls.Add (p0);

      OK_but.Dock   = DockStyle.Right;

      Controls.Add (OK_but);
   //
      Controls.Add (p);

      ESC_but.Dock   = DockStyle.Right;
      Controls.Add (ESC_but);
//      p2.Size = new Size ( p2.Size.Width*20, p2.Size.Height);
#if DEBUG
      Controls.Add (p1);
      Controls.Add (p2);
      Controls.Add (p3);
#endif

 #endif
     
      

      

      Location  =  new System.Drawing.Point(ut.SZ.X_SPC, ut.SZ.Y_SPC);
    }



   private void 
   _OK_but (object sender, System.EventArgs e)
   {
#if DEBUG
       Console.WriteLine ("OkCancel._OK_but: \n");
#endif

   }                           

  }
}

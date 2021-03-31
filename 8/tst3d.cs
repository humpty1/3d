using System;     
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using ut;
using Logger;

#if DEBUG
#endif

namespace tst3d
{

      struct sgmDouble {
         public double min;
         public double max;
         public  sgmDouble (double mi, double ma) {
              min = mi; max = ma;
         }
         public  sgmDouble (int i) {
              min = double.MaxValue; max = double.MinValue;
         }
      } 
      struct sgmInt {
         public int min;
         public int max;
         public  sgmInt (int mi, int ma) {
              min = mi; max = ma;
         }
      } 

      ///  отображение  оси координат из реального мира в пиксели
      struct mapping {
         public sgmDouble  i;    // входной отрезок, реальный мир	
         public sgmInt     o;    // выходной отрезок, пиксели на экране
         public mapping ( sgmInt oo): this(oo, new sgmDouble(1)){
         // o = oo;
         // i = 
         }
         public mapping (sgmInt oo, sgmDouble ii){
           i = ii;
           o = oo;
         }
         public int val ( double Val) {
            return (int)Math.Round  ( 
                ((Val - i.min) /(i.max - i.min)) 
                                 * (o.max - o.min) + o.min
            )	;
         }
         public static implicit operator string (mapping m) {
             return  String.Format("in({0}:{1}):out({2}:{3})",m.i.min,m.i.max, m.o.min,m.o.max);
         }


      }     
      



    public  class PanelX : Panel

    {
      public   PanelX(): base(){
         DoubleBuffered=true;
      }

    }
    public  class Graph3D : OkCancel

    {
         const double padding = 25.0;
         public static Point space2pic (int x, int y, int z, int horzDegree, int vertDegree) {
            return   new Point(  (int)Math.Round( -x * cos(horzDegree) 
                                                    + z * sin(horzDegree)
                                  )
                       ,  (int)Math.Round(
                                - y * cos(vertDegree) 
                                  + x * (sin(horzDegree) * sin(vertDegree)) 
                                     + z * (cos(horzDegree) * sin(vertDegree))
                          )
                     );
         }
         static double sin( int degree){
            return sin ((double) degree);
         }
         static double sin( double degree){
            return Math.Sin(degree*(Math.PI/180));
         }                                  
         static double cos( int degree){
            return cos ((double) degree);
         }
         static double cos( double degree){
            return Math.Cos(degree*(Math.PI/180));
         }                                  



        Padding  _pd ;

        mapping x ;
        mapping y ;
        mapping z ;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
                   
                   // реальные значения из файла отображаются на графике

        public   Point space2pic (double xc,double yc, double zc){
            return space2pic(  x.val(xc)
                              , y.val(yc)
                                , z.val(zc)
                                  , hScrollBar1.Value
                                    , vScrollBar1.Value);
         }

         // рисовать ось координат, подается условные единицы шкалы от 0.0 до 1.0, 
         //            что бы рисовать на оси координат отметки шкалы, 
         //            например одну десятую, одну  вторую.

        public   Point space01_2pic (double xc, double yc, double zc ){
            return space2pic( (int)Math.Round(xc * s)
                              , (int)Math.Round(yc * s)
                                , (int)Math.Round( zc * s)
                                  , hScrollBar1.Value
                                    , vScrollBar1.Value);
         }


        int ww = 7, hh = 500;
        double[][] a;            // входные точки
        int s;                   // грань кубика для рисования
        int sh,sv;               //  точка возле которой выполняется вращение
        bool down = false;
        int xp, yp;
        int xv = 45, yv = 45; ///  начальная позиция    кубика
        PanelX  pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        Brush gBrush ;
        Brush wBrush ;
        Brush bBrush ;
        Pen pB ;
        Pen gP ;
        Pen rP ;
        
        public Graph3D(string name, int p):  base (name,700/ut.SZ.X_BUTTON - 2)
        {                 
            this.Name = "Form1";
            Paint += new PaintEventHandler(paint);
            OK_but.Text = "Read data";
            ESC_but.Text = "Exit";
            OK_but.Click    += new System.EventHandler(this.button1_Click);
            ESC_but.Click   += new System.EventHandler  (_close);
            InitializeComponent(p);

            Load            += new System.EventHandler  (_load);
            Resize          += new System.EventHandler  (_resize);
            gBrush = new SolidBrush (Color.Thistle);
            wBrush = new SolidBrush (Color.White);
            bBrush = new SolidBrush (Color.Black);
            pB = new Pen(Color.Black);
            gP = new Pen(Color.Blue);
            rP = new Pen(Color.Red);

            FormBorderStyle = FormBorderStyle.None;
            FormBorderStyle = FormBorderStyle.Sizable;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void 
        _load (object sender, System.EventArgs e)
        {
            /*
             sv = pictureBox1.Height / 2;
             sh = pictureBox1.Width / 2;
             s = (int)Math.Round(0.70 * sv);     ///???    задаем размер оси координат
             for (sv = pictureBox1.Height / 2    // новая версия, небольшой сдвиг вверх.
                                                 // чтобы помещалась гипотенуза
                    ; sv + Math.Sqrt(sv * sv + sv* sv) + 4 /// отступить от краев окна на 4 пикселя
                           >= pictureBox1.Height
                      ; sv--)
                ;  // sv размер катета
             s = sv;

            */

             gbl.log.WriteLine ("Load: PictureBox Size: W/H: {0}/{1}"
              , pictureBox1.Width, pictureBox1.Height);
             readFile (gbl.flNm);
             Invalidate();
             gbl.log.WriteLine ("Load")	;
             gbl.log.WriteLine ("mapping x :{0}", (string)x)	;
             gbl.log.WriteLine ("mapping y :{0}", (string)y);
             gbl.log.WriteLine ("mapping z :{0}", (string)z	);
         }

        private void button1_Click(object sender, EventArgs e)
        {   /// Graph3D x = (Graph3D)  sender;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                readFile (openFileDialog1.FileName);
                Draw();
            }
        }

        private void 
        _resize (object sender, System.EventArgs e)
        {
             ///Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
             ///pictureBox1.Image = bmp;
             gbl.log.WriteLine (IMPORTANCELEVEL.Spam,"Resize: PictureBox Size: W/H: {0}/{1}"
              , pictureBox1.Width, pictureBox1.Height);

             sh = pictureBox1.Width / 2;
             sv = pictureBox1.Height / 2;
             s = (int)Math.Round(0.70 * sv);     ///старая версия   задаем размер оси кооринат


             for (sv = pictureBox1.Height / 2    // новая версия, небольшой сдвиг вверх.
                                                 // чтобы помещалась гипотенуза
                    ;  sv + Math.Sqrt(sv * sv + sv* sv) + 4  // два пикселя сверху, два снизу
                          >= pictureBox1.Height
                      ; sv--)
                ;  // sv размер катета
             s = sv;
             sv +=2;
             x.o = new sgmInt(0, s);
             y.o = new sgmInt(0, s);
             z.o = new sgmInt(0, s);
             Draw();
        }


        void Draw() 
        {
            pictureBox1.Invalidate();
        }

        void paint(object s, PaintEventArgs e)
        {
           e.Graphics.FillRectangle (gBrush, 5,5, ClientSize.Width-10, ClientSize.Height-10);
        }


        void paint1(object s, PaintEventArgs e)
        {
            Draw1(e.Graphics);
        }


        int readFile (string flNm){
          string line="";
          x = new mapping(new sgmInt(0, s));
          y = new mapping(new sgmInt(0, s));
          z = new mapping(new sgmInt(0, s));
                 
          try {
            List<double[]> _x = new List<double[]>();
            using( StreamReader sr = new StreamReader(flNm))
            {   
                char[] dels = new char[] {' ','\t'};
                int lineno = 0;
                while (!sr.EndOfStream)
                {
                    lineno++;
                    line  = sr.ReadLine();
                    int Pos = line.IndexOf('#');
                    //Если был найден, удалить подстроку, начиная с этой позиции
                    if (Pos >= 0)
                        line = line.Remove(Pos);
                    line = line.Trim();
                    string[] ss = line.Split( dels,  StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length >= 3)  {        /// becouse x, y, z only 3 coors
                       double[] r = new double[3];  //  это три координаты одна точка
                       int j = 0; 
                       for (j=0; j < 3; j++)
                       {
                          r[j] = Convert.ToDouble(ss[j]);
                       }
                                                  gbl.log.WriteLine (IMPORTANCELEVEL.Spam
                                                       ,"readFile:  line/s  :'{3}'/'{0}', '{1}', '{2}'"
                                                           , ss[0], ss[1], ss[2], line)	;
                                                  gbl.log.WriteLine (IMPORTANCELEVEL.Spam
                                                       ,"readFile:  r  :{0}, {1}, {2}"
                                                           , r[0], r[1], r[2])	;
                        x.i.min  = Math.Min(x.i.min, r[0]) ;
                        x.i.max  = Math.Max(x.i.max, r[0]) ;
                        y.i.min  = Math.Min(y.i.min, r[1]) ;
                        y.i.max  = Math.Max(y.i.max, r[1]) ;
                        z.i.min  = Math.Min(z.i.min, r[2]) ;
                        z.i.max  = Math.Max(z.i.max, r[2]) ;
                       _x.Add(r);                             /// список для   накапливания строчек
                                                  gbl.log.WriteLine (IMPORTANCELEVEL.Spam,"mapping x :{0}", (string)x)	;
                                                  gbl.log.WriteLine (IMPORTANCELEVEL.Spam,"mapping y :{0}", (string)y);
                                                  gbl.log.WriteLine (IMPORTANCELEVEL.Spam,"mapping z :{0}", (string)z	);
                    }
                    else 
                      if (!String.IsNullOrEmpty(line))
                       gbl.log.WriteLine (  IMPORTANCELEVEL.Warning
                         , ">> readFile: wrong line #{0}: '{1}'"
                            , lineno, line)	;
                }
                //
                //  что бы рисовать плоские фигуры, перпендикулярные одной из осей координат
                //  надо чуть чуть раздвинуть максимум и минимум по этой  оси
                //
                if((x.i.max - x.i.min) <= 0.0) x.i.max= x.i.min+1;
                if((y.i.max - y.i.min) <= 0.0) y.i.max= y.i.min+1;
                if((z.i.max - z.i.min) <= 0.0) z.i.max= z.i.min+1;

                x.i.min  -= ((x.i.max - x.i.min)/padding);
                x.i.max  += (x.i.max - x.i.min)/padding ;
                y.i.min  -= (y.i.max - y.i.min)/padding ;
                y.i.max  += (y.i.max - y.i.min)/padding ;
                z.i.min  -= (z.i.max - z.i.min)/padding ;
                z.i.max  += (z.i.max - z.i.min)/padding ;

                gbl.log.WriteLine (IMPORTANCELEVEL.Debug,"readFile: after all mapping1 x :{0}"
                    , (string)x)	;
                gbl.log.WriteLine (IMPORTANCELEVEL.Debug,"readFile: after all mapping1 y :{0}"
                     , (string)y);
                gbl.log.WriteLine (IMPORTANCELEVEL.Debug,"readFile: after all mapping1 z :{0}"
                    , (string)z	);

                a = _x.ToArray();         //// выходной массив готов
            }
            return a.Length;
         }
         catch (Exception e){
            gbl.log.WriteLine ("*** readFile: error while reading data file. line :'{}'", line);
            gbl.log.WriteLine ("Exception '{0}'\n'{1}'\n'{2}'\n'{3}' ***", e.Message
                               , e.StackTrace
                                 , e.TargetSite
                                    , e.Source
                                         );

            return -1;
         }
        } 

        void Draw1(Graphics g) 
        {
          //  int h, v, i, k1, k2;
            Point po;
            string text;                                            // малюємо ОХ
            po = space01_2pic(1.0,0.0,0.0);
            g.DrawLine(pB,sh,sv,sh+po.X,sv+po.Y);
            text=String.Format (
//              "x:[{0:F2}, {1:F2}]"
              "x:[{0}, {1}]"
              , ((int)(Math.Round(x.i.min*100)))/100.0
                     , (int)(Math.Round(x.i.max*100))/100.0);
            g.DrawString(text, this.Font, bBrush, sh+po.X,sv+po.Y);
                                                                    // малюємо ОY
            po = space01_2pic(0.0,1.0,0.0);
            g.DrawLine(pB,sh,sv,sh+po.X,sv+po.Y);
            text=String.Format (
//              "y:[{0:F2}, {1:F2}]"
///              "y:[{0}, {1}]"
              "z:[{0}, {1}]"
              , (int)(Math.Round(y.i.min*100))/100.0, (int)(Math.Round(y.i.max*100))/100.0);
            g.DrawString(text, this.Font, bBrush, sh+po.X,sv+po.Y);
                                                         // малюємо ОZ
            po = space01_2pic(0.0,0.0,1.0);
            g.DrawLine(pB,sh,sv,sh+po.X,sv+po.Y);
            text=String.Format (
//               "z:[{0:F2}, {1:F2}]"
///               "z:[{0}, {1}]"
               "y:[{0}, {1}]"
            , (int)(Math.Round(z.i.min*100))/100.0, (int)(Math.Round(z.i.max*100))/100.0);
            g.DrawString(text, this.Font, bBrush, sh+po.X,sv+po.Y);
 
           DrawPoints1( g, rP);
         }

        void DrawPoints1( Graphics gr, Pen p)
        {
         //    double hx, hy, hz;
           //  string foo;
             if (a!=null)
             {
                List<Point> ln  = new List<Point>();
                Point po;
                Point po1;
                for(int i = 0; i < a.Length; i++)  {
                    po = space2pic( a[i][0],  a[i][1], a[i][2]);
                    po1 = new Point(sh + po.X,  sv + po.Y);
                    gr.DrawEllipse (p, po1.X
                                     , po1.Y, 2,2);

                    if (gbl.lnF && po != null )
                      ln.Add(po1);

                    if (gbl.vF) 
                      gr.DrawString(i.ToString(), this.Font, bBrush, sh + po.X+2, sv + po.Y );
                }
                if (gbl.lnF && ln != null ) {     //нарисовать линию
                  gr.DrawLines(gP, ln.ToArray());
                  ln.Clear();
                }
             }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Draw();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Draw();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
            xp = e.X;
            yp = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (down && e.Button == MouseButtons.Left)
            {
                if (xp < e.X && xv < 89) xv = xv + 1;
                if (xp > e.X && xv > 0 ) xv = xv - 1;
                if (yp < e.Y && yv < 89) yv = yv + 1;
                if (yp > e.Y && yv > 0)  yv = yv - 1;
//                hScrollBar1.Value = (int)Math.Round(xv);
//                vScrollBar1.Value = (int)Math.Round(yv);
                hScrollBar1.Value = xv;
                vScrollBar1.Value = yv;
                xp = e.X;
                yp = e.Y;
                Draw();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }
        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            down = false;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }
        private void 
        _close (object sender, System.EventArgs e)
        {
           Close();
        }                                     
        private void InitializeComponent(int  p)
        {
            AutoSize = true;
            _pd =  new Padding(p, p,p, p);
			      Padding  = _pd;
            this.pictureBox1 = new PanelX();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
     ////       ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
//            this.pictureBox1.Size = new System.Drawing.Size(694, 451);
////            this.pictureBox1.Size = new System.Drawing.Size(70, 45);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
						this.pictureBox1.Size =new Size(ww, hh);
						this.pictureBox1.Dock   = DockStyle.Top;
            this.pictureBox1.Paint += new PaintEventHandler(paint1);
            this.pictureBox1.BackColor = Color.White;
           // this.pictureBox1.DoubleBuffered = true;
            ///this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            // 
            this.openFileDialog1.FileName = "data.txt"; //
            this.openFileDialog1.FileName = "data.txt"; //
						this.openFileDialog1.InitialDirectory = "." ;            // 
						this.openFileDialog1.Filter = "txt files (*.txt)|*.txt| csv files (*.csv)|*.csv| All files (*.*)|*.*" ;
						this.openFileDialog1.FilterIndex = 1 ;
    				this.openFileDialog1.RestoreDirectory = true ;
            // hScrollBar1
            // 
//            this.hScrollBar1.Location = new System.Drawing.Point(1, 455);
            this.hScrollBar1.Maximum = 91;  //  угол разворота  радианах
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(714, 17);
            this.hScrollBar1.TabIndex = 3;
            this.hScrollBar1.Value = 45;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler
                     (this.hScrollBar1_Scroll);
						this.hScrollBar1.Dock   = DockStyle.Top;
////            
            // 
            // vScrollBar1
            // 
      //      this.vScrollBar1.Location = new System.Drawing.Point(698, 1);
            this.vScrollBar1.Maximum = 91;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 451);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Value = 45;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
//	
					this.vScrollBar1.Dock   = DockStyle.Right;

            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
///            this.ClientSize = new System.Drawing.Size(780, 475);
            Panel p1 = new Panel();

            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.vScrollBar1);


            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
         /////   ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}

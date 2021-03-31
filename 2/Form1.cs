using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ut;

namespace SkakovskiyKursova
{
    public partial class Graph3D : OkCancel

    {
        Padding  _pd ;

        int ww = 7, hh = 500;
        double[][] a,b;

        int N, h1,v1,h2,v2,h3,v3,s;
        int sh,sv;
        int a2,b2;
        double minx,miny,minz,maxx,maxy,maxz;
        bool down = false;
        int xp, yp;
        double xv = 45, yv = 45;
        private System.Windows.Forms.PictureBox pictureBox1;
        //private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
       // private System.Windows.Forms.Button button2;
        Brush gBrush ;
        Brush wBrush ;
        Brush bBrush ;

        
        
        public Graph3D(string name, int p):  base (name,700/ut.SZ.X_BUTTON - 2)
        {                 
              //ControlBox = true;
                               	
//            Paint += ;
            this.Name = "Form1";
            Paint += new PaintEventHandler(paint);
            OK_but.Text = "Read data";
            OK_but.Click += new System.EventHandler(this.button1_Click);

            ESC_but.Click    += new System.EventHandler  (_close);
        //    Show    += new System.EventHandler  (_show);

            InitializeComponent(p);
            Load    += new System.EventHandler  (_load);
            Resize    += new System.EventHandler  (_resize);
            gBrush = new SolidBrush (Color.Green);
            wBrush = new SolidBrush (Color.White);
            bBrush = new SolidBrush (Color.Black);
            FormBorderStyle = FormBorderStyle.None;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            FormBorderStyle = FormBorderStyle.Sizable;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;


        }


   private void 
   _load (object sender, System.EventArgs e)
   {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            Console.WriteLine ("Load: PictureBox Size: W/H: {0}/{1}"
             , pictureBox1.Width, pictureBox1.Height);
#if DEBUG
#endif
   
   
   }

   private void 
   _resize (object sender, System.EventArgs e)
   {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            Console.WriteLine ("Resize: PictureBox Size: W/H: {0}/{1}"
             , pictureBox1.Width, pictureBox1.Height);
#if DEBUG
#endif
   
   
   }


   private void 
   _close (object sender, System.EventArgs e)
   {
      Close();
#if DEBUG
       Console.WriteLine ("OkCancel._OK_but: \n");
#endif

   }                           

       private void InitializeComponent(int  p)
        {

            AutoSize = true;
            _pd =  new Padding(p, p,p, p);
			      Padding  = _pd;


            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
  ////          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
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



            ///this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            // 
            // button1
            // 
/** 
            this.button1 = new System.Windows.Forms.Button();
            this.button1.Location = new System.Drawing.Point(718, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "прочитать файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.Controls.Add(this.button1);
   **/         // 
            // openFileDialog1
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
            this.hScrollBar1.Maximum = 90;  //  угол разворота
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
            this.vScrollBar1.Maximum = 90;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 451);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Value = 45;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
//	
					this.vScrollBar1.Dock   = DockStyle.Right;
////
            // 
            // button2
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
///            this.ClientSize = new System.Drawing.Size(780, 475);
this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.pictureBox1);
this.Controls.Add(this.vScrollBar1);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
///            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        void paint(object s, PaintEventArgs e)
        {
           // 
            Graphics g = e.Graphics;
            // залить окно под картинкой  зеленым цветом 
            g.FillRectangle (gBrush, 5,5, ClientSize.Width-10, ClientSize.Height-10);
            Draw1();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            List<double[]> x = new List<double[]>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(' ','\t');
                    double[] r = new double[3];
                    int j = 0; int i = 0;
                    while (j < s.Length)
                    {
                        if (!String.IsNullOrEmpty(s[j]))
                        {
                            r[i] = Convert.ToDouble(s[j].Replace('.',','));
                            i += 1;
                        }
                        j += 1;
                    }
                    x.Add(r);
                }
                N = x.Count;
                a = x.ToArray();
                sr.Close();
                Draw();
            }
        }
        void Draw() 
        {
       // Draw1();   
          /// 
            Rectangle rec = new Rectangle(pictureBox1.Location.X, pictureBox1.Location.Y
                                                   , pictureBox1.Width, pictureBox1.Height);
          Invalidate(rec);
        }
        void Draw1() 
        {
            int h, v, i, k1, k2, first = 0;

//                        Bitmap 
            //pictureBox1.Image = bmp;

            Bitmap bmp;    // 
            Console.WriteLine ("Draw: PictureBox Size: W/H: {0}/{1}"
             , pictureBox1.Width, pictureBox1.Height);

            //
            if (hh != pictureBox1.Height || ww !=pictureBox1.Width )  
            { first = 1;
              Console.WriteLine ("new size");
              bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
              hh = pictureBox1.Height;
              ww =pictureBox1.Width;
            }
            else  {
              first = 0;
              bmp = (Bitmap)      pictureBox1.Image;
                
            }
           /// pictureBox1.Image =bmp;
              //  pictureBox1.Image =bmp;
                              //new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);       /// рисую на картинке.
            g.FillRectangle(wBrush
                ,0,0
                   ,pictureBox1.Width
                       ,pictureBox1.Height);
            Pen p = new Pen(Color.Black);
            //найшли середину
            sv = pictureBox1.Height / 2;
            sh = pictureBox1.Width / 2;
            s = (int)Math.Round(0.70 * sv);     ///???    задаем размер оси координат
            
///                    a2 = (int)Math.Round(-a[i][0] * Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) + a[i][2] * Math.Sin((hScrollBar1.Value) * (Math.PI / 180)));  // горизонталь
////                    b2 = (int)Math.Round(-a[i][1] * Math.Cos((vScrollBar1.Value) * (Math.PI / 180)) + a[i][0] * (Math.Sin((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180))) + a[i][2] * (Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180)))); //ветикаль
            // малюємо ОХ
            v1 = (int)Math.Round(s*Math.Cos((hScrollBar1.Value)*(Math.PI/180)));
            h1 = (int)Math.Round(s*Math.Sin((hScrollBar1.Value)*(Math.PI/180))
                                         *Math.Sin((vScrollBar1.Value)*(Math.PI/180)));
            
            g.DrawLine(p,sh,sv,sh-v1,sv+h1);


            g.DrawString("X", this.Font, bBrush,sh-v1,sv+h1);




           // малюємо ОZ
           v3 = (int)Math.Round(s*Math.Sin((hScrollBar1.Value)*(Math.PI/180)));
           h3 = (int)Math.Round(s*Math.Cos((hScrollBar1.Value)*(Math.PI/180))
                            *Math.Sin((vScrollBar1.Value)*(Math.PI/180)));
           g.DrawLine(p,sh,sv,sh+v3,sv+h3);
           g.DrawString("Z", this.Font, bBrush, sh+v3,sv+h3);

           // малюємо ОY
           v2 = (int)Math.Round(s*Math.Sin((vScrollBar1.Value)*(Math.PI/180)));
           h2 = (int)Math.Round(s*Math.Cos((vScrollBar1.Value)*(Math.PI/180)));
           g.DrawLine(p,sh,sv,   sh,sv-h2);
                                     g.DrawString("Y", this.Font, bBrush, sh,sv-h2);
           DrawPoints(bmp, g);
        //   if (first>0)
         }

        void DrawPoints(Bitmap g, Graphics gr)
        {
             double hx, hy, hz;
             string foo;
             if (a!=null)
             {
                minx = a[0][0];
                maxx = a[0][0];
                miny = a[0][1];
                maxy = a[0][1];
                minz = a[0][2];
                maxz = a[0][2];
                for (int i = 0; i < N;i++)
                {
                    if (a[i][0] < minx)  minx = a[i][0];
                    if (a[i][1] < miny)  miny = a[i][1];
                    if (a[i][2] < minz)  minz = a[i][2];
                    if (a[i][0] > maxx)  maxx = a[i][0];
                    if (a[i][1] > maxy)  maxy = a[i][1];
                    if (a[i][2] > maxz)  maxz = a[i][2];
                }

                hx = maxx-minx;
                hy = maxy-miny;
                hz = maxz-minz;
                hx = s/hx;
                hy = s/hy;
                hz = s/hz;         	
                for(int i = 0; i < N; i++)
                {
                    a[i][0] = (a[i][0]-minx)*hx;
                    a[i][1] = (a[i][1]-miny)*hy;
                    a[i][2] = (a[i][2]-minz)*hz;
                }
                minx = (minx-minx)*hx;
                miny = (miny-miny)*hy;
                minz = (minz-minz)*hz;
                maxx = (maxx-minx)*hx;
                maxy = (maxy-miny)*hy;
                maxz = (maxz-minz)*hz;
                for (int i = 0; i < N; i++)
                {
                    a2 = (int)Math.Round(-a[i][0] * Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) + a[i][2] * Math.Sin((hScrollBar1.Value) * (Math.PI / 180)));  // горизонталь
                    b2 = (int)Math.Round(-a[i][1] * Math.Cos((vScrollBar1.Value) * (Math.PI / 180)) + a[i][0] * (Math.Sin((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180))) + a[i][2] * (Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180)))); //ветикаль

                    g.SetPixel(sh + a2,   sv + b2, Color.Red);
                    g.SetPixel(sh + a2+1, sv + b2, Color.Blue);
                    g.SetPixel(sh + a2-1, sv + b2, Color.Blue);
                    g.SetPixel(sh + a2,   sv + b2+1, Color.Blue);
                    g.SetPixel(sh + a2,   sv + b2-1, Color.Blue);
                }
                for (int i = 0; i < N; i++)
                {
                    a2 = (int)Math.Round(-a[i][0] * Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) + a[i][2] * Math.Sin((hScrollBar1.Value) * (Math.PI / 180)));  // горизонталь
                    b2 = (int)Math.Round(-a[i][1] * Math.Cos((vScrollBar1.Value) * (Math.PI / 180)) + a[i][0] * (Math.Sin((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180))) + a[i][2] * (Math.Cos((hScrollBar1.Value) * (Math.PI / 180)) * Math.Sin((vScrollBar1.Value) * (Math.PI / 180)))); //ветикаль

                    gr.DrawString(i.ToString(), this.Font, bBrush, sh+a2+2,sv+b2+2);
                    Console.WriteLine("{0}-th point  py/px: {1}/{2} ", i,  sh+a2+2, sv+b2+2);
                }

               // pictureBox1.Image = g;
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
                if (xp > e.X && xv > 0.5) xv = xv - 1;
                if (yp < e.Y && yv < 89) yv = yv + 1;
                if (yp > e.Y && yv > 0.7) yv = yv - 1;
                hScrollBar1.Value = (int)Math.Round(xv);
                vScrollBar1.Value = (int)Math.Round(yv);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}

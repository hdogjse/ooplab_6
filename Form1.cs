using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ooplab_4
{
    public partial class Form1 : Form
    {
        private Color _color = Color.Blue;
        class gOBJ
        {


            protected class Model
            {
                public bool PointOnArea(Rectangle area, int x, int y)
                {
                    if (x >= area.X && x <= area.X + area.Width && y >= area.Y && y <= area.Y + area.Height)
                    {
                        return true;
                    }
                    return false;
                }
            }


            protected Rectangle area;

            protected Model model;

            protected Pen pen;

            protected Color _color = Color.Blue;

            protected bool selected = false;

            public virtual void SetSelected()
            {
                selected = true;

            }

            public virtual void SetColor(Color color)
            {
                pen.Color = color;
            }

            public virtual void SetUnSelected()
            {
                selected = false;
            }

            public int X()
            {
                return area.X;
            }

            public int Y()
            {
                return area.Y;
            }

            public void SetPos(int x, int y)
            {
                area.X = x;
                area.Y = y;
            }

            public void SetWidth(int width)
            {
                area.Width = width;
            }

            public int Width()
            {
                return area.Width;
            }

            public void SetHeight(int height)
            {
                area.Height = height;
            }

            public int Height()
            {
                return area.Height;
            }

            public bool PointOnMe(int x, int y)
            {
                return model.PointOnArea(area, x, y);
            }
            public virtual void Draw(PaintEventArgs ev)
            {
                if (selected)
                {
                    Pen pen = new Pen(Color.Brown);
                    pen.DashStyle = DashStyle.Dash;
                    pen.Width = 5;
                    Rectangle rec;
                    rec = area;
                    rec.X -= 5;
                    rec.Y -= 5;
                    rec.Width += 10;
                    rec.Height += 10;
                    ev.Graphics.DrawRectangle(pen, rec);
                }
            }


        }

        class CCircle : gOBJ
        {

            public CCircle(int x, int y)
            {
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 100;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.DrawEllipse(pen, area);
            }
        }

        class CRect : gOBJ
        {

            public CRect(int x, int y)
            {
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 100;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.DrawRectangle(pen, area);
            }
        }


        class CLine : gOBJ
        {
            private Brush brush;
            public CLine(int x, int y)
            {
                brush = new SolidBrush(Color.Blue);
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 10;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void SetColor(Color color)
            {
                base.SetColor(color);
                brush = new SolidBrush(color);
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.FillRectangle(brush, area);
            }
        }

        List<gOBJ> circles;

        List<gOBJ> selected_circles;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            circles = new List<gOBJ>();
            selected_circles = new List<gOBJ>();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].Draw(e);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Left)
            //    return;
            //for (int i = 0; i < circles.Count; i++)
            //{
            //    if (circles[i].PointOnMe(e.X, e.Y))
            //    {
            //        if (!MultiSelect.Checked |
            //            MultiSelect.Checked &
            //            (
            //            ctrl_use.Checked & !ModifierKeys.HasFlag(Keys.Control)
            //            ))
            //        {
            //            for (int j = 0; j < selected_circles.Count; j++)
            //            {
            //                selected_circles[j].SetUnSelected();
            //            }
            //            selected_circles.Clear();
            //        }
            //        selected_circles.Add(circles[i]);
            //        circles[i].SetSelected();
            //        this.Invalidate(true);
            //        return;
            //    }
            //}
            //this.Update();
            //gOBJ obj;
            //if (e.X + 120 >= Width)
            //    return;
            //if (e.Y + 140 >= Height)
            //    return;
            //switch (comboBox1.SelectedIndex)
            //{
            //    case 0:
            //        obj = new CCircle(e.X-50, e.Y-50);
            //        break;
            //    case 1:
            //        obj = new CRect(e.X-50, e.Y-50);
            //        break;
            //    case 2:
            //        obj = new CLine(e.X, e.Y);
            //        break;
            //    default:
            //        return;

            //}

            //circles.Add(obj);
            //this.Invalidate(true);
            //return;


            if (e.Button != MouseButtons.Left)
                return;
            bool sel = false;
            for (int i = 0; i < circles.Count; i++)
            {
                if (circles[i].PointOnMe(e.X, e.Y))
                {
                    if (!MultiSelect.Checked |
                        MultiSelect.Checked &
                        (
                        ctrl_use.Checked & !ModifierKeys.HasFlag(Keys.Control)
                        ))
                    {
                        for (int j = 0; j < selected_circles.Count; j++)
                        {
                            selected_circles[j].SetUnSelected();
                        }
                        selected_circles.Clear();
                    }
                    selected_circles.Add(circles[i]);
                    circles[i].SetSelected();
                    this.Invalidate(true);
                    if (ctrl_use.Checked & ModifierKeys.HasFlag(Keys.Control))
                    {
                        return;
                    }
                    sel = true;
                }
            }
            if (sel)
                return;
            this.Update();
            gOBJ obj;
            if (e.X + 120 >= Width)
                return;
            if (e.Y + 140 >= Height)
                return;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    obj = new CCircle(e.X-50, e.Y-50);
                    break;
                case 1:
                    obj = new CRect(e.X-50, e.Y-50);
                    break;
                case 2:
                    obj = new CLine(e.X, e.Y);
                    break;
                default:
                    return;

            }

            circles.Add(obj);
            this.Invalidate(true);
            return;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected_circles.Count == 0)
                return;
            return;
            /*            selected_circle.SetPos(e.X, e.Y);
                        selected_circle.SetSelected();
                        this.Invalidate(true);*/
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {

            }
            if (e.KeyCode == Keys.Escape)
            {
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    selected_circles[i].SetUnSelected();
                }
                selected_circles.Clear();
            }

            if (e.KeyCode == Keys.Delete)
            {
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    circles.Remove(selected_circles[i]);
                }
                selected_circles.Clear();
            }

            if (e.Control)
            {
                if (e.KeyCode == Keys.Left)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Width() - 1 <= 0)
                            continue;
                        selected_circles[i].SetWidth(selected_circles[i].Width() - 1);
                    }
                }

                if (e.KeyCode == Keys.Right)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() + selected_circles[i].Width() + 20 >= Width)
                            continue;
                        selected_circles[i].SetWidth(selected_circles[i].Width() + 1);
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() + selected_circles[i].Height() + 40 >= Height)
                            continue;
                        selected_circles[i].SetHeight(selected_circles[i].Height() + 1);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Height() - 1 <= 0)
                            continue;
                        selected_circles[i].SetHeight(selected_circles[i].Height() - 1);
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Left)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() - 1 < 0)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X() - 1, selected_circles[i].Y());
                    }
                }

                if (e.KeyCode == Keys.Right)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() + selected_circles[i].Width() + 20 >= Width)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X() + 1, selected_circles[i].Y());
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() + selected_circles[i].Height() + 40 >= Height)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X(), selected_circles[i].Y() + 1);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() - 1 < 0)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X(), selected_circles[i].Y() - 1);
                    }
                }

            }
            this.Invalidate(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void clr_btn_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = _color;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                _color = MyDialog.Color;
                clr_btn.BackColor = _color;
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    selected_circles[i].SetColor(_color);
                }
                this.Invalidate(true);
            }

        }
    }
}
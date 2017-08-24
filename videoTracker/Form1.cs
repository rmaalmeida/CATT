using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Xml.Serialization;

//serialization
using System.IO;
using System.Runtime.InteropServices;

//control transparency
using System.Drawing.Drawing2D;


namespace videoTracker
{
    public partial class Form1 : Form
    {
        Boolean saved = true;
        Boolean played = false;
        Boolean tracklistExist = false;
        TranspForm transpImage;

        public Form1()
        {
            InitializeComponent();

        }

        bool videoloaded;

        private string formContext()
        {
            string txt = "";
            string[] a;
            a = mplayer.URL.Split('\\');
            txt += "N:" + a[a.Length - 1];
            txt += ", T:" + mplayer.Ctlcontrols.currentPositionString;
            // txt += ", Ctr:"+mplayer.Ctlcontrols.ToString();
            txt += ", Selec:" + tracksList.SelectedIndex;
            txt += ", Tracking:" + tick.Enabled.ToString();
            txt += ", Map:" + displayMap.ToString();

            return txt;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case 'z':
                    // open video
                    loadVideo();

                    break;
                case 'x':
                    // play video
                    mplayer.Ctlcontrols.play();
                    played = true;
                    break;
                case 'c':
                    // pause video
                    mplayer.Ctlcontrols.pause();
                    break;
                case 'v':
                    // stop video
                    mplayer.Ctlcontrols.stop();
                    break;
                case 's':
                    // new track (put on list)
                    //Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);
                   // MessageBox.Show("O width é " + nb.Width + "e o height é" + nb.Height);
                    startNewTrack();
                    break;
                case 'd':
                    // (re)start tracking selected on list
                    if (System.IO.File.Exists(mplayer.URL))
                    {
                        if (tracksList.Items.Count > 0)
                        {
                            tick.Start();
                        }
                        else
                        {
                            MsgBox.Show("Select or Create a New Track", "Warning Message", "OK");
                        }
                    }
                    else MsgBoxVideo.Show("Please, Load a Video Using 'z'", "Warning Message", "OK");
                    //MessageBox.Show("Abra um vídeo");
                           
                    //tick.Start();
                    break;
                case 'f':
                    // stop/pause tracking
                    tick.Stop();
                    break;
                case 'e':
                    // enable map generation
                    //regenerate at each point added or resize
                    displayMap = true;
                    break;
                case 'r':
                    // disable map generation
                    displayMap = false;
                    break;
                case 'm':
                    //double data = mapDist((track)tracksList.Items[tracksList.SelectedIndex]);
                    //MsgBoxVideo.Show(data.ToString(), "Warning Message", "OK");
                    break;
                default:
                    break;
            }

            this.Text = formContext() + " " + e.KeyChar;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog;
            if (saved == false)
            {            
                if (MsgBoxQuit.Show("You made changes, do you really want to exit without saving?", "Exit", "Yes", "No") == DialogResult.Yes)
                {
                }
                else e.Cancel = true;
            }

        }
        void loadVideo()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            mplayer.URL = ofd.FileName;
            saved = false;
        }

        void startNewTrack()
        {
            tracksList.Items.Add(new track());
            //select last task
            tracksList.SelectedIndex = tracksList.Items.Count - 1;
            saved = false;
            tracklistExist = true;
        }

        void deleteTrack()
        {
            //tracksList.Items.RemoveAt(new track());
            //select last task
            if (tracksList.Items.Count <= 0)
            {
              return;
            }
            tracksList.Items.RemoveAt(tracksList.SelectedIndex);
            tracksList.SelectedIndex = tracksList.Items.Count - 1;
            saved = false;
            if (tracksList.Items.Count < 1) {
                tracklistExist = false;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            loadVideo();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            startNewTrack();
            mplayer.Ctlcontrols.currentPosition = 0;
            mplayer.Ctlcontrols.play();
        }

        private void btClose_Click(object sender, EventArgs e)
        {

        }

        private void tick_Tick(object sender, EventArgs e)
        {
            //try to get conversion
            getConversion();

            PointF pt = new PointF();

            pt = mplayer.PointToClient(Cursor.Position);

            pt.X = (pt.X - bw) * aw;
            pt.Y = (pt.Y - bh) * ah;

            ((track)tracksList.Items[tracksList.SelectedIndex]).dataList.Add(new trackPoint(pt, mplayer.Ctlcontrols.currentPosition));

            tracksList.Refresh();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //show controls
            mplayer.uiMode = "full";
            mplayer.stretchToFit = true;
            mplayer.settings.autoStart = false;

            transpImage = new TranspForm();
            transpImage.Show();
            transpImage.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        //converter values
        float aw = 1;
        float ah = 1;
        float bw = 0;
        float bh = 0;


        void getConversion()
        {

            int vw = mplayer.currentMedia.imageSourceWidth;
            int vh = mplayer.currentMedia.imageSourceHeight;
            int mw = mplayer.Width;
            //remove control bar (64 is empirical
            int mh = mplayer.Height - 64;
            if ((float)vw / mw > (float)vh / mh)
            {
                //width full
                bw = 0;
                aw = 1.0f / mw;

                bh = (mh - (((float)mw / vw) * vh)) / 2;
                ah = 1.0f / (mh - (bh * 2));
            }
            else
            {
                //heigh full
                bw = (mw - (((float)mh / vh) * vw)) / 2;
                aw = 1.0f / (mw - (bw * 2));


                bh = 0;
                ah = 1.0f / mh;
            }
        }

        bool displayMap;

        private void Form1_Resize(object sender, EventArgs e)
        {

            if (displayMap)
            {
                getConversion();
                //-64 to remove control bar
                Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);
                drawHeatMap(nb, (track)tracksList.Items[tracksList.SelectedIndex]);


                //transp form setup
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

                int titleHeight = screenRectangle.Top - this.Top;
                //sum 8 I simply dont know why, but it works
                Point pt = new Point(this.Location.X + mplayer.Location.X + (int)bw + 8, this.Location.Y + mplayer.Location.Y + titleHeight + (int)bh);

                transpImage.Location = pt;
                transpImage.Size = mplayer.Size;
                transpImage.SetBits(nb);
            }
        }

        public void saveXml()
        {
            List<track> tracks = new List<track>();
            for (int tr = 0; tr < tracksList.Items.Count; tr++)
            {
                tracks.Add((track)tracksList.Items[tr]);
            }
            XmlSerializer ser = new XmlSerializer(typeof(List<track>));
            using (TextWriter writer = new StreamWriter(mplayer.URL + ".xml", false))
            {
                ser.Serialize(writer, tracks);
            }
            saved = true;
        }

        public void loadXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<track>));
            List<track> tracks;
            using (TextReader reader = new StreamReader(mplayer.URL + ".xml"))
            {
                tracks = (List<track>)ser.Deserialize(reader);
            }
            tracksList.Items.Clear();
            foreach (track tr in tracks)
            {
                tracksList.Items.Add(tr);
            }
            if (tracksList.Items.Count>0)
            {
                tracksList.SelectedIndex = 0;
            }
            saved = false;
            tracklistExist = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveXml();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(mplayer.URL))
            {
                if (System.IO.File.Exists(mplayer.URL + ".xml")) {
                    loadXml();
                }
                else MsgBoxTrack.Show("You Need to Create a Track First", "Warning Message", "OK");
            }
            else MsgBoxVideo.Show("Please, Load a Video Using 'z'", "Warning Message", "OK");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //-64 to remove media player bar
            if (System.IO.File.Exists(mplayer.URL))
            {
                if (System.IO.File.Exists(mplayer.URL + ".xml"))
                {
                    if (tracklistExist == true) {

                    Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);
                    drawHeatMap(nb, (track)tracksList.Items[tracksList.SelectedIndex]);


                    //transp form setup
                    Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

                    int titleHeight = screenRectangle.Top - this.Top;
                    //sum 8 I simply dont know why, but it works
                    Point pt = new Point(this.Location.X + mplayer.Location.X + (int)bw + 8, this.Location.Y + mplayer.Location.Y + titleHeight + (int)bh);

                    transpImage.Location = pt;
                    transpImage.Size = mplayer.Size;
                    transpImage.SetBits(nb);
                    displayMap = true;
                    }
                    else MsgBoxTrack.Show("        Create or Load a Track     ", "Warning Message", "OK");
                }
                else MsgBoxTrack.Show("You Need to Create a Track First", "Warning Message", "OK");
            }
            else MsgBoxVideo.Show("Please, Load a Video Using 'z'", "Warning Message", "OK");

        }


        public double findDist(track points)
        {
            double res = 0;
            for (int i = 0; i < (points.dataList.Count-1); i++)
            {
                res += Math.Sqrt(
                    Math.Pow((points.dataList[i].point.X - points.dataList[i + 1].point.X), 2) +
                    Math.Pow((points.dataList[i].point.Y - points.dataList[i+1].point.Y),2)
                    );
            }
            return res;
        }


        public double mapDist(track points)
        {
            List<double> dists = new List<double>();
            double res = 0;
            double interval = 10; // in seconds
            double startTime;
            startTime = points.dataList[0].timeStamp;

            //be sure it is sorted before;
            for (int i = 0; i < (points.dataList.Count - 1); i++)
            {
                if (startTime + interval > points.dataList[i].timeStamp)
                {
                    res += Math.Sqrt(
                   Math.Pow((points.dataList[i].point.X - points.dataList[i + 1].point.X), 2) +
                   Math.Pow((points.dataList[i].point.Y - points.dataList[i + 1].point.Y), 2)
                   );
                }
                else
                {
                    dists.Add(res);
                    startTime = points.dataList[i].timeStamp;
                    res = Math.Sqrt(
                   Math.Pow((points.dataList[i].point.X - points.dataList[i + 1].point.X), 2) +
                   Math.Pow((points.dataList[i].point.Y - points.dataList[i + 1].point.Y), 2)
                   );
                }
               
            }
            return res;
        }


        public void drawHeatMap(Bitmap processedBitmap, track points)
        {
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * processedBitmap.Height;
            byte[] pixels = new byte[byteCount];

            IntPtr ptrFirstPixel = bitmapData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            int widthInPixels = bitmapData.Width;
            //heatmap calc
            int[,] heatCountNeighbours = points.getHeatMatrix(heightInPixels, widthInPixels, Math.Min(heightInPixels, widthInBytes) / 15, 10);

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    //maximizing
                    byte value = (byte)heatCountNeighbours[y, x / 4];
                    // calculate new pixel value
                    pixels[currentLine + x] = 255;
                    pixels[currentLine + x + 1] = 255;
                    pixels[currentLine + x + 2] = 255;
                    //only changes alpha
                    pixels[currentLine + x + 3] = value;
                }
            }

            // copy modified bytes back
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            processedBitmap.UnlockBits(bitmapData);
            points.drawTracjetory(processedBitmap, 0.00007);
        }

        private void tracksList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = new SolidBrush(((track)tracksList.Items[e.Index]).cor);

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            // switch (e.Index) {
            //     case 0:
            //         myBrush = Brushes.Red;
            //         break;
            //     case 1:
            //         myBrush = Brushes.Orange;
            //         break;
            //     case 2:
            //         myBrush = Brushes.Purple;
            //         break;
            //} 

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(((track)tracksList.Items[e.Index]).ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        //create track edit box by doubleclick
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void tracksList_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (tracksList.SelectedIndex != -1)
            {

                cd.Color = ((track)tracksList.Items[tracksList.SelectedIndex]).cor;
                cd.ShowDialog();


                ((track)tracksList.Items[tracksList.SelectedIndex]).cor = cd.Color;

                string nome = ((track)tracksList.Items[tracksList.SelectedIndex]).name;
                ShowInputDialog(ref nome);
                ((track)tracksList.Items[tracksList.SelectedIndex]).name = nome;

            }
            mplayer.Focus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);

            //drawHeatMap(nb, (track)tracksList.Items[tracksList.SelectedIndex]);

            //Graphics g = Graphics.FromImage(nb);

            track points = (track)tracksList.SelectedItem;

            
            if (System.IO.File.Exists(mplayer.URL))
            {
                if (System.IO.File.Exists(mplayer.URL + ".xml"))
                {
                    if (tracklistExist == true)
                    {
                        if (points.dataList.Count > 0)
                        {
                            // if (played == true)
                            // {



                            Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);

                            Graphics g = Graphics.FromImage(nb);

                            points.dataList.Sort((x, y) => x.timeStamp.CompareTo(y.timeStamp));

                            track timedPoints = new track();



                            timedPoints.cor = points.cor;
                            for (int i = 0; i < 5; i++)
                            {
                                timedPoints.dataList.Add(points.dataList[i]);
                            }
                            for (int i = 5; i < points.dataList.Count; i += 6)
                            {
                                g.Clear(Color.Transparent);
                                timedPoints.dataList.Add(points.dataList[i]);
                                timedPoints.drawTracjetory(nb, 0.00007);
                                //g.Clear(Color.Black);

                                int min = (int)(points.dataList[i].timeStamp / 60);
                                int sec = (int)points.dataList[i].timeStamp % 60;
                                g.DrawString("T: " + min.ToString("D2") + ":" + sec.ToString("D2"), new Font("Arial", 14), Brushes.White, nb.Width * 0.25f, nb.Height * 0.95f);


                                //transp form setup
                                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

                                int titleHeight = screenRectangle.Top - this.Top;
                                //sum 8 I simply dont know why, but it works
                                Point pt = new Point(this.Location.X + mplayer.Location.X + (int)bw + 8, this.Location.Y + mplayer.Location.Y + titleHeight + (int)bh);

                                transpImage.Location = pt;
                                transpImage.Size = mplayer.Size;
                                transpImage.SetBits(nb);
                                displayMap = true;

                            }
                            // }
                            //  else MessageBox.Show("Play the video at least one time before animate");
                        }
                        else MsgBoxTrackEmpty.Show("Your Track is Empty! Start Tracking Before Animate", "Warning Message", "OK");
                    }
                    else MsgBoxTrack.Show("        Create or Load a Track     ", "Warning Message", "OK" );
                    //else MessageBox.Show("Create or load a track");
                }
                else MsgBoxTrack.Show("You Need to Create a Track First", "Warning Message", "OK");
            }
            else MsgBoxVideo.Show("Please, Load a Video Using 'z'", "Warning Message", "OK");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void tracksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mplayer.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);

            //drawHeatMap(nb, (track)tracksList.Items[tracksList.SelectedIndex]);

            Graphics g = Graphics.FromImage(nb);

            double maxtime = 0;
            double mintime = double.MaxValue;

            int numberTracks = tracksList.Items.Count;

            List<track> points = new List<track>();
            for (int i = 0; i < numberTracks; i++)
            {

                points.Add((track)tracksList.Items[i]);
                points[i].dataList.Sort((x, y) => x.timeStamp.CompareTo(y.timeStamp));
                mintime = Math.Min(mintime, points[i].dataList[0].timeStamp);
                maxtime = Math.Max(maxtime, points[i].dataList[points[i].dataList.Count - 1].timeStamp);
            }

            List<track> timedPoints = new List<track>();
            for (int i = 0; i < numberTracks; i++)
            {
                timedPoints.Add(new track());
                timedPoints[i].cor = ((track)tracksList.Items[i]).cor;
            }

            int[] pos = new int[numberTracks];

            for (double time = mintime; time < maxtime; time++)
            {

                g.Clear(Color.Transparent);
                for (int i = 0; i < numberTracks; i++)
                {
                    while (points[i].dataList[pos[i]].timeStamp < time)
                    {
                        if (pos[i] >= (points[i].dataList.Count-1))
                        {
                            break;
                        }
                        else
                        {
                            pos[i]++;
                        }
                    }
                    timedPoints[i].dataList.Add(points[i].dataList[pos[i]]);
                    if (timedPoints[i].dataList.Count >= 5)
                    {
                        timedPoints[i].drawTracjetory(nb, 0.00007);
                    }
                }

                int min = (int)(time / 60);
                int sec = ((int)time) % 60;
                g.DrawString("T: " + min.ToString("D2") + ":" + sec.ToString("D2"), new Font("Arial", 14), Brushes.White, nb.Width * 0.25f, nb.Height * 0.95f);
                //transp form setup
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

                int titleHeight = screenRectangle.Top - this.Top;
                //sum 8 I simply dont know why, but it works
                Point pt = new Point(this.Location.X + mplayer.Location.X + (int)bw + 8, this.Location.Y + mplayer.Location.Y + titleHeight + (int)bh);

                transpImage.Location = pt;
                transpImage.Size = mplayer.Size;
                transpImage.SetBits(nb);
                displayMap = true;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Bitmap nb = new Bitmap(mplayer.Width - (int)(bw * 2), mplayer.Height - (int)(bh * 2) - 64);

            //drawHeatMap(nb, (track)tracksList.Items[tracksList.SelectedIndex]);

            Graphics g = Graphics.FromImage(nb);
            g.Clear(Color.Transparent);
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

            int titleHeight = screenRectangle.Top - this.Top;
            //sum 8 I simply dont know why, but it works
            Point pt = new Point(this.Location.X + mplayer.Location.X + (int)bw + 8, this.Location.Y + mplayer.Location.Y + titleHeight + (int)bh);

            transpImage.Location = pt;
            transpImage.Size = mplayer.Size;
            transpImage.SetBits(nb);
            displayMap = true;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Text = findDist ((track)tracksList.Items[tracksList.SelectedIndex]).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            deleteTrack();
        }

        private void mplayer_Enter(object sender, EventArgs e)
        {

        }
    }
}





public class trackPoint
{
    //point from 0 to 1 (width and heigh) 
    public System.Drawing.PointF point;
    //time syncronized with video.
    public double timeStamp;
    public trackPoint()
    {
    }
    public trackPoint(PointF nPoint, double nTime)
    {
        point = nPoint;
        timeStamp = nTime;
    }
}
//classe soh pra garantir serializacao da cor
public class XmlColor
{
    private Color color_ = Color.Black;

    public XmlColor() { }
    public XmlColor(Color c) { color_ = c; }


    public Color ToColor()
    {
        return color_;
    }

    public void FromColor(Color c)
    {
        color_ = c;
    }

    public static implicit operator Color(XmlColor x)
    {
        return x.ToColor();
    }

    public static implicit operator XmlColor(Color c)
    {
        return new XmlColor(c);
    }

    [XmlAttribute]
    public string Web
    {
        get { return ColorTranslator.ToHtml(color_); }
        set
        {
            try
            {
                if (Alpha == 0xFF) // preserve named color value if possible
                    color_ = ColorTranslator.FromHtml(value);
                else
                    color_ = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
            }
            catch (Exception)
            {
                color_ = Color.Black;
            }
        }
    }

    [XmlAttribute]
    public byte Alpha
    {
        get { return color_.A; }
        set
        {
            if (value != color_.A) // avoid hammering named color if no alpha change
                color_ = Color.FromArgb(value, color_);
        }
    }

    public bool ShouldSerializeAlpha() { return Alpha < 0xFF; }
}

public class track
{
    public string name;
    public List<trackPoint> dataList;
    [XmlElement(Type = typeof(XmlColor))]
    public Color cor;
    public track()
    {
        name = "Noname";
        dataList = new List<trackPoint>();
        cor = Color.Red;
    }
    public track(string nName)
    {
        name = nName;
        cor = Color.Red;
        dataList = new List<trackPoint>();
    }
    public override string ToString()
    {
        return name + " - " + dataList.Count.ToString();
    }
    public int[,] getHeatMatrix(int heightInPixels, int widthInPixels, int circleSize, int levels)
    {
        byte[,] heatCount = new byte[heightInPixels, widthInPixels];

        //      width
        //     
        //      +---------->
        //  h   |
        //  e   |
        //  i   |
        //  g   |
        //  h   |
        //  t   |
        //      v

        //init count
        for (int h = 0; h < heightInPixels; h++)
        {
            for (int w = 0; w < widthInPixels; w++)
            {
                heatCount[h, w] = 0;
            }
        }

        int[,] heatCountNeighbours = new int[heightInPixels, widthInPixels];

        //convert points list to map, using the given width and heigh
        for (int pt = 0; pt < dataList.Count; pt++)
        {
            if (dataList[pt].point.Y >= 0 &&
                dataList[pt].point.Y <= 1 &&
                dataList[pt].point.X >= 0 &&
                dataList[pt].point.X <= 1)
            {
                int h = (int)(dataList[pt].point.Y * heightInPixels);
                if (h >= heightInPixels)
                {
                    h = heightInPixels - 1;
                }
                int w = (int)(dataList[pt].point.X * widthInPixels);
                if (w >= widthInPixels)
                {
                    w = widthInPixels - 1;
                }
                heatCount[h, w]++;
                for (int adj_h = Math.Max(-circleSize + h, 0); adj_h < Math.Min(circleSize + h, heightInPixels); adj_h++)
                {
                    for (int adj_w = Math.Max(-circleSize + w, 0); adj_w < Math.Min(circleSize + w, widthInPixels); adj_w++)
                    {
                        //se estiver dentro do circulo soma.
                        if ((adj_h - h) * (adj_h - h) + (adj_w - w) * (adj_w - w) < circleSize * circleSize)
                        {
                            heatCountNeighbours[adj_h, adj_w]++;
                        }
                    }
                }
            }
        }


        ////count map, use linear cone from central point, incrementing each X ajacent cell, where X is the point count
        //for (int h = 0; h < heightInPixels; h++) {
        //    for (int w = 0; w < widthInPixels; w++) {
        //        if (heatCount[h, w]>0) {
        //            //for each point, create a circle summing the value
        //            for (int adj_h = Math.Max(-circleSize + h, 0); adj_h < Math.Min(circleSize + h, heightInPixels); adj_h++) {
        //                for (int adj_w = Math.Max(-circleSize + w, 0); adj_w < Math.Min(circleSize + w, widthInPixels); adj_w++) {
        //                    heatCountNeighbours[adj_h, adj_w] += heatCount[h, w];
        //                    ;
        //                }
        //            }
        //        }


        //        // pretty degrade circle with radius matching size of cone

        //        //for (int adj_h = Math.Max(-heatCount[h, w] + h, 0); adj_h < Math.Min(heatCount[h, w] + h, heightInPixels); adj_h++) {
        //        //    for (int adj_w = Math.Max(-heatCount[h, w] + w, 0); adj_w < Math.Min(heatCount[h, w] + w, widthInPixels); adj_w++) {
        //        //        heatCountNeighbours[adj_h, adj_w] += (float)Math.Max(0, heatCount[h, w] - (Math.Sqrt(
        //        //            Math.Pow((h - adj_h), 2) +
        //        //            Math.Pow((w - adj_w), 2))));
        //        //   } 
        //        //}
        //    }
        //}

        //calculate max to increase span
        float max = 0;
        for (int h = 0; h < heightInPixels; h++)
        {
            for (int w = 0; w < widthInPixels; w++)
            {
                max = Math.Max(heatCountNeighbours[h, w], max);
            }
        }
        //make colors more intense on the superior half
        max *= 0.8f;
        for (int i = 0; i < heightInPixels; i++)
        {
            for (int j = 0; j < widthInPixels; j++)
            {
                heatCountNeighbours[i, j] = Math.Min((((int)((heatCountNeighbours[i, j] / max) * levels)) * 255) / levels, 255);
            }
        }


        //create "border" on heatmap


        int[,] nHeat = new int[heightInPixels, widthInPixels];

        for (int i = 1; i < heightInPixels - 1; i++)
        {
            for (int j = 1; j < widthInPixels - 1; j++)
            {
                nHeat[i, j] = (byte)heatCountNeighbours[i, j];
                //for (int pi = -1; pi <= 1; pi++)
                //{
                //    for (int pj = -1; pj <= 1; pj++)
                //    {
                if ((heatCountNeighbours[i + 1, j] != heatCountNeighbours[i, j]) || (heatCountNeighbours[i, j + 1] != heatCountNeighbours[i, j]))
                {
                    nHeat[i, j] = 255 - heatCountNeighbours[i, j];
                    //pj = 2; pi = 2;
                }
                //    }
                //}
            }
        }

        return nHeat;
    }

    public void drawTracjetory(Bitmap bmp, double delta)
    {
        Color nC = Color.FromArgb(255, cor);
        Pen p = new Pen(nC, 2);
        Graphics g = Graphics.FromImage(bmp);
        //GraphicsPath gp = new GraphicsPath();
        //for (int pt = 4; pt < dataList.Count; pt+=4) {
        //    //Point p1 = pt2bmp(bmp, pt);

        //    // Point p2 = new Point();
        //    //p2.X = (int)(dataList[pt].point.X * bmp.Width);
        //    //p2.Y = (int)(dataList[pt].point.Y * bmp.Height);

        //    gp.AddBezier(pt2bmp(bmp, pt), pt2bmp(bmp, pt - 1), pt2bmp(bmp, pt - 2), pt2bmp(bmp, pt - 3));
        //}
        List<trackPoint> lp = new List<trackPoint>();


        double minTime = double.MaxValue;
        double maxTime = 0;
        int pt;
        for (pt = 1; pt < dataList.Count; pt++)
        {
            if (dist(dataList[pt - 1].point, dataList[pt + 0].point) > delta)
            {
                lp.Add(dataList[pt]);
                minTime = Math.Min(minTime, dataList[pt].timeStamp);
                maxTime = Math.Max(maxTime, dataList[pt].timeStamp);
            }

        }

        for (pt = 0; pt < lp.Count - 4; pt += 3)
        {
            //double cgRed = (pt + 0.0) / (lp.Count);//valor entre 0 e 1;
            //double cgBlue = cgRed;
            //double cgGreen = cgRed;



            //cgRed = Math.Min(510 * (1 - cgRed), 255);
            //cgGreen = Math.Min(510 * cgGreen, 255);
            //cgBlue = 0 * cgBlue;

            Color nColor;
            //nColor = Color.FromArgb(((int)cgRed) % 256, ((int)cgGreen) % 256, ((int)cgBlue) % 256);
            nColor = this.cor;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawBezier(new Pen(nColor, 2), pt2bmp(bmp, lp[pt + 0]), pt2bmp(bmp, lp[pt + 1]), pt2bmp(bmp, lp[pt + 2]), pt2bmp(bmp, lp[pt + 3]));

        }

        if (lp.Count >= 4)
        {
            //trackPoint ini = new trackPoint();
            //ini.point.X = lp[0].point.X-5;
            //ini.point.Y = lp[0].point.Y-5;
            PointF ini = pt2bmp(bmp, lp[0]);
            ini.X -= 10;
            ini.Y -= 10;

            PointF fim = pt2bmp(bmp, lp[pt]);
            fim.X -= 10;
            fim.Y -= 10;

            g.FillRectangle(new SolidBrush(this.cor), new RectangleF(ini, new SizeF(20, 20)));
            g.FillEllipse(new SolidBrush(this.cor), new RectangleF(fim, new SizeF(20, 20)));

        }


        //change the reference to the bitmap size
        //for (int pt = 0; pt < (dataList.Count - 1); pt++)
        //{
        //    if (dist(dataList[pt + 0].point, dataList[pt + 1].point) > 0.00007)
        //    {
        //        lp.Add(pt2bmp(bmp, pt));
        //        minTime = Math.Min(minTime, dataList[pt].timeStamp);
        //        maxTime = Math.Max(maxTime, dataList[pt].timeStamp);
        //    }

        //    // Point p2 = new Point();
        //    //p2.X = (int)(dataList[pt].point.X * bmp.Width);
        //    //p2.Y = (int)(dataList[pt].point.Y * bmp.Height);

        //}

        //gp.AddCurve(lp.ToArray());

        //-4 becouse splint


        // g.DrawPath(p, gp);

    }

    private double dist(PointF one, PointF two)
    {
        double x = one.X - two.X;
        double y = one.Y - two.Y;

        return x * x + y * y;
    }

    private PointF pt2bmp(Bitmap bmp, trackPoint pt)
    {

        PointF p1 = new PointF();

        p1.X = (pt.point.X * bmp.Width);
        p1.Y = (pt.point.Y * bmp.Height);
        return p1;
    }
}



public partial class TranspForm : Form
{
    bool haveHandle = false;
    public TranspForm()
    {
        this.SuspendLayout();
        // 
        // FishForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(1F, 1F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1, 1);
        this.DoubleBuffered = true;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "transpForm";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.Text = "transp_form";
        this.ResumeLayout(false);

        this.TopMost = true;

        bitmap = new Bitmap(1, 1);
        this.Width = 1;
        this.Height = 1;
    }

    Bitmap bitmap;

    #region Override
    protected override void OnHandleCreated(EventArgs e)
    {
        InitializeStyles();
        base.OnHandleCreated(e);
        haveHandle = true;
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cParms = base.CreateParams;
            cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
            return cParms;
        }
    }
    #endregion

    private void InitializeStyles()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);
        UpdateStyles();
    }

    public void SetBits(Bitmap bitmap)
    {
        if (!haveHandle)
            return;

        if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
            throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

        IntPtr oldBits = IntPtr.Zero;
        IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
        IntPtr hBitmap = IntPtr.Zero;
        IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

        try
        {
            Win32.Point topLoc = new Win32.Point(Left, Top);
            Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
            Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
            Win32.Point srcLoc = new Win32.Point(0, 0);

            hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
            oldBits = Win32.SelectObject(memDc, hBitmap);

            blendFunc.BlendOp = Win32.AC_SRC_OVER;
            blendFunc.SourceConstantAlpha = 255;
            blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
            blendFunc.BlendFlags = 0;

            Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
        }
        finally
        {
            if (hBitmap != IntPtr.Zero)
            {
                Win32.SelectObject(memDc, oldBits);
                Win32.DeleteObject(hBitmap);
            }
            Win32.ReleaseDC(IntPtr.Zero, screenDC);
            Win32.DeleteDC(memDc);
        }
    }

    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
}
using MemoryGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private static IEnumerable<Image> sliki;
        private bool _allowClick = true;
        private PictureBox _firstGuess;
        private readonly Random _random = new Random();
        private readonly Timer _clickTimer = new Timer();
        private bool isFinished = false;
        private bool musicON = true;
        int ticks = 60;
        private SoundPlayer player;
        private bool Sound { get; set; }
        Timer timer = new Timer { Interval = 1000};
        public Form1()
        {
            InitializeComponent();
        }
        private PictureBox[] PictureBoxes
        {
            get {
                PictureBox[] lista = new PictureBox[16];
                lista[0]=pictureBox1;
                lista[1] = pictureBox2;
                lista[2] = pictureBox3;
                lista[3] = pictureBox4;
                lista[4] = pictureBox5;
                lista[5] = pictureBox6;
                lista[6] = pictureBox7;
                lista[7] = pictureBox8;
                lista[8] = pictureBox9;
                lista[9] = pictureBox10;
                lista[10] = pictureBox11;
                lista[11] = pictureBox12;
                lista[12] = pictureBox13;
                lista[13] = pictureBox14;
                lista[14] = pictureBox15;
                lista[15] = pictureBox16;
                return lista;
            }
        }
      
        private void StartGameTimer()
        {
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ticks--;
            if (ticks == -1)
            {
                timer.Stop();
                timer.Enabled = false;
                player.Stop();
                MessageBox.Show("Times up. Better luck next time !", "Memory Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                timer.Stop();
                tabControl1.SelectedTab = tabMenu;
                ResetImages();
            }
            if(isFinished)
            {
                timer.Stop();
                player.Stop();
                MessageBox.Show("Congratulations, You Won!", "Memory Game", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                tabControl1.SelectedTab = tabMenu;
                lblTime.Text = "01:00";
                ResetImages();
                isFinished = false;
            }
            int left = ticks;
            int min = left / 60;
            int sec = left % 60;
            lblTime.Text = string.Format("{0:00}:{1:00}", min, sec);
        }

        private void ResetImages()
        {
            foreach(var pic in PictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }
           // HideImages();
           // SetRandomImages();
           ticks = 60;
           // timer.Stop();
        }
        private void HideImages()
        {
            foreach(var pic in PictureBoxes)
            {
                pic.Image=Resources.question;
            }
        }
        private PictureBox GetFreeSlot()
        {
            int num;
            do
            {
                num = _random.Next(0, PictureBoxes.Count());
            }
            while (PictureBoxes[num].Tag != null);
            return PictureBoxes[num];
        }
        private void SetRandomImages()
        {
            foreach(var image in sliki)
            {
                GetFreeSlot().Tag = image;
                GetFreeSlot().Tag = image;
            }
        }
        private async void ClickImage(object sender, EventArgs e)
        {
            if (!_allowClick)
            {
                return;
            }
            var pic = (PictureBox)sender;
            if (_firstGuess == null)
            {
                _firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }
            pic.Image = (Image)pic.Tag;
            if (pic.Image == _firstGuess.Image && pic != _firstGuess)
            {
                await Task.Delay(250);
                pic.Visible = _firstGuess.Visible = false;
                {
                    _firstGuess = pic;
                }
                HideImages();
            }
            else
            {
                _allowClick = false;
                _clickTimer.Start();
            }
            _firstGuess = null;
            if (PictureBoxes.Any(p => p.Visible)) return;
            isFinished = true;           
        }
        private void _clickTimer_Tick(object sender,EventArgs e)
        {
            HideImages();
            _allowClick = true;
            _clickTimer.Stop();
        }

        private void btnPokemon_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox17.Image =Resources.pok4;
        }

        private void btnScoo_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox17.Image = Resources.img2;
        }

        private void btnPokemon_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Image = null;
        }

        private void btnScoo_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Image = null;
        }

        private void btnYuGiOh_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox17.Image =Resources.yu1;
        }

        private void btnYuGiOh_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Image = null;
        }

        private void btnTMNT_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox17.Image = Resources.tmnt5;
        }

        private void btnTMNT_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Image = null;
        }

        private void btnWinx_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox17.Image =Resources.winx1;
        }

        private void btnWinx_MouseLeave(object sender, EventArgs e)
        {
            pictureBox17.Image = null;
        }

        private void btnPokemon_Click(object sender, EventArgs e)
        {
            sliki=Images("pok");
            SetRandomImages();
            HideImages();
            tabControl1.SelectedTab = tabPage2;
            player = new SoundPlayer(@"../../Resources/PokemonMusic.wav");
            player.PlayLooping();
            Sound = true;
            timer = new Timer { Interval = 1000 };
            StartGameTimer();
            _clickTimer.Interval = 300;
            _clickTimer.Tick += _clickTimer_Tick;
        }
        private  IEnumerable<Image> Images(string category)
        {
            if(category=="pok")
            {
                return new Image[]
                {
                Resources.pok1,
                Resources.pok2,
                Resources.pok3,
                Resources.pok4,
                Resources.pok5,
                Resources.pok6,
                Resources.pok7,
                Resources.pok8
                };
            }
            else if (category == "sco")
            {
                return new Image[]
                {
                Resources.img1,
                Resources.img2,
                Resources.img3,
                Resources.img4,
                Resources.img5,
                Resources.img6,
                Resources.img7,
                Resources.img8
                };
            }
            else if (category == "winx")
            {
                return new Image[]
                {
                Resources.winx1,
                Resources.winx2,
                Resources.winx3,
                Resources.winx4,
                Resources.winx5,
                Resources.winx6,
                Resources.winx7,
                Resources.winx8
                };
            }
            else if (category == "yugioh")
            {
                return new Image[]
                {
                Resources.yu1,
                Resources.yu2,
                Resources.yu3,
                Resources.yu4,
                Resources.yu5,
                Resources.yu6,
                Resources.yu7,
                Resources.yu8
                };
            }
            else
            {
               return new Image[]
                 {
                Resources.tmnt1,
                Resources.tmnt2,
                Resources.tmnt3,
                Resources.tmnt4,
                Resources.tmnt5,
                Resources.tmnt6,
                Resources.tmnt7,
                Resources.tmnt8
                    };
                
            }
        }
        private void btnScoo_Click(object sender, EventArgs e)
        {
            sliki = Images("sco");
            SetRandomImages();
            HideImages();
            tabControl1.SelectedTab = tabPage2;
            player = new SoundPlayer(@"../../Resources/ScoobyDooMusic.wav");
            player.PlayLooping();
            Sound = true;
            timer = new Timer { Interval = 1000 };
            StartGameTimer();   
            _clickTimer.Interval = 300;
            _clickTimer.Tick += _clickTimer_Tick;
        }

        private void btnYuGiOh_Click(object sender, EventArgs e)
        {
            sliki = Images("yugioh");
            SetRandomImages();
            HideImages();
            tabControl1.SelectedTab = tabPage2;
            player = new SoundPlayer(@"../../Resources/YuGiOhMusic.wav");
            player.PlayLooping();
            Sound = true;
            timer = new Timer { Interval = 1000 };
            StartGameTimer();          
            _clickTimer.Interval = 300;
            _clickTimer.Tick += _clickTimer_Tick;
        }

        private void btnTMNT_Click(object sender, EventArgs e)
        {
            sliki = Images("tmnt");
            SetRandomImages();
            HideImages();
            tabControl1.SelectedTab = tabPage2;
            player = new SoundPlayer(@"../../Resources/TMNTMusic.wav");
            player.PlayLooping();
            Sound = true;
            timer = new Timer { Interval = 1000 };
            StartGameTimer();       
            _clickTimer.Interval = 300;
            _clickTimer.Tick += _clickTimer_Tick;
        }

        private void btnWinx_Click(object sender, EventArgs e)
        {
            sliki = Images("winx");
            SetRandomImages();
            HideImages();
            tabControl1.SelectedTab = tabPage2;
            player = new SoundPlayer(@"../../Resources/WinxMusic.wav");
            player.PlayLooping();
            Sound = true;
            timer = new Timer { Interval = 1000 };
            StartGameTimer();         
            _clickTimer.Interval =300;
            _clickTimer.Tick += _clickTimer_Tick;
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            if(musicON)
            {
                musicON = !musicON;
                pictureBox18.Image = Resources.off;
                player.Stop();
            }
            else
            {
                musicON = !musicON;
                pictureBox18.Image = Resources.on;
                player.PlayLooping();
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            timer.Stop();
            player.Stop();
            tabControl1.SelectedTab = tabMenu;
            lblTime.Text = "01:00";
            ResetImages();
            isFinished = false;
        }

        private void pictureBox19_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox19.Cursor = Cursors.Hand;
        }

        private void pictureBox19_MouseLeave(object sender, EventArgs e)
        {
            pictureBox19.Cursor = Cursors.Default;
        }

        private void pictureBox18_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox18.Cursor = Cursors.Hand;
        }

        private void pictureBox18_MouseLeave(object sender, EventArgs e)
        {
            pictureBox19.Cursor = Cursors.Default;
        }
    }
}

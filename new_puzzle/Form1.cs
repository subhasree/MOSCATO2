using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureTiles
{
    public partial class Form1 : Form
    {
        string playerPath;
        public Form1(string path)
        {
            playerPath = path;
            InitializeComponent();
            ofdPicture.Multiselect = true;
            //ofdPicture.InitialDirectory = path;
            //ofdPicture.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Users";
            string dir1 = System.IO.Directory.GetCurrentDirectory();
            //MessageBox.Show(dir1);
            string folderLocation1 = dir1 + "\\PUZZLE\\";
            //ofdPicture.InitialDirectory = System.IO.Path.Combine(dir1, "\\PUZZLE");
            ofdPicture.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), folderLocation1);
            //ofdPicture.InitialDirectory = "%HOMEDRIVE%%HOMEPATH%\\Dropbox\\PUZZLE";
            //MessageBox.Show(ofdPicture.InitialDirectory.ToString());
            Console.WriteLine(ofdPicture.InitialDirectory.ToString());

            this.CenterToScreen();
        }

        // The current full picture.
        private Bitmap FullPicture = null;
        private Bitmap[] FullPictures = new Bitmap[6];
        private Graphics[] grs = new Graphics[6];

        // The board's background.
        private Bitmap Background = null;

        // The board.
        private Bitmap Board = null;

        // The pieces.
        private List<Piece> Pieces = null;

        // The target size. (Initially easy.)
        //private int TargetSize = 300;

        // The number and size of the rows and columns.
        private int NumRows = 2, NumCols = 3, RowHgt = 300, ColWid = 300;

        private int MainWidth = 900, MainHeight = 600;

        // The piece the user is moving.
        private Piece MovingPiece = null;
        private Point MovingPoint;

        // True when the game is over.
        private bool GameOver = true;

        // Exit.
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Open a file.
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (ofdPicture.ShowDialog() == DialogResult.OK)
            {
                lblstart.Visible = false;
                int cnt = 0;
                //StreamWriter writer = File.AppendText(playerPath + "\\fileTracker.csv");
                //foreach (String file in ofdPicture.FileNames)
                //{
                //    DateTime now = DateTime.Now;
                //    string timeOfNow;
                //    timeOfNow = now.ToString("yyyy.MM.dd  HH.mm.ss.ffff");
                //    writer.WriteLine(timeOfNow + "," + file);
                //    writer.Close();
                //    LoadPicture2(file, cnt);
                //    cnt++;
                //}

                if (File.Exists(playerPath + "\\fileTracker.csv"))
                {
                    // MessageBox.Show("Exists!");
                    using (System.IO.StreamWriter writer2 = File.AppendText(playerPath + "\\fileTracker.csv"))
                    {
                        foreach (String file in ofdPicture.FileNames)
                        {
                            DateTime now = DateTime.Now;
                            string timeOfNow;
                            timeOfNow = now.ToString("yyyy.MM.dd  HH.mm.ss.ffff");
                            writer2.Write(timeOfNow + "," + file);
                            LoadPicture2(file, cnt);
                            writer2.Write(System.Environment.NewLine);
                            cnt++;
                        }
                        writer2.Close();
                    }
                }
                else
                {
                    using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite(playerPath + "\\fileTracker.csv")))
                    {
                        foreach (String file in ofdPicture.FileNames)
                        {
                            DateTime now = DateTime.Now;
                            string timeOfNow;
                            timeOfNow = now.ToString("yyyy.MM.dd  HH.mm.ss.ffff");
                            writer2.Write(timeOfNow + "," + file);
                            LoadPicture2(file, cnt);
                            writer2.Write(System.Environment.NewLine);
                            cnt++;
                        }
                        writer2.Close();
                    }
                }

                if (cnt<6)
                {
                    for(int a=cnt; a<6; a++)
                    {
                        FullPictures[a] = new Bitmap(300, 300);

                        using (Graphics gr = Graphics.FromImage(FullPictures[a]))
                        {
                            Rectangle tmprec = new Rectangle(0, 0, 300, 300);
                            gr.FillRectangle(Brushes.White, tmprec);
                        }

                    }
                }

                FullPicture = new Bitmap(MainWidth, MainHeight);

                int cnt2 = 0;
                for(int i=0; i<NumRows; i++)
                {
                    for(int j=0; j<NumCols; j++)
                    {
                        using (Graphics gr = Graphics.FromImage(FullPicture))
                        {
                            gr.DrawImage(FullPictures[cnt2], j*300, i*300);
                            cnt2++;
                        }
                    }
                }

                Background = new Bitmap(MainWidth, MainHeight);
                Board = new Bitmap(MainWidth, MainHeight);
                picPuzzle.Size = FullPicture.Size;
                picPuzzle.Image = Board;
                this.ClientSize = new Size(
                    picPuzzle.Right + picPuzzle.Left,
                    picPuzzle.Bottom + picPuzzle.Left);
                //LoadPicture(ofdPicture.FileName);
                //FullPicture = FullPictures[3];
                //StartGame2();
                StartGame();

            }
        }


        private void LoadPicture2(string filename, int picno)
        {
            try
            {
                // Load the picture.
                using (Bitmap bm = new Bitmap(filename))
                {
                    FullPictures[picno] = new Bitmap(300, 300);
                    using (Graphics gr = Graphics.FromImage(FullPictures[picno]))
                    {
                        gr.DrawImage(bm, 0, 0);
                    }
                }

                // Make the Board and background bitmaps.
                // Start a new game.
                //StartGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Load this file.
        private void LoadPicture(string filename)
        {
            try
            {
                // Load the picture.
                using (Bitmap bm = new Bitmap(ofdPicture.FileNames[0]))
                {
                    FullPicture = new Bitmap(MainWidth, MainHeight);
                    using (Graphics gr = Graphics.FromImage(FullPicture))
                    {
                        gr.DrawImage(bm, 0, 0);
                    }
                }

                // Make the Board and background bitmaps.
                Background = new Bitmap(MainWidth, MainHeight);
                Board = new Bitmap(MainWidth, MainHeight);
                picPuzzle.Size = FullPicture.Size;
                picPuzzle.Image = Board;
                this.ClientSize = new Size(
                    picPuzzle.Right + picPuzzle.Left,
                    picPuzzle.Bottom + picPuzzle.Left);

                // Start a new game.
                StartGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Select the level.
        private void StartGame2()
        {
            //if (FullPicture == null) return;
            GameOver = false;

            // Figure out how big the pieces should be.
            //NumRows = FullPicture.Height / TargetSize;
            //RowHgt = FullPicture.Height / NumRows;
            //NumCols = FullPicture.Width / TargetSize;
            //ColWid  = FullPicture.Width / NumCols;

            // Make the pieces.
            Random rand = new Random();
            Pieces = new List<Piece>();
            for (int row = 0; row < NumRows; row++)
            {
                int hgt = RowHgt;
                if (row == NumRows - 1) hgt = FullPicture.Height - row * RowHgt;
                for (int col = 0; col < NumCols; col++)
                {
                    int wid = ColWid;
                    if (col == NumCols - 1) wid = FullPicture.Width - col * ColWid;
                    Rectangle rect = new Rectangle(col * ColWid, row * RowHgt, wid, hgt);
                    Piece new_piece = new Piece(FullPicture, rect);

                    // Randomize the initial location.
                    new_piece.CurrentLocation = new Rectangle(
                        rand.Next(0, FullPicture.Width - wid),
                        rand.Next(0, FullPicture.Height - hgt),
                        wid, hgt);

                    // Add to the Pieces collection.
                    Pieces.Add(new_piece);
                }
            }

            // Make the background.
            MakeBackground2();

            // Draw the board.
            DrawBoard2();
        }

        // Start a new game.
        private void StartGame()
        {
            if (FullPicture == null) return;
            GameOver = false;

            // Figure out how big the pieces should be.
            //NumRows = FullPicture.Height / TargetSize;
            //RowHgt = FullPicture.Height / NumRows;
            //NumCols = FullPicture.Width / TargetSize;
            //ColWid  = FullPicture.Width / NumCols;

            // Make the pieces.
            Random rand = new Random();
            Pieces = new List<Piece>();
            for (int row = 0; row < NumRows; row++)
            {
                int hgt = RowHgt;
                if (row == NumRows - 1) hgt = FullPicture.Height - row * RowHgt;
                for (int col = 0; col < NumCols; col++)
                {
                    int wid = ColWid;
                    if (col == NumCols - 1) wid = FullPicture.Width - col * ColWid;
                    Rectangle rect = new Rectangle(col * ColWid, row * RowHgt, wid, hgt);
                    Piece new_piece = new Piece(FullPicture, rect);

                    // Randomize the initial location.
                    new_piece.CurrentLocation = new Rectangle(
                        rand.Next(0, FullPicture.Width - wid),
                        rand.Next(0, FullPicture.Height - hgt),
                        wid, hgt);

                    // Add to the Pieces collection.
                    Pieces.Add(new_piece);
                }
            }

            // Make the background.
            MakeBackground();

            // Draw the board.
            DrawBoard();
        }


        private void MakeBackground2()
        {
            using (Graphics gr = Graphics.FromImage(Background))
            {
                gr.Clear(picPuzzle.BackColor);

                // Draw a grid on the background.
                DrawGrid2(gr);

                // Draw the pieces.
                DrawPieces2(gr);
            }

            picPuzzle.Visible = true;
            picPuzzle.Refresh();
        }


        // Make the background image without MovingPiece.
        private void MakeBackground()
        {
            using (Graphics gr = Graphics.FromImage(Background))
            {
                gr.Clear(picPuzzle.BackColor);

                // Draw a grid on the background.
                DrawGrid(gr);

                // Draw the pieces.
                DrawPieces(gr);
            }

            picPuzzle.Visible = true;
            picPuzzle.Refresh();
        }

        private void DrawGrid2(Graphics gr)
        {
            using (Pen thick_pen = new Pen(Color.DarkGray, 4))
            {
                for (int y = 0; y <= MainHeight; y += RowHgt)
                {
                    gr.DrawLine(thick_pen, 0, y, MainWidth, y);
                }
                gr.DrawLine(thick_pen, 0, MainHeight, MainWidth, MainHeight);

                for (int x = 0; x <= MainWidth; x += ColWid)
                {
                    gr.DrawLine(thick_pen, x, 0, x, MainHeight);
                }
                gr.DrawLine(thick_pen, MainWidth, 0, MainWidth, MainHeight);
            }
        }


        // Draw a grid on the background.
        private void DrawGrid(Graphics gr)
        {
            using (Pen thick_pen = new Pen(Color.DarkGray, 4))
            {
                for (int y = 0; y <= FullPicture.Height; y += RowHgt)
                {
                    gr.DrawLine(thick_pen, 0, y, FullPicture.Width, y);
                }
                gr.DrawLine(thick_pen, 0, FullPicture.Height, FullPicture.Width, FullPicture.Height);

                for (int x = 0; x <= FullPicture.Width; x += ColWid)
                {
                    gr.DrawLine(thick_pen, x, 0, x, FullPicture.Height);
                }
                gr.DrawLine(thick_pen, FullPicture.Width, 0, FullPicture.Width, FullPicture.Height);
            }
        }

        private void DrawPieces2(Graphics gr)
        {
            using (Pen white_pen = new Pen(Color.White, 3))
            {
                using (Pen black_pen = new Pen(Color.Black, 3))
                {
                    foreach (Piece piece in Pieces)
                    {
                        // Don't draw the piece we are moving.
                        if (piece != MovingPiece)
                        {
                            gr.DrawImage(FullPicture,
                                piece.CurrentLocation,
                                piece.HomeLocation,
                                GraphicsUnit.Pixel);
                            if (!GameOver)
                            {
                                if (piece.IsHome())
                                {
                                    // Draw locked pieces with a white border.
                                    //gr.DrawRectangle(white_pen, piece.CurrentLocation);
                                }
                                else
                                {
                                    // Draw locked pieces with a black border.
                                    //gr.DrawRectangle(black_pen, piece.CurrentLocation);
                                }
                            }
                        }
                    }
                }
            }
        }


        // Draw the pieces.
        private void DrawPieces(Graphics gr)
        {
            using (Pen white_pen = new Pen(Color.White, 3))
            {
                using (Pen black_pen = new Pen(Color.Black, 1))
                {
                    foreach (Piece piece in Pieces)
                    {
                        // Don't draw the piece we are moving.
                        if (piece != MovingPiece)
                        {
                            gr.DrawImage(FullPicture,
                                piece.CurrentLocation,
                                piece.HomeLocation,
                                GraphicsUnit.Pixel);
                            if (!GameOver)
                            {
                                if (piece.IsHome())
                                {
                                    // Draw locked pieces with a white border.
                                    //gr.DrawRectangle(white_pen, piece.CurrentLocation);
                                }
                                else
                                {
                                    // Draw locked pieces with a black border.
                                    //gr.DrawRectangle(black_pen, piece.CurrentLocation);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Draw the board.
        private void DrawBoard2()
        {
            using (Graphics gr = Graphics.FromImage(Board))
            {
                // Restore the background.
                gr.DrawImage(Background, 0, 0, Background.Width, Background.Height);

                // Draw MovingPiece.
                if (MovingPiece != null)
                {
                    gr.DrawImage(FullPicture,
                        MovingPiece.CurrentLocation,
                        MovingPiece.HomeLocation,
                        GraphicsUnit.Pixel);

                    using (Pen blue_pen = new Pen(Color.Blue, 4))
                    {
                        //gr.DrawRectangle(blue_pen, MovingPiece.CurrentLocation);
                    }
                }
            }

            picPuzzle.Visible = true;
            picPuzzle.Refresh();
        }

        private void DrawBoard()
        {
            using (Graphics gr = Graphics.FromImage(Board))
            {
                // Restore the background.
                gr.DrawImage(Background, 0, 0, Background.Width, Background.Height);

                // Draw MovingPiece.
                if (MovingPiece != null)
                {
                    gr.DrawImage(FullPicture,
                        MovingPiece.CurrentLocation,
                        MovingPiece.HomeLocation,
                        GraphicsUnit.Pixel);

                    using (Pen blue_pen = new Pen(Color.Blue, 4))
                    {
                        //gr.DrawRectangle(blue_pen, MovingPiece.CurrentLocation);
                    }
                }
            }

            picPuzzle.Visible = true;
            picPuzzle.Refresh();
        }



        // Start moving a piece.
        private void picPuzzle_MouseDown(object sender, MouseEventArgs e)
        {
            // See which piece contains this point.
            // Skip fixed pieces.
            // Keep the last one because it's on the top.
            MovingPiece = null;
            foreach (Piece piece in Pieces)
            {
                if (!piece.IsHome() && piece.Contains(e.Location))
                    MovingPiece = piece;
            }
            if (MovingPiece == null) return;

            // Save this location.
            MovingPoint = e.Location;

            // Move it to the top of the stack.
            Pieces.Remove(MovingPiece);
            Pieces.Add(MovingPiece);

            // Redraw.
            MakeBackground();
            DrawBoard();
        }

        // Move the selected piece.
        private void picPuzzle_MouseMove(object sender, MouseEventArgs e)
        {
            if (MovingPiece == null) return;

            // Move the piece.
            int dx = e.X - MovingPoint.X;
            int dy = e.Y - MovingPoint.Y;
            MovingPiece.CurrentLocation.X += dx;
            MovingPiece.CurrentLocation.Y += dy;

            // Save the new mouse location.
            MovingPoint = e.Location;

            // Redraw.
            DrawBoard();
        }

        // Stop moving the piece and see if it is where it belongs.
        private void picPuzzle_MouseUp(object sender, MouseEventArgs e)
        {
            if (MovingPiece == null) return;

            // See if the piece is in its home position.
            if (MovingPiece.SnapToHome())
            {
                // Move the piece to the bottom.
                Pieces.Remove(MovingPiece);
                Pieces.Reverse();
                Pieces.Add(MovingPiece);
                Pieces.Reverse();

                // See if the game is over.
                GameOver = true;
                foreach (Piece piece in Pieces)
                {
                    if (!piece.IsHome())
                    {
                        GameOver = false;
                        break;
                    }
                }
            }

            // Stop moving the piece.
            MovingPiece = null;

            // Redraw.
            MakeBackground();
            DrawBoard();
        }
    }
}

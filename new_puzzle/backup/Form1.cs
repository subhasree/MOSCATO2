using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureTiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The current full picture.
        private Bitmap FullPicture = null;

        // The board's background.
        private Bitmap Background = null;

        // The board.
        private Bitmap Board = null;

        // The pieces.
        private List<Piece> Pieces = null;

        // The target size. (Initially easy.)
        private int TargetSize = 100;

        // The number and size of the rows and columns.
        private int NumRows, NumCols, RowHgt, ColWid;

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
                LoadPicture(ofdPicture.FileName);
            }
        }

        // Load this file.
        private void LoadPicture(string filename)
        {
            try
            {
                // Load the picture.
                using (Bitmap bm = new Bitmap(ofdPicture.FileName))
                {
                    FullPicture = new Bitmap(bm.Width, bm.Height);
                    using (Graphics gr = Graphics.FromImage(FullPicture))
                    {
                        gr.DrawImage(bm, 0, 0, bm.Width, bm.Height);
                    }
                }

                // Make the Board and background bitmaps.
                Background = new Bitmap(FullPicture.Width, FullPicture.Height);
                Board = new Bitmap(FullPicture.Width, FullPicture.Height);
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
        private void mnuOptionsLevel_Click(object sender, EventArgs e)
        {
            // Check and uncheck the items.
            ToolStripMenuItem mnu = sender as ToolStripMenuItem;
            mnuOptionsEasy.Checked = (mnu == mnuOptionsEasy);
            mnuOptionsMedium.Checked = (mnu == mnuOptionsMedium);
            mnuOptionsHard.Checked = (mnu == mnuOptionsHard);

            // Get the target size.
            TargetSize = int.Parse((string)mnu.Tag);

            // Start a new game.
            StartGame();
        }

        // Start a new game.
        private void StartGame()
        {
            if (FullPicture == null) return;
            GameOver = false;

            // Figure out how big the pieces should be.
            NumRows = FullPicture.Height / TargetSize;
            RowHgt = FullPicture.Height / NumRows;
            NumCols = FullPicture.Width / TargetSize;
            ColWid  = FullPicture.Width / NumCols;

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

        // Draw the pieces.
        private void DrawPieces(Graphics gr)
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
                                    gr.DrawRectangle(white_pen, piece.CurrentLocation);
                                }
                                else
                                {
                                    // Draw locked pieces with a black border.
                                    gr.DrawRectangle(black_pen, piece.CurrentLocation);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Draw the board.
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
                        gr.DrawRectangle(blue_pen, MovingPiece.CurrentLocation);
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

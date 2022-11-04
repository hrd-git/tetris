using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Tetris;

namespace Tetris
{
    public partial class TetrisControl : Form
    {
        private Boolean IsCanMove = false;
        private Boolean IsStockBlock = false;
        private int[][] field = new int[Constants.FIELD_ARRAY_HEIGHT][];
        private int[][] nextBlockField = new int[Constants.ALL_NEXT_BLOCK_FIELD_HEIGHT][];
        private int[][] stockBlockField = new int[Constants.BLOCK_FIELD_ARRAY_LENGTH][];
        private int[][] block;
        private int[][] stockBlock;
        private Queue<int[][]> allNextBlock = new Queue<int[][]>();
        private int X_POS;
        private int Y_POS;
        private Graphics TetrisFieldGraphics;
        private Graphics NextBlockGraphics;
        private Graphics StockBlockGraphics;
        private TetrisCreateView view;

        //フォームのコンストラクタ
        public TetrisControl()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.TetrisFieldGraphics = this.TetrisField.CreateGraphics();
            this.NextBlockGraphics = this.NextBlockField.CreateGraphics();
            this.StockBlockGraphics = this.StockBlockField.CreateGraphics();
            this.view = new TetrisCreateView();
        }

        //テトリスを起動する
        private void TetrisInit(object sender, EventArgs e)
        {
            this.view.CreateField(this.field, Constants.FIELD_ARRAY_WIDTH, Constants.FIELD_ARRAY_HEIGHT);
            this.view.CreateField(this.nextBlockField, Constants.BLOCK_FIELD_ARRAY_LENGTH, Constants.ALL_NEXT_BLOCK_FIELD_HEIGHT);
            this.view.CreateField(this.stockBlockField, Constants.BLOCK_FIELD_ARRAY_LENGTH, Constants.BLOCK_FIELD_ARRAY_LENGTH);
            this.view.ShowField(this.field, this.TetrisFieldGraphics);
            this.view.ShowField(this.nextBlockField, this.NextBlockGraphics);
            this.view.ShowField(this.stockBlockField, this.StockBlockGraphics);
            this.Start.Stop();
        }

        //テトリスの初期化処理
        private void Reset()
        {
            this.X_POS = Constants.X_POS;
            this.Y_POS = Constants.Y_POS;
            this.CreateNextBlock();
            this.view.ClearBlock(ref this.stockBlockField);
            this.view.WriteBlockOnField(this.field, this.block, this.X_POS, this.Y_POS);
            this.view.WriteNextBlockOnField(this.nextBlockField, this.allNextBlock, 1, 1);
            this.view.ShowField(this.field, this.TetrisFieldGraphics);
            this.view.ShowField(this.nextBlockField, this.NextBlockGraphics);
            this.view.ShowField(this.stockBlockField, this.StockBlockGraphics);
        }

        //スタートボタンでテトリスを始める
        private void StartButton_Click(object sender, EventArgs e)
        {
            this.ResetButton.Enabled = true;
            this.IsCanMove = true;
            this.IsStockBlock = false;
            this.GameOverText.Visible = false;
            this.StartButton.Enabled = false;
            this.view.CreateAllNextBlock(this.allNextBlock, Constants.NEXT_BLOCK_NUMBER);
            this.block = this.view.CreateBlock();
            this.Reset();
            this.Drop.Start();
        }

        //ブロックを一定時間ごとに下に落とす
        private void DropDownTick(object sender, EventArgs e)
        {

            if (!this.IsMoveHit(this.block, 0, 1))
            {
                 this.Y_POS++;
            }
            else
            {
                this.FixedBlock();
                //ゲームオーバーの処理
                //停止位置がyの初期値かつ他のブロックに接触している場合
                if (this.Y_POS == Constants.Y_POS &&
                         this.IsMoveHit(this.block, 0, 0))
                {
                    this.GameOver();
                    return;
                }
                this.TetrisRepeat();
                if (this.CheckCompleteCol().Count() != 0)
                {
                    this.DeleteCol(this.CheckCompleteCol());
                }
            }
            
            //下に移動したブロックを描画する
            this.view.ClearBlock(ref this.field);
            this.view.WriteBlockOnField(this.field, this.block, X_POS, Y_POS);
            if (this.HardDropPosition() != this.Y_POS)
            {
                this.view.WriteHardDropBlockOnField(this.field, this.block, X_POS, this.HardDropPosition());
            }
            this.view.ShowField(this.field, this.TetrisFieldGraphics);
        }

        //escボタンで初期状態に戻す
        private void TetrisReset()
        {
            this.X_POS = Constants.X_POS;
            this.Y_POS = Constants.Y_POS;
            this.allNextBlock.Clear();
            this.CreateNextBlock();
            this.view.CreateField(this.field, Constants.FIELD_ARRAY_WIDTH, Constants.FIELD_ARRAY_HEIGHT);
            this.view.CreateField(this.nextBlockField, Constants.BLOCK_FIELD_ARRAY_LENGTH, Constants.ALL_NEXT_BLOCK_FIELD_HEIGHT);
            this.view.CreateAllNextBlock(this.allNextBlock, Constants.NEXT_BLOCK_NUMBER);
            this.block = this.view.CreateBlock();
            this.Drop.Start();
        }

        //テトリスの初期化処理
        private void TetrisRepeat()
        {
            this.block = this.allNextBlock.Dequeue();
            this.X_POS = Constants.X_POS;
            this.Y_POS = Constants.Y_POS;
            this.CreateNextBlock();
            this.view.ClearBlock(ref this.nextBlockField);
            this.view.ClearBlock(ref this.stockBlockField);
            this.view.WriteBlockOnField(this.field, this.block, this.X_POS, this.Y_POS);
            this.view.WriteNextBlockOnField(this.nextBlockField, this.allNextBlock, 1, 1);
            this.view.ShowField(this.nextBlockField, this.NextBlockGraphics);
            this.view.ShowField(this.field, this.TetrisFieldGraphics);
        }

        //NextBlockFieldに新しいブロックを作る
        private void CreateNextBlock()
        {
            this.allNextBlock.Enqueue((int[][])this.view.CreateBlock());
            this.view.ShowField(this.nextBlockField, this.NextBlockGraphics);
        }

        //操作ブロックをストックする
        private void InStockBlock()
        {
            //既にストックがあれば操作ブロックと交換する
            if (this.IsStockBlock)
            {
                if (!this.IsMoveHit(this.stockBlock, 0, 0))
                {
                    int[][] temp = (int[][])this.block.Clone();
                    this.view.ClearBlock(ref this.field);  
                    this.block = (int[][])this.stockBlock.Clone();
                    this.view.ClearBlock(ref this.stockBlockField);
                    this.stockBlock = temp;
                }
            }
            else
            {
                this.stockBlock = (int[][])this.block.Clone();
                this.view.ClearBlock(ref this.field);
                this.view.ClearBlock(ref this.stockBlockField);
                this.block = this.allNextBlock.Dequeue();
                this.CreateNextBlock();
                this.IsStockBlock = true;
                this.view.WriteNextBlockOnField(this.nextBlockField, this.allNextBlock, 1, 1);
            }
            this.view.WriteBlockOnField(this.stockBlockField, this.stockBlock, 1, 1);
            this.view.ShowField(this.stockBlockField, this.StockBlockGraphics);
        }

        //ブロックを操作する
        private void TetrisMove(object sender, KeyEventArgs e)
        {
            if (this.IsCanMove)
            {
                switch (e.KeyCode.ToString())
                {
                    case "D":
                        if (!this.IsMoveHit(this.block, 1, 0)) this.X_POS++;
                        break;
                    case "A":
                        if (!this.IsMoveHit(this.block, -1, 0)) this.X_POS--;
                        break;
                    case "S":
                        if (!this.IsMoveHit(this.block, 0, 1)) this.Y_POS++;
                        break;
                    case "W":
                        if (!this.IsMoveHit(this.block, 0, 1)) this.HardDrop();
                        break;
                    case "Left":
                        this.BlockRotate('l');
                        break;
                    case "Right":
                        this.BlockRotate('r');
                        break;
                    case "Up":
                        this.InStockBlock();
                        break;
                }

                this.view.ClearBlock(ref this.field);
                this.view.WriteBlockOnField(this.field, this.block, X_POS, Y_POS);
                if(this.HardDropPosition() != this.Y_POS)
                {
                    this.view.WriteHardDropBlockOnField(this.field, this.block, X_POS, this.HardDropPosition());
                }
                this.view.ShowField(this.field, this.TetrisFieldGraphics);

                if (e.KeyCode.ToString().Equals("Escape")) {
                    this.TetrisReset();
                }
            }
        }
        
        //テトリス99のハードドロップ
        private void HardDrop()
        {
            int y_pos = 1;
            while (true)
            {
                for (int y = 0; y < this.block.Length; y++)
                {
                    for (int x = 0; x < block[y].Length; x++)
                    {
                        //ブロックが移動可能か判断する
                        if (block[y][x] == 1 &&
                            (this.field[y + this.Y_POS + y_pos + 1][x + this.X_POS] == -1 ||
                            this.field[y + this.Y_POS + y_pos + 1][x + this.X_POS] == 2))
                        {
                            y_pos += this.Y_POS;
                            goto EXIT;
                        }
                    }
                }
                y_pos++;
            }
            EXIT:
                //下に移動したブロックを描画する
                this.Y_POS = y_pos;
                this.view.ClearBlock(ref this.field);
                this.view.WriteBlockOnField(this.field, this.block, X_POS, Y_POS);
                this.view.ShowField(this.field, this.TetrisFieldGraphics);
        }

        //ハードドロップの位置を返す
        private int HardDropPosition()
        {
            int y_pos = 0;
            while (true)
            {
                for (int y = 0; y < this.block.Length; y++)
                {
                    for (int x = 0; x < block[y].Length; x++)
                    {
                        //ブロックが移動可能か判断する
                        if (block[y][x] == 1 &&
                            (this.field[y + this.Y_POS + y_pos + 1][x + this.X_POS] == -1 ||
                            this.field[y + this.Y_POS + y_pos + 1][x + this.X_POS] == 2))
                        {
                            y_pos += this.Y_POS;
                            return y_pos;
                        }
                    }
                }
                y_pos++;
            }
        }

        //ブロックを回転させる
        private void BlockRotate(char direction)
        {
            int[][] temp = new int[][]
            {
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 0, 0}
            };

            //右回転
            if (direction == 'r')
            {
                for (int y = 0; y < this.block.Length; y++)
                {
                    for (int x = 0; x < this.block[y].Length; x++)
                    {
                        temp[x][3 - y] = this.block[y][x];
                    }
                }
            //左回転
            }
            else if(direction == 'l')
            {
                for (int y = 0; y < this.block.Length; y++)
                {
                    for (int x = 0; x < this.block[y].Length; x++)
                    {
                        temp[3 - x][y] = this.block[y][x];
                    }
                }
            }
            //壁や他のブロックに接触しない位置にあれば回転させる
            if (!IsMoveHit(temp, 0, 0))
            {
                this.block = temp;
            }
        }

        //下まで落ちた時ブロックを停止する
        private void FixedBlock()
        {
            for (int y = 1; y < this.field.Length - 1; y++)
            {
                for (int x = 1; x < this.field[y].Length; x++)
                {
                    if (this.field[y][x] == 1)
                    {
                        this.field[y][x] = 2;
                    }
                }
            }
        }

        //揃っている行があればy軸の位置を返す
        private List<int> CheckCompleteCol()
        {
            List<int> res = new List<int>();
            for (int y = 0; y < this.field.Length; y++)
            {
                int temp = 0;
                for (int x = 0; x < this.field[y].Length; x++)
                {
                    if (this.field[y][x] == 2)
                    {
                        temp++;
                    }
                }
                if (temp == Constants.FIELD_WIDTH)
                {
                    res.Add(y);
                }
            }
            return res;
        }

        //揃っている行を削除
        private void DeleteCol(List<int> target)
        {
            for (int y = 0; y < target.Count(); y++)
            {
                for (int x = 1; x < Constants.FIELD_ARRAY_WIDTH - 1; x++)
                {
                    this.field[target[y]][x] = 0;
                }
            }

            //削除したブロックの数だけ下に落とす
            foreach (int i in target)
            {
                for (int y = i; y > 1; y--)
                {
                    for (int x = 1; x < Constants.FIELD_ARRAY_WIDTH - 1; x++)
                    {
                        this.field[y][x] = this.field[y - 1][x];
                    }
                }
            }

        }

        //ブロックが操作可能か判定する
        private Boolean IsMoveHit(int[][] block, int _x, int _y)
        {
                //X_POSがマイナスだと配列外を参照してしまうのでtrueを返す
                //これ以外の方法を思いつかなかった
                if (_x <= 0 && this.X_POS < 0 && _y != 1)
                {
                    return true;
                }


                for (int y = 0; y < block.Length; y++)
                {
                    for (int x = 0; x < block[y].Length; x++)
                    {
                        //ブロックが移動可能か判断する
                        if (block[y][x] == 1 &&
                            (this.field[y + Y_POS + _y][x + X_POS + _x] == -1 ||
                            this.field[y + Y_POS + _y][x + X_POS + _x] == 2))
                        {
                            return true;
                        }
                    }
                }
                return false;
        }

        //ゲームオーバー処理
        private void GameOver()
        {
            this.Drop.Dispose();
            this.allNextBlock.Clear();
            this.view.CreateField(this.nextBlockField, Constants.BLOCK_FIELD_ARRAY_LENGTH, Constants.ALL_NEXT_BLOCK_FIELD_HEIGHT);
            this.IsCanMove = false;
            int y_len = Constants.FIELD_HEIGHT;
            for(int y = 1; y <= Constants.FIELD_HEIGHT; y++)
            {
                for(int x = 1; x <= Constants.FIELD_WIDTH; x++)
                {
                    if(y < y_len)
                    {
                        this.field[y + 1][x] = this.field[y][x];
                        this.field[y][x] = 0;
                    }else
                    {
                        this.field[y][x] = 0;
                    }
                }
                this.view.ShowField(this.field, this.TetrisFieldGraphics);
                Thread.Sleep(50);
            }
            this.GameOverText.Visible = true;
            this.StartButton.Enabled = true;
            this.ResetButton.Enabled = false;
            this.IsCanMove = false;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.TetrisReset();
        }
    }
}

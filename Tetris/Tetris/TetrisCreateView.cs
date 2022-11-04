using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Tetris
{
    public class TetrisCreateView
    {
        private Random random = new Random();

        public TetrisCreateView()
        {
        }

        //テトリスの画面を生成
        public void CreateField(int[][] field, int x_length, int y_length)
        {
            for (int y = 0; y < y_length; y++)
            {
                field[y] = new int[x_length];
                for (int x = 0; x < x_length; x++)
                {
                    if (WallCheck(y, y_length, x, x_length))
                    {
                        field[y][x] = -1;
                    }
                    else
                    {
                        field[y][x] = 0;
                    }
                }
            }
        }

        //テトリスの操作ブロックを生成
        public int[][] CreateBlock()
        {
            int[][] block = TetrisBlock.BLOCK_ARRAY[this.random.Next(Constants.BLOCK_TYPE)];
            return block;
        }

        //テトリスの操作フィールドが壁であるか判定
        public Boolean WallCheck(int y, int y_length, int x, int x_length)
        {
            Boolean res = false;

            if (y == 0 || y == y_length - 1)
            {
                res = true;
            }

            if (x == 0 || x == x_length - 1)
            {
                res = true;
            }

            return res;
        }

        //テトリスの画面を表示
        public void ShowField(int[][] field, Graphics g)
        {
            for (int y = 0; y < field.Length; y++)
            {
                for (int x = 0; x < field[y].Length; x++)
                {
                    DrawField(g, field[y][x], x, y);
                }
            }
        }

        //テトリスの画面を描画
        public void DrawField(Graphics g, int target, int x, int y)
        {
            switch (target)
            {
                case -2:
                    g.FillRectangle(Brushes.RosyBrown, this.SearchDrawPos(nameof(x), x), this.SearchDrawPos(nameof(y), y), Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
                    break;
                case -1:
                    g.FillRectangle(Brushes.Silver, this.SearchDrawPos(nameof(x), x), this.SearchDrawPos(nameof(y), y), Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
                    break;
                case 0:
                    g.FillRectangle(Brushes.Black, this.SearchDrawPos(nameof(x), x), this.SearchDrawPos(nameof(y), y), Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
                    break;
                case 1:
                    g.FillRectangle(Brushes.Brown, this.SearchDrawPos(nameof(x), x), this.SearchDrawPos(nameof(y), y), Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
                    break;
                case 2:
                    g.FillRectangle(Brushes.RoyalBlue, this.SearchDrawPos(nameof(x), x), this.SearchDrawPos(nameof(y), y), Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
                    break;
            }
        }

        //テトリスの操作ブロックを消す
        public void ClearBlock(ref int[][] field)
        {
            for(int y = 0; y < field.Length; y++)
            {
                for(int x = 0; x < field[y].Length; x++)
                {
                    if(field[y][x] == 1 || field[y][x] == - 2)
                    {
                        field[y][x] = 0;
                    }
                }
            }
        }

        //描画する位置を返す
        public int SearchDrawPos(String direction, int pos)
        {
            int res = 0;

            switch (direction)
            {
                case "x":
                    res = Constants.FIELD_POS + (pos * Constants.BLOCK_SIZE);
                    break;
                case "y":
                    res = Constants.FIELD_POS + (pos * Constants.BLOCK_SIZE);
                    break;
            }
            return res;
        }

        //ブロックを指定した座標に表示
        public void WriteBlockOnField(int[][] field, int[][] block, int _x, int _y)
        {
            for (int y = 0; y < block.Length; y++)
            {
                for (int x = 0; x < block.Length; x++)
                {
                    if (block[y][x] == 1)
                    {
                        field[y + _y][x + _x] = 1;
                    }
                }
            }

        }

        public void WriteHardDropBlockOnField(int[][] field, int[][] block, int _x, int _y)
        {
            for (int y = 0; y < block.Length; y++)
            {
                for (int x = 0; x < block.Length; x++)
                {
                    if (block[y][x] == 1)
                    {
                        field[y + _y][x + _x] = -2;
                    }
                }
            }

        }

        public void CreateAllNextBlock(Queue<int[][]> q, int num)
        {
            for(int i = 0; i < num; i++)
            {
                q.Enqueue((int[][])this.CreateBlock());
            }
        }

        //NextBlockを配列に生成
        public void WriteNextBlockOnField(int[][] field, Queue<int[][]> q, int _x, int _y)
        {
            foreach (var block in q)
            {
                this.WriteBlockOnField(field, block, _x, _y);
                _y += 4;
            }
        }
    }
}

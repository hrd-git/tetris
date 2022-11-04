using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Tetris
{
    public static class Constants
    {

        //ゲームエリアの縦サイズ
        public static readonly int FIELD_HEIGHT = 20;

        //ゲームエリアの横サイズ
        public static readonly int FIELD_WIDTH = 10;

        //1ブロックのサイズ　20px×20px
        public static readonly int BLOCK_SIZE = 20;

        //ゲームエリアの壁
        public static readonly int FIELD_WALL = 2;

        //ゲームエリア配列の縦
        public static readonly int FIELD_ARRAY_HEIGHT = FIELD_HEIGHT + FIELD_WALL;

        //ゲームエリア配列の横
        public static readonly int FIELD_ARRAY_WIDTH = FIELD_WIDTH + FIELD_WALL;

        //AllNextBlockFieldの配列の縦
        public static readonly int ALL_NEXT_BLOCK_FIELD_HEIGHT = 22;

        //nextBlockの数
        public static readonly int NEXT_BLOCK_NUMBER = 4;

        //BlockField配列の長さ
        public static readonly int BLOCK_FIELD_ARRAY_LENGTH = 6;

        //BlockField
        public static readonly int BLOCK_FIELD_LABEL_WIDTH = BLOCK_SIZE * BLOCK_ARRAY_LENGTH + FIELD_WALL;

        //BlockFieldの縦
        public static readonly int BLOCK_FIELD_LABEL_HEIGHT = BLOCK_SIZE * BLOCK_ARRAY_LENGTH + FIELD_WALL;

        //ゲームエリア表示位置
        public static readonly int FIELD_POS = 0;

        //操作するブロックの初期X軸
        public static readonly int X_POS = 4;

        //操作するブロックの初期Y軸
        public static readonly int Y_POS = 0;

        //ブロックの種類
        public static readonly int BLOCK_TYPE = 7;

        //ブロック配列の長さ
        public static readonly int BLOCK_ARRAY_LENGTH = 4;

        //TetrisLabelの縦
        public static readonly int TETRIS_FIELD_LABEL_HEIGHT = BLOCK_SIZE * FIELD_ARRAY_HEIGHT;

        //TetrisLabelの横
        public static readonly int TETRIS_FIELD_LABEL_WIDTH = BLOCK_SIZE * FIELD_ARRAY_WIDTH;


    }
}

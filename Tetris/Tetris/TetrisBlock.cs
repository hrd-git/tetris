using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Tetris
{
    public static class TetrisBlock
    {
        public static readonly int[][][] BLOCK_ARRAY = new int[][][]
        {
            //　凸ブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{0, 1, 0, 0},
                new int[]{0, 1, 1, 0},
                new int[]{0, 1, 0, 0},
            },


            // Lブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 1, 0},
                new int[]{1, 1, 1, 0},
                new int[]{0, 0, 0, 0}
            },

            // 逆Lブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{1, 0, 0, 0},
                new int[]{1, 1, 1, 0},
                new int[]{0, 0, 0, 0}
            },

            // Zブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 1, 0},
                new int[]{0, 1, 1, 0},
                new int[]{0, 1, 0, 0}

            },

            // 逆Zブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{0, 1, 0, 0},
                new int[]{0, 1, 1, 0},
                new int[]{0, 0, 1, 0}

            },

            //　四角ブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{0, 1, 1, 0},
                new int[]{0, 1, 1, 0},
                new int[]{0, 0, 0, 0}
            },

            //　棒ブロック
            new int[][]{
                new int[]{0, 0, 0, 0},
                new int[]{1, 1, 1, 1},
                new int[]{0, 0, 0, 0},
                new int[]{0, 0, 0, 0}
            },
        };
    };

}

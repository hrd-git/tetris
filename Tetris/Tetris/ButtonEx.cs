using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tetris.Tetris
{
    public class ButtonEx : Button
    {
        protected override bool IsInputKey(Keys keyData)
        {
            //Altキーが押されているか確認する
            if ((keyData & Keys.Alt) != Keys.Alt)
            {
                //矢印キーが押されたときは、trueを返す
                Keys kcode = keyData & Keys.KeyCode;
                if (kcode == Keys.Up || kcode == Keys.Down ||
                    kcode == Keys.Left || kcode == Keys.Right ||
                    kcode == Keys.Enter)
                {
                    return true;
                }
            }
            return base.IsInputKey(keyData);
        }
    }
}
    
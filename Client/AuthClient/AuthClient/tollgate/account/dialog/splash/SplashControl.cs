﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace AuthClient.tollgate.account.dialog
{
    public partial class SplashControl : UserControl
    {
        public SplashControl()
        {
            InitializeComponent();

            PrivateFontCollection fonts = new PrivateFontCollection();
            fonts.AddFontFile(Property.FONT_BLACK);

            Font font = new Font(fonts.Families[0], 24);
            label_hello.Font = font;
            label_hello.Text = "안녕하세요, " + Config.GetCurrentUser() + "님!";

            label_hello.Left = (this.ClientSize.Width - (label_hello.Width + img_smile.Width - 10)) / 2;
            label_hello.Top = 288;
            img_smile.Left = label_hello.Right - 10;
            img_smile.Top = ((label_hello.Height - img_smile.Height) / 2) + label_hello.Top + 2;
            label_sub.Left = (this.ClientSize.Width - label_sub.Width) / 2;
            label_sub.Top = label_hello.Bottom + 10;
            btn_splash.Left = (this.ClientSize.Width - btn_splash.Width) / 2;
            btn_splash.Top = label_sub.Bottom + 51;
        }
    }

    public class SubTitle : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Point drawPoint = new Point(0, 0);

            string subL = "이제 언제나 ";
            string subM = "TOLLGATE와 함께";
            string subR = "하실 수 있습니다.";

            PrivateFontCollection fonts = new PrivateFontCollection();
            fonts.AddFontFile(Property.FONT_BOLD);
            fonts.AddFontFile(Property.FONT_MEDIUM);

            Font fontM = new Font(fonts.Families[0], 10);
            Size sizeM = TextRenderer.MeasureText(subM, fontM);

            Font font = new Font(fonts.Families[1], 10);
            Size sizeL = TextRenderer.MeasureText(subL, font);
            Size sizeR = TextRenderer.MeasureText(subR, font);

            Rectangle rectL = new Rectangle((this.Width - (sizeL.Width + sizeM.Width + sizeR.Width)) / 2, 0, sizeL.Width, sizeL.Height);
            Rectangle rectM = new Rectangle(rectL.Right, rectL.Top, sizeM.Width, sizeM.Height);
            Rectangle rectR = new Rectangle(rectM.Right, rectM.Top, sizeR.Width, sizeR.Height);

            TextRenderer.DrawText(e.Graphics, subL, font, rectL, ForeColor);
            TextRenderer.DrawText(e.Graphics, subM, fontM, rectM, ForeColor);
            TextRenderer.DrawText(e.Graphics, subR, font, rectR, ForeColor);
        }
    }
}

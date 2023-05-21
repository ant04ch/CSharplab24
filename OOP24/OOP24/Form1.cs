using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Threading;

namespace OOP24
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource1;
        private CancellationTokenSource cancellationTokenSource2;
        private CancellationTokenSource cancellationTokenSource3;

        public Form1()
        {
            InitializeComponent();
        }

        private async Task DrawRectangleAsync(CancellationToken cancellationToken)
        {
            try
            {
                Random rnd = new Random();
                Graphics g = panel1.CreateGraphics();

                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(40, cancellationToken);
                    g.DrawRectangle(Pens.Pink, 0, 0, rnd.Next(panel1.Width), rnd.Next(panel1.Height));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task DrawEllipseAsync(CancellationToken cancellationToken)
        {
            try
            {
                Random rnd = new Random();
                Graphics g = panel2.CreateGraphics();

                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(40, cancellationToken);
                    g.DrawEllipse(Pens.Pink, 0, 0, rnd.Next(panel2.Width), rnd.Next(panel2.Height));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task GenerateRandomNumberAsync(CancellationToken cancellationToken)
        {
            try
            {
                Random rnd = new Random();

                for (int i = 0; i < 500; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    richTextBox1.Invoke((MethodInvoker)delegate
                    {
                        richTextBox1.AppendText(rnd.Next().ToString() + Environment.NewLine);
                    });

                    await Task.Delay(1, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            cancellationTokenSource1 = new CancellationTokenSource();
            cancellationTokenSource2 = new CancellationTokenSource();
            cancellationTokenSource3 = new CancellationTokenSource();

            CancellationToken cancellationToken1 = cancellationTokenSource1.Token;
            CancellationToken cancellationToken2 = cancellationTokenSource2.Token;
            CancellationToken cancellationToken3 = cancellationTokenSource3.Token;

            await Task.WhenAll(
                DrawRectangleAsync(cancellationToken1),
                DrawEllipseAsync(cancellationToken2),
                GenerateRandomNumberAsync(cancellationToken3)
            );
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cancellationTokenSource1 = new CancellationTokenSource();
            CancellationToken cancellationToken1 = cancellationTokenSource1.Token;
            await DrawRectangleAsync(cancellationToken1);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            cancellationTokenSource2 = new CancellationTokenSource();
            CancellationToken cancellationToken2 = cancellationTokenSource2.Token;
            await DrawEllipseAsync(cancellationToken2);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            cancellationTokenSource3 = new CancellationTokenSource();
            CancellationToken cancellationToken3 = cancellationTokenSource3.Token;
            await GenerateRandomNumberAsync(cancellationToken3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cancellationTokenSource1?.Cancel();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cancellationTokenSource2?.Cancel();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cancellationTokenSource3?.Cancel();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cancellationTokenSource1?.Cancel();
            cancellationTokenSource2?.Cancel();
            cancellationTokenSource3?.Cancel();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cancellationTokenSource1?.Cancel();
            cancellationTokenSource2?.Cancel();
            cancellationTokenSource3?.Cancel();
        }
    }
}

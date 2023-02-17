using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboration_2
{
    public partial class MainForm : Form
    {
        private int[] row = new int[7];
        private int[] userRow = new int[7];
        private int fiveScore = 0;
        private int sixScore = 0;
        private int sevenScore = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // Metod som körs när man trycker på "Starta Lotto"
        private void StartaLotto(object sender, EventArgs e)
        {
            int antal = 0;

            try {
                antal = int.Parse(AntalDragningar.Text);
                userRow[0] = Convert.ToInt32(lotto1.Text);
                userRow[1] = Convert.ToInt32(lotto2.Text);
                userRow[2] = Convert.ToInt32(lotto3.Text);
                userRow[3] = Convert.ToInt32(lotto4.Text);
                userRow[4] = Convert.ToInt32(lotto5.Text);
                userRow[5] = Convert.ToInt32(lotto6.Text);
                userRow[6] = Convert.ToInt32(lotto7.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            if (antal <= 0)
            {
                MessageBox.Show("Antal dragningar måste vara minst 1");
                return;
            }

            if (userRow[0] < 1 || userRow[0] > 35 || userRow[1] < 1 || userRow[1] > 35 || userRow[2] < 1 || userRow[2] > 35 ||
                userRow[3] < 1 || userRow[3] > 35 || userRow[4] < 1 || userRow[4] > 35 || userRow[5] < 1 || userRow[5] > 35 ||
                userRow[6] < 1 || userRow[6] > 35)
            {
                MessageBox.Show("Lottorad talen måste vara mellan (inklusiv) 1-35");
                return;
            } else if (userRow.Length != userRow.Distinct().Count()) {
                MessageBox.Show("Talen måste vara unika! (inga dubletter)");
                return;
            }

            for (int i = 0; i < antal; i++)
            {
                GenerateNewRow();
                CheckForMatches();
            }
        }

        // Genererar en lottorad (7 unika tal mellan (inklusiv) 1-35)
        Random r = new Random();
        private void GenerateNewRow() 
        {
            // Genererar och lagrar 7 slumpmässiga tal mellan 1-35 i en array
            for (int i = 0; i < row.Length; i++)
            {
                int slump = r.Next(1, 36);
                row[i] = slump;
            }

            // Kontrollerar om det finns dubletter
            for (int i = row.Length - 1; i > 0; i--) 
            {
                for (int j = 0; j < i; j++) 
                {
                    if (row[i] == row[j])
                    {
                        GenerateNewRow();
                        break;
                    }
                }
            }
        }

        // Jämför arrayen vi har genererat med talen användaren har angivit
        private void CheckForMatches()
        {
            int matches = 0;

            for (int i = 0; i < row.Length; i++)
            {
                for (int j = 0; j < userRow.Length; j++)
                {
                    if (row[i] == userRow[j])
                    {
                        matches++;
                    }
                }
            }

            if (matches == 5) fiveScore++;
            if (matches == 6) sixScore++;
            if (matches == 7) sevenScore++;

            matchBox5.Text = fiveScore.ToString();
            matchBox6.Text = sixScore.ToString();
            matchBox7.Text = sevenScore.ToString();
        }
    }
}

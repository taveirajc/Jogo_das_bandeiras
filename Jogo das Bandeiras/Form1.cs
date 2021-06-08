using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Jogo_das_Bandeiras
{

    #region DEFINIÇÃO DAS VARIÁVEIS
    public partial class Form1 : Form
    {
        string  resultado;
        string palavra,  // É O NOME DA BANDEIRA
               palavra1, // É O NOME DA BANDEIRA EMBARALHADO
               capital, //  É O NOME DA CAPITAL DO PAÍS SORTEADO
               continente;  // CONTINENTE DO PAIS SORTEADO.
        
        String novapalavra = "XWYKQÊÂ"; // LETRAS A SEREM ADICIONADAS NO NOME ORIGINAL
        
        int tam, nivel, x1, i;

        int totalbotoes, L1;
        int achou = 0;
        int conta = 0;
        int D1=1;
        Random naleatorio = new Random();

        const int X = 0, Y = 0;

        Button[] btn;
        Button[] backspace;
        Label[] labels;
        Panel painel = new Panel();

        public string pasta_aplicacao = Application.StartupPath + @"\";  // CAMINHO DO PROGRAMA
        SoundPlayer player = new SoundPlayer();

        string[,] BANDEIRAS = new string[3, 14] 
        { 
                {"Alemanha", "Brasil", "Argentina", "França", "Portugal", "Espanha", "EUA", 
                "Chile", "Colômbia", "Itália", "Paraguai", "México", "Venezuela", "Japão"},
                {"Bolívia", "Camarões", "Canadá", "Cuba", "Israel", "Rússia", "China",
                "Coréia", "Croácia", "Egito", "India", "Irlanda", "Noruega", "Nicarágua"},
                {"Líbano", "Panamá", "Senegal", "Síria", "Austrália", "Uruguai", "Somália",
                "Angola", "Hungria", "Catar", "Paquistão", "Gana", "Filipinas", "Haiti"}
        };

        string[,] CAPITAIS = new string[3, 14]
        {
                {"Berlim", "Brasília", "Buenos Aires", "Paris", "Lisboa", "Madri", "Washington", 
                "Santiago", "Bogotá", "Roma", "Assunção", "Cidade do México", "Caracas", "Tóquio"},
                {"Sucre", "Iaundé", "Otava", "Havana", "Jerusalém", "Moscovo", "Pequim",
                "Seul", "Zagrebe", "Cairo", "Nova Déli", "Dublim", "Oslo", "Manágua"},
                {"Beirute", "Cidade do Panamá", "Dacar", "Damasco", "Camberra", "Montevideu", "Mogadíscio",
                "Luanda", "Budapeste", "Doa", "Islamabade", "Acra", "Manila", "Porto Príncipe"}
        };

        string[,] CONTINENTES = new string[3, 14]
        {
                {"Europa", "América", "América", "Europa", "Europa", "Europa", "América", 
                "América", "América", "Europa", "América", "América", "América", "Ásia"},
                {"América", "África", "América", "América", "Ásia", "Europa", "Ásia",
                "Ásia", "Europa", "África", "Ásia", "Europa", "Europa", "América"},
                {"Ásia", "América", "África", "Ásia", "Oceania", "América", "África",
                "África", "Europa", "Ásia", "Ásia", "África", "Ásia", "América"}
        };
        #endregion

        #region form1 - INICIALIZAÇÃO DO PROGRAMA
        public Form1()
        {
            InitializeComponent();

            painel.Width = 400;// largura do flow
            painel.Height = 40; // altura do flow
            painel.Anchor = AnchorStyles.None;

            var point = new Point(111, 210); // posicionamento (x,y)
            this.painel.Location = point;
            this.Controls.Add(painel); // adição do controle
        }
        #endregion

        #region limpa_nivel LIMPA OS RADIOBUTTON
        private void limpa_nivel()
        {           
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is RadioButton)
                {
                    RadioButton radioB = (RadioButton)ctrl;

                    if (radioB.Checked)
                    {
                        radioB.Checked = false;
                    }
                }
            }
   
        }
        #endregion

        #region obtempalavra - SORTEIA A PALAVRA E PEGA NO ARRAY DE ACORDO COM A CATEGORIA ESCOLHIDA
        public void obtempalavra()
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is RadioButton)
                {
                    RadioButton radioB = (RadioButton)ctrl;

                    if (radioB.Checked)
                    {
                        nivel = radioB.TabIndex; // PEGA O ÍNDICE DA CATEGORIA ESCOLHIDO PARA COLOCAR NA PRIMEIRA COLUNA DA MATRIZ
                    }
                }
            }

            x1 = naleatorio.Next(0, 14); // naleatorio.Next(valor mínimo, valor máximo +1 = então entre 1 e 14)
            palavra = BANDEIRAS[nivel, x1].ToUpper();  // palavra a ser advinhada em maiuscula
            capital = CAPITAIS[nivel, x1].ToUpper();
            continente = CONTINENTES[nivel, x1].ToUpper();
            tam = palavra.Length;  // tamanho do nome da bandeira
        }
        #endregion

        #region DesenharLabels - DESENHA OS LABELS DA PALAVRA
        private void DesenharLabels()
        {
            pictureBox1.Image = Image.FromFile(pasta_aplicacao + @"Imagens_Bandeiras\" + palavra + ".jpg");

            tam = palavra.Length;
            labels = new Label[tam];

            int L2 = 0;
            switch (tam)
            {
                case 9: L2 = 85;
                    break;
                case 8: L2 = 104;
                    break;
                case 7: L2 = 122;
                    break;
                case 6: L2 = 140;
                    break;
                case 5: L2 = 160;
                    break;
                case 4: L2 = 173;
                    break;
                case 3: L2 = 190;
                    break;
            }

            for (int i = 0; i < tam; i++)
            {
                labels[i] = new Label();
                //labels[i].Click += new EventHandler(desfaz_label);

                labels[i].Font = new Font("Arial", 20, FontStyle.Bold);
                labels[i].ForeColor = System.Drawing.Color.Black;
                labels[i].BackColor = Color.White;
                labels[i].TextAlign = ContentAlignment.MiddleLeft;
                labels[i].AutoSize = false;
                labels[i].Size = new Size(30, 30);
                labels[i].Text = "";
                labels[i].Name = labels[i].ToString();
                labels[i].Location = i == 0 ? new Point(L2, 5) : new Point((labels[i - 1].Location.X + 35), 5);
                painel.Controls.Add(labels[i]);
            }  
        }
        #endregion

        #region desfaz_botao CLIQUE NOS BOTÕES DAS LETRAS
        private void desfaz_botao(object sender, EventArgs e)        
        {
              string letra;
              int posicao=-1;

              Button botao_clicado = sender as Button;

              if (botao_clicado != null)
              {
                  achou = 0;
                  for (int k = 0; k < tam; k++)
                  {
                      if (botao_clicado.Text == palavra.Substring(k, 1))
                      {
                          posicao = k;
                          achou = 1;
                          if (labels[k].Text == "")
                            break;
                      }
                  }
                  if (achou == 1)
                  {
                      player.SoundLocation = (pasta_aplicacao + @"\Ring07.wav");
                      player.Play();
                      labels[posicao].BackColor = Color.PaleGreen;
                      labels[posicao].Text = botao_clicado.Text;
                      labels[posicao].Update();
                      letra = labels[posicao].Text;

                      botao_clicado.Visible = false;


                      pictureBox2.BackgroundImage = Image.FromFile(pasta_aplicacao + @"\certo.png");
                      pictureBox2.Update();
                      System.Threading.Thread.Sleep(600); // DÁ UM TEMPO DE 0,6 SEGUNDO PARA A MÃOZINHA APARECER
                      pictureBox2.BackgroundImage = null;  // DESAPARECE A IMAGEM DA MÃOZINHA
                      conta = 0;
                      for (int k = 0; k < tam; k++)
                      {
                   
                          if (labels[k].Text == palavra.Substring(k, 1))
                          {
                              conta++; ;
                          }
                      }
                      if (conta == tam)
                      {
                          player.SoundLocation = (pasta_aplicacao + @"\Parabéns.wav");
                          player.Play();
                          label1.Text = "";
                          label4.Text = "Parabéns!!!!!!";
                      }
                  }

                    if (achou == 0)
                    {
                        pictureBox2.BackgroundImage = Image.FromFile(pasta_aplicacao + @"\errado.png");
                        pictureBox2.Update();
                        player.SoundLocation = (pasta_aplicacao + @"\CrocCroc.wav");
                        player.Play();

                        System.Threading.Thread.Sleep(600);
                        pictureBox2.BackgroundImage = null;
                    }
              }                   
        }
        #endregion

        #region embaralha - EMBARALHA A DESCRIÇÃO DA BANDEIRA ESCOLHIDA
        private void embaralha()
        {
            var rnd = new Random();

            char[] especiais = { '\r', '.', '?' };
            char? especial = null;

            // caso a palavra termine com um caractere especial
            if (especiais.Contains(palavra.Last()))
            {
                // guarda o caractere especial
                especial = palavra.Last();

                // remove o caretere especial
                palavra = palavra.Substring(0, palavra.Length - 1);
            }
            palavra1 = palavra;

            if (tam >= 3 && tam <= 5)
            {
                totalbotoes = 8;
            }
            else if (tam > 5 && tam <= 8)
            {
                totalbotoes = 10;
            }
            else if (tam > 8)
            {
                totalbotoes = 12;
            }
            for (int t = tam; t < totalbotoes; t++)
            {
                palavra1 = palavra1 + novapalavra.Substring(totalbotoes - t, 1);
            }
            // Skip(1)                   : salta o primeiro elemento
            // Take(palavra.Length - 2)  : trás os elementos consuante o tamanho da palavra menos 2
            // OrderBy(c => rnd.Next())  : faz uma ordenação aleatória dos elementos
            // ToArray()                 : converte para array
            char[] chars_do_meio = palavra1.Skip(1).Take(palavra1.Length - 2).OrderBy(c => rnd.Next()).ToArray();

            // new string(chars_do_meio): cria uma string a partir de um array de chars
            resultado = palavra1.First() + new string(chars_do_meio) + palavra1.Last() + especial;
        }
        #endregion

        #region mostra_botoes - CRIA OS BOTÕES QUE SERÃO CLICADOS JÁ COM AS LETRAS DA BANDEIRA E O RESTO ALEATÓRIO
        private void mostra_botoes()
        {
            if (tam >= 3 && tam <= 5)
            {
                totalbotoes = 8;
                L1 = 81; 
            }
            else if (tam > 5 && tam <= 8)
            {
                totalbotoes = 10;
                L1 = 45;
            }
            else if (tam > 8)
            {
                totalbotoes = 12;
                L1 = 14;
            }

            btn = new Button[totalbotoes];
            backspace = new Button[totalbotoes];

            for (i = 0; i < totalbotoes; i++)
            {
                btn[i] = new Button();
                backspace[i] = new Button();
                btn[i].Click += new EventHandler(desfaz_botao);
                btn[i].Font = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
                btn[i].ForeColor = System.Drawing.Color.Black;
                btn[i].BackColor = Color.White;
                btn[i].TextAlign = ContentAlignment.MiddleLeft;
                btn[i].AutoSize = false;
                btn[i].Size = new Size(30, 30);
                btn[i].Text = resultado.Substring(i, 1);

                btn[i].Location = i == 0 ? new Point(L1, 2) : new Point((btn[i - 1].Location.X + 35), 2);
                backspace[i] = btn[i];
                panel1.Controls.Add(btn[i]);
            }
        }
        #endregion

        #region limpa_controles LIMPA OS CONTROLES BUTTON E LABEL DO JOGO
        private void limpa_controles()
        {
            var list = (from object item in painel.Controls where item is Label select item as Control).ToList();

            list.ForEach(x => painel.Controls.Remove(x));

            var lista = (from object item1 in panel1.Controls where item1 is Button select item1 as Control).ToList();

            lista.ForEach(x => panel1.Controls.Remove(x));
        }
        #endregion

        #region nivel_escolhido  VERIFICA O NÍVEL SELECIONADO PELO USUÁRIO E COMEÇA O PROGRAMA
        private void nivel_escolhido(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = null;
            label4.Text = "";
            label1.Text = "Dica 1";
            limpa_controles();
            obtempalavra();
            DesenharLabels();
            embaralha();
            mostra_botoes();
            label5.Text = capital;
            label7.Text = continente;
        }
        #endregion

        #region inibe_botoes ATIVA E DESATIVA BOTOES DEPENDENDO DO VALOR DO FLAG RECEBIDO
        private void inibe_botoes(int flag)
        {
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button radioB = (Button)ctrl;
                    if (flag == 1)
                        radioB.Enabled = false; // desativa os botoes no final do acerto da palavra
                    else
                        radioB.Enabled = true; // ativa os botoes no final do acerto da palavra   
                }
             }          
        }
        #endregion

        #region Dicas - ROTINA DE DICAS
        private void Dicas(object sender, EventArgs e)
        {
            bool achou = true;
            Random numaleat = new Random();
            int indice = numaleat.Next(0, tam - 1);  // naleatorio.Next(valor mínimo, valor máximo +1 = então entre 0 e tamanho no nome da bandeira -1)
            while (achou)
            {
                if (labels[indice].Text == "")
                {
                    labels[indice].Text = palavra.Substring(indice, 1);
                    labels[indice].BackColor = Color.PaleGreen;
                    labels[indice].Update();
                    achou = false;
                    for (int g = 0; g < resultado.Length; g++)
                    {
                        if (labels[indice].Text == btn[g].Text && btn[g].Visible == true)
                        {
                            btn[g].Visible = false;
                            break;
                        }
                    }
                    D1++;
                    if (D1 <= 3)
                    {
                        label1.Text = "Dica " + D1.ToString();
                        label1.Update();
                    }
                    else
                    {
                        label1.Text = string.Empty;
                        D1 = 0;
                        //achou = false;
                    }
                }
                if (achou == true)
                    indice = numaleat.Next(0, tam - 1);  // naleatorio.Next(valor mínimo, valor máximo +1 = então entre 0 e tamanho no nome da bandeira -1)
            }           
        }
        #endregion

        #region SAÍDA DO PROGRAMA
        private void button28_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}

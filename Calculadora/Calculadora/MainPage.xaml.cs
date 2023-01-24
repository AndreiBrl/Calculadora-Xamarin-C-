using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        /* operacao => Esta variável acumula a ultima operação pressionada pelo usuário se tratando apenas dos operadores  + - * /*/
        string operacao;


        /* resultado =>  Acumula as operações de natureza Double*/
        double resultado = 0;


        /* numeroDeOperacoes=> Em caso do usuário pressionar o botao Igual e decidir realizar novas operações em cima 
          daquele ultimo resultado a Label precisa voltar ao valor inicial para que novas operações sejam feitas.
         Portanto esta variável tem a função de retornar o valor da Label para 0 a partir do momento em que o igual seja pressionado
        por pelo menos uma vez*/
        int numeroDeOperacoes = 0;


        /* sequencia => Em caso do usúário decidir por fazer várias operações em sequencia sem apertar o botao de Igual será necessário uma variável que 
         * identifique este comportamento e  partir dele efetue um novo caminho de operação. Esta é a funcao desta variável*/
        int sequencia = 0;

        /* operacaoAnterior => Em casos normais a operação é finalizada com o botao de igual pressionado. Contudo como dito acima uma sequencia de operadores pode ser apertada
         * sem que o botao de igual seja pressionado e para que isso ocorra sem crash a operação anterior deve ser guardada tanto quanto a atual. Esta variável
         armazena a operação anterior pressionada.*/
        string operacaoAnterior;

        /* labelAnterior => Acessora no armazenamento de valores na labelAntecipada. */
        string labelAnterior;

        
        
        public MainPage()
        {
            InitializeComponent();
        }

       private void onSelectNumber(object sender, EventArgs e)
        {
            double formataNumero;
            Button button = (Button)sender;
            
                
            if(this.resultado!=0 && this.label.Text == "-")
            {
                this.label.Text = "";
            }
            this.labelAnterior = this.label.Text;

            if (this.label.Text == "0" || this.label.Text == "+" || this.label.Text == "/" || this.label.Text == "*" || this.label.Text == "/" )
            {
                this.label.Text = "";
                
            }

            this.label.Text += button.Text;
            formataNumero = Convert.ToDouble(this.label.Text);
            
            this.label.Text = formataNumero.ToString("N0");



            if ( this.labelAnterior=="/" && this.label.Text == "0")
            {
                this.labelAntecipaResultado.Text = "Impossível";
            }
        }

        

        private void btnSoma_Clicked(object sender, EventArgs e)
        {
            operacaoAnterior = operacao;
            operacao = "+";
            if (sequencia >= 1)
            {
                calculaSequenciaDeOperadores();
                this.labelAntecipaResultado.Text = this.resultado.ToString("N0");
            }
            else
            {
                this.labelAntecipaResultado.Text = this.label.Text + "+";
                calcula();
            }
            this.label.Text = "+";

        }

        

        private void btnDivisao_Clicked(object sender, EventArgs e)
        {
            operacaoAnterior = operacao;
            operacao = "/";
            
            if (sequencia >= 1)
            {
                calculaSequenciaDeOperadores();
                this.labelAntecipaResultado.Text = this.resultado.ToString("N0");
            }
            else
            {
                this.labelAntecipaResultado.Text = this.label.Text + "/";
                calcula();
            }
            this.label.Text = "/";
        }

        private void btnMultiplicacao_Clicked(object sender, EventArgs e)
        {

            operacaoAnterior = operacao;
            operacao = "*";
            if (sequencia >= 1)
            {
                calculaSequenciaDeOperadores();
                this.labelAntecipaResultado.Text = Convert.ToString(this.resultado);
            }
            else
            {
                this.labelAntecipaResultado.Text = this.label.Text + "*";
                calcula();
            }
            this.label.Text = "*";
        }

        private void btnSubtracao_Clicked(object sender, EventArgs e)
        {

            operacaoAnterior = operacao;
            operacao = "-";
            if (sequencia >= 1)
            {
                calculaSequenciaDeOperadores();
                this.labelAntecipaResultado.Text = Convert.ToString(this.resultado);
            }
            else
            {
                this.labelAntecipaResultado.Text = this.label.Text + "-";
                calcula();
            }
            this.label.Text = "-";
        }
        private void btnVirgula_Clicked(object sender, EventArgs e)
        {
            this.label.Text += ",";
        }
        private void btnC_Clicked(object sender, EventArgs e)
        {
            this.label.Text = "0";
            this.resultado = 0;
            numeroDeOperacoes= 0;
            this.labelAntecipaResultado.Text = "";
            
        }
        private void btnIgual_Clicked(object sender, EventArgs e)
        {
            
            
            calcula();
            this.label.Text = this.resultado.ToString("N0");

            numeroDeOperacoes = 1;
            sequencia = 0;
            this.labelAntecipaResultado.Text = "";

        }


        private void calcula ()
        {
                    sequencia++;
                if (numeroDeOperacoes == 1)
                {
                    this.label.Text = "0";
                }
                if (this.label.Text != "+" && this.label.Text != "-" && this.label.Text != "*" && this.label.Text != "/")
                {
                    if (operacao == "+")
                    {
                        this.resultado += Convert.ToDouble(this.label.Text);
                        if (double.Parse(this.label.Text) < 0)
                        {
                            this.resultado = this.resultado - double.Parse(this.label.Text);
                        }


                    }

                    if (operacao == "/")
                    {

                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);

                        }
                        else
                        {
                            if (this.label.Text == "0")
                            {
                                this.label.Text = "1";
                            }
                            this.resultado /= Convert.ToDouble(this.label.Text);
                        }
                    }
                    if (operacao == "*")
                    {

                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);
                        }
                        else
                        {
                            if (this.label.Text == "0")
                            {
                                this.label.Text = "1";
                            }

                            this.resultado *= Convert.ToDouble(this.label.Text);
                        }


                    }
                    if (operacao == "-")
                    {
                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);
                        }
                        else
                        {

                            this.resultado = this.resultado - Convert.ToDouble(this.label.Text);
                        }
                    }
                    numeroDeOperacoes = 0;
                
            }
            


        }
        private void calculaSequenciaDeOperadores()
        {
            
                if (numeroDeOperacoes == 1)
                {
                    this.label.Text = "0";
                }
                if (this.label.Text != "+" && this.label.Text != "-" && this.label.Text != "*" && this.label.Text != "/")
                {
                    if (operacaoAnterior == "+")
                    {
                        this.resultado += Convert.ToDouble(this.label.Text);
                        if (double.Parse(this.label.Text) < 0)
                        {
                            this.resultado = this.resultado - double.Parse(this.label.Text);
                        }


                    }

                    if (operacaoAnterior == "/")
                    {

                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);

                        }
                        else
                        {
                            if (this.label.Text == "0")
                            {
                                this.label.Text = "1";
                            }
                            this.resultado /= Convert.ToDouble(this.label.Text);
                        }
                    }
                    if (operacaoAnterior == "*")
                    {

                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);
                        }
                        else
                        {
                            if (this.label.Text == "0")
                            {
                                this.label.Text = "1";
                            }

                            this.resultado *= Convert.ToDouble(this.label.Text);
                        }


                    }
                    if (operacaoAnterior == "-")
                    {
                        if (this.resultado == 0)
                        {
                            this.resultado = Convert.ToDouble(this.label.Text);
                        }
                        else
                        {

                            this.resultado = this.resultado - Convert.ToDouble(this.label.Text);
                        }
                    }
                    numeroDeOperacoes = 0;
                }
            

        }

        
    }
}

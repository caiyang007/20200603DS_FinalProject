using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form f = new Form2();
            f.ShowDialog();
            textBox2.ReadOnly = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                button23_Click(button23, e);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text += ".";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox1.Text += "*";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/";
        }

        private void button29_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += "ln(";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += "exp(";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            textBox1.Text += "^";
        }

        private void button27_Click(object sender, EventArgs e)
        {
            textBox1.Text += "sin(";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            textBox1.Text += "cos(";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";    
        }



        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="") textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            textBox2.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string formula = textBox1.Text;
            if (isFormatRight(formula) == 1)
            {
                textBox2.Text = calculate(formula).ToString();
            }
            else
            {
                MessageBox.Show("Error Input!", "Error");
                button14_Click(button14, e);
            }
        }
        private double getNumValue(string s)
        {//将字符值转成数值
            int numlen = s.Length;
            double sum = 0;
            int pointFlag = 0,pointLoc=-1;
            for(int i = numlen-1; i >= 0; i--)
            {
                if(s[i]>='0' && s[i] <= '9' && pointFlag==0)
                {
                    sum = sum + (s[i] - 48) * Math.Pow(10, numlen - i - 1);
                }
                if (s[i] == '.')
                {
                    pointFlag = 1;
                    pointLoc = i;
                    sum = sum * Math.Pow(10, (-1) * (numlen - i-1));
                }
                if (s[i] >= '0' && s[i] <= '9' && pointFlag == 1 && pointLoc != -1)
                {
                    sum = sum + (s[i] - 48) * Math.Pow(10, numlen - i - 1) * Math.Pow(10, (-1) * (numlen - pointLoc));
                }
            }
            return sum;
        }
        private int isNumRight(string s)
        {//判断数值是否格式正确
            int pointCnt = 0;
            int pointLoc = -1;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == '.')
                {
                    pointCnt += 1;
                    pointLoc = i;
                }
            }
            if (pointCnt > 1) return 0;
            if (pointLoc == 0 || pointLoc == s.Length - 1) return 0;
            return 1;
        }
        private double calculate(string s)
        {//计算主程序
            Stack S1 = new Stack();//运算符栈
            Stack S2 = new Stack();//运算数栈
            for (int i = 0; i < s.Length;)
            {
                if ((s[i] <= '9' && s[i] >= '0') || s[i] == '.') //s[i]是数字或小数点的情况
                {//获得数字的值
                    int j = i;//i是数字起始位置
                    double num;
                    int numLen = 0;
                    //for (; ((s[j] <= '9' && s[j] >= '0') || s[j] == '.')&&j<s.Length; j++) ;//获得数字的长度
                    while ((s[j]>='0'&&s[j]<='9')||s[j]=='.')
                    {
                        numLen += 1;
                        j += 1;
                        if (j == s.Length) break;
                    }
                    num = getNumValue(s.Substring(i, numLen));
                    i = j;
                    numLen = 0;
                    S2.Push(num);
                    //i = j+1;//把i置于数字后的一个字符
                    continue;
                }

                if (S1.Count == 0 && s[i]!='e'&&s[i]!='s'&&s[i]!='c'&&s[i]!='l')//数字后是运算符，压入
                {//压入第一个运算符
                    S1.Push(s[i++]);
                    continue;
                }
                else
                {
                    switch (s[i])
                    {
                        case 'e':
                        case 'c':
                        case 's':
                            S1.Push(s[i]);
                            i += 3; break;
                        case 'l':
                            S1.Push(s[i]);
                            i += 2;break;
                    }
                }

                char sign = (char)S1.Peek();//记录此时操作数栈栈顶元素，为后续判断优先级
                switch (s[i])
                {
                    case '+':
                    case '-':
                        if (sign == '+' || sign == '-' || sign == '*' || sign == '/' || sign == '(' || sign == '^') 
                        {
                            if (sign == '(') S1.Push(s[i++]);//若是(则后续符号直接压入
                            else
                            {
                                S1.Pop();
                                simpleCalculate(S2, sign);
                                S1.Push(s[i++]);
                            }
                        }; break;

                    case '*':
                    case '/':
                        if (sign == '*' || sign == '/'||sign=='^')
                        {
                            S1.Pop();
                            simpleCalculate(S2, sign);
                        }; 
                        S1.Push(s[i++]); break;
                    case '^':
                        if (sign == '^')
                        {
                            S1.Pop();
                            simpleCalculate(S2, sign);
                        };
                        S1.Push(s[i++]);break;
                    case '(': 
                        S1.Push(s[i++]); break;
                    case ')':
                        while ((char)S1.Peek() != '(')
                        {
                            simpleCalculate(S2, (char)S1.Pop());
                        }
                        S1.Pop();//Pop左括号
                        if (S1.Count != 0)
                        {
                            char complexSign = (char)S1.Peek();//此时之前的sign已经Pop掉了
                            if (complexSign == 's' || complexSign == 'c' || complexSign == 'e' || complexSign == 'l')
                            {
                                S1.Pop();//Pop特殊运算符
                                complexCalculate(S2, complexSign);
                            }
                        }
                        i += 1; break;
                    case 's': 
                    case 'c':
                        S1.Push(s[i]);
                        i += 3;break;
                    case 'l':
                        S1.Push(s[i]);
                        i += 2; break;
                }
            }
            while (S1.Count != 0)
            {
                simpleCalculate(S2, (char)S1.Pop());
            }
            return (double)S2.Peek();
        }
        private void simpleCalculate(Stack S, char sign)
        {//计算加减乘除幂
            double num1 = (double)S.Pop();
            double num2 = (double)S.Pop();
            if (sign == '+') S.Push(num2 + num1);
            if (sign == '-') S.Push(num2 - num1);
            if (sign == '*') S.Push(num2 * num1);
            if (sign == '/') S.Push(num2 / num1);
            if (sign == '^') S.Push(Math.Pow(num2, num1));
        }

        private void complexCalculate(Stack S,char sign)
        {//计算正弦、余弦、ln对数、e的幂指
            double num = (double)S.Pop();
            if (sign == 's') S.Push(Math.Sin(num));
            if (sign == 'c') S.Push(Math.Cos(num));
            if (sign == 'l') S.Push(Math.Log(num));
            if (sign == 'e') S.Push(Math.Exp(num));
        }

        private int isFormatRight(string s)
        {
            if (s == "") return 0;
            else return complexOperatorMatching(s) * bracketMatching(s) * simpleOperatorMatching(s) * numberUsing(s);
        }
        private int complexOperatorMatching(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] != '+' && s[i] != '-' && s[i] != '*' && s[i] != '/' && s[i] != '^' && s[i] != '.' && (s[i] < '0' || s[i] > '9') && s[i]!='(' &&s[i]!=')')
                {
                    if((s.Length-i)<5 && s[i] > 'a' && s[i] < 'z') return 0;
                    switch (s[i])
                    {
                        case 's':
                            if (s.Substring(i, 4) != "sin(") return 0;
                            else i += 4; break;
                        case 'c':
                            if (s.Substring(i, 4) != "cos(") return 0;
                            else i += 4; break;
                        case 'e':
                            if (s.Substring(i, 4) != "exp(") return 0;
                            else i += 4; break;
                        case 'l':
                            if (s.Substring(i, 3) != "ln(") return 0;
                            else i += 3; break;
                        default: return 0;break;
                    }
                }
            }
            return 1;
        }

        private int bracketMatching(string s)
        {
            int cnt = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') cnt += 1;
                if (s[i] == ')') cnt -= 1;
                if (s[i] == '（' || s[i] == '）') return 0;
                if (cnt < 0) return 0;
            }
            if (cnt == 0) return 1;
            else return 0;
        }

        private int simpleOperatorMatching(string s)
        {
            Stack S = new Stack();
            for (int i = 0; i < s.Length; i++)
            {
                if (S.Count == 0) S.Push(s[i]);
                else
                {
                    if (s[i] == '+' || s[i] == '-' || s[i] == '*' || s[i] == '/' || s[i] == '.' || s[i] == '^')
                    {
                        char tmpChar = (char)S.Peek();
                        if (tmpChar == '+' || tmpChar == '-' || tmpChar == '*' || tmpChar == '/' || tmpChar == '.' || tmpChar == '^')
                        {
                            return 0;
                        }
                    }
                    S.Push(s[i]);
                }
            }
            char tmpChar2 = (char)S.Peek();
            if (tmpChar2 == '+' || tmpChar2 == '-' || tmpChar2 == '*' || tmpChar2 == '/' || tmpChar2 == '.' || tmpChar2 == '^'||tmpChar2=='(')
            {
                return 0;
            }
            return 1;
        }
        private int numberUsing(string s)
        {
            int pointCnt = 0;
            for(int i = 0; i < s.Length; i++)
            {
                while ((i < s.Length) && ((s[i] >= '0' && s[i] <= '9') || s[i] == '.'))
                {
                    if (s[i] == '.') pointCnt += 1;
                    if (pointCnt > 1)
                    {
                        return 0;
                    }
                    i += 1;
                }
                pointCnt = 0;
            }
            return 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
        }
    }
}

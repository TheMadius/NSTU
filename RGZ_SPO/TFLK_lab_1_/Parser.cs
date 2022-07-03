using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFLK_lab_1_
{
    public class Parser
    {
        string str;
        int nowIndex;
        string log;
        string read;
        string strAir;

        public string Log { get => log; }
        public string StrAir { get => strAir; set => strAir = value; }

        public Parser()
        {
            this.nowIndex = 0;
            this.str = "";
            this.log = "";
        }

        private void error(int pos, string e,int NUM = 0)
        {
            string str = "";
            string erno = "";
            str += "Ошибка!! " + e + ", Позиция " + pos + "\n";
            for (int i = 0; i < pos; i++)
                erno += " ";
            erno += "^"+"\n";
            str += this.str.Insert(pos, (NUM == 0)? " ":"") + "\n";
            str += erno + "\n";
            log += str; 
        }

        public void setString(string str)
        {
            this.nowIndex = 0;
            this.str = str;
            this.log = "";
        }
    
        private void state4()
        {
            if (nowIndex >= str.Length)
            {
                if (!read.Equals("read"))
                {
                    error(nowIndex, "Ожидался оператор read");
                    this.strAir += "read";
                }
                else
                {
                    this.strAir += read;
                }
                error(nowIndex, "Отсутствует символ « ( »");
                this.strAir += "(";
                state5();
                return;
            }
            if(isChar(this.str[this.nowIndex]))
            {
                this.read += this.str[this.nowIndex];
                nowIndex++;
                state4();
                return;
            }
            if (str[nowIndex] == '(')
            {
                if (!read.Equals("read"))
                {
                    error(nowIndex, "Ожидался оператор read");
                    this.strAir += "read";
                }
                else
                {
                    this.strAir += read;
                }
                this.strAir += str[nowIndex];
                nowIndex++;
                state5();
                return;
            }
            else
            {
                if (!read.Equals("read"))
                {
                    error(nowIndex, "Ожидался оператор read");
                    this.strAir += "read";
                }
                else
                {
                    this.strAir += read;
                }
                string err = "";
                int pos = this.nowIndex;
                while (str[nowIndex] != '(' && str[nowIndex] != ';')
                {
                    err += str[nowIndex];
                    nowIndex++;
                    if (nowIndex >= str.Length)
                    {
                        nowIndex = pos;
                        error(pos, "Отсутствует символ « ( »");
                        this.strAir += "(";
                        state5();
                        return;
                    }
                }

                if (str[nowIndex] == ';')
                {
                    nowIndex = pos;
                    error(pos, "Отсутствует символ « ( »");
                    this.strAir += "(";
                    state5();
                    return;
                }

                error(pos, "Некорректные символы:" + err);

                if (str[nowIndex] == '(')
                {
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state5();
                    return;
                }
            }
        }
        private void state5()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует 1 значение");
                this.strAir += "*";
                state6();
                return;
            }
            if (str[nowIndex] == '*')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state6();
                return;
            }
            else
            {
                string err = "";
                int pos = this.nowIndex;
                while (str[nowIndex] != ',' && str[nowIndex] != '*' && !IsDigit(str[nowIndex]) && str[nowIndex] != ')' && str[nowIndex] != ';')
                {
                    err += str[nowIndex];
                    nowIndex++;
                    if (nowIndex >= str.Length)
                    {
                        nowIndex = pos;
                        error(pos, "Отсутствует 1 значение");
                        this.strAir += "*";
                        state6();
                        return;
                    }
                }
                if (str[nowIndex] == ',')
                {
                    error(pos, (err == "") ? "Отсутствует 1 значение" : "Некорректные значения:" + err, (err == "") ? 0 : 1);
                    this.strAir += "*";
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state7();
                    return;
                }   
                if (IsDigit(str[nowIndex]))
                {
                    error(pos, (err == "") ? "Отсутствует 1 значение" : "Некорректные значения:" + err, (err == "") ? 0 : 1);
                    this.strAir += "*";
                    state6();
                    return;
                }
                if (str[nowIndex] == ')')
                {
                    error(pos, (err == "") ? "Отсутствует 1 значение" : "Некорректные значения:" + err, (err == "")?0:1);
                    this.strAir += "*";
                    state6();
                    return;
                }
                if (str[nowIndex] == ';')
                {
                    nowIndex = pos;
                    this.strAir += "*";
                    error(pos, "Отсутствует 1 значение");
                    state6();
                    return;
                }

                error(pos, "Некорректные значения:" + err,1);

                if (str[nowIndex] == '*')
                {
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state6();
                    return;
                }
            }
        }
        private void state6()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует ,");
                this.strAir += ",";
                state7();
                return;
            }

            if (str[nowIndex] == ',')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state7();
                return;
            }
            else
            {
                string err = "";
                int pos = this.nowIndex;
                while (str[nowIndex] != ',' && str[nowIndex] != '*' && !IsDigit(str[nowIndex]) && str[nowIndex] != ')' && str[nowIndex] != ';')
                {
                    err += str[nowIndex];
                    nowIndex++;
                    if (nowIndex >= str.Length)
                    {
                        nowIndex = pos;
                        error(pos, "Отсутствует ,");
                        state7();
                        return;
                    }
                }

                if (str[nowIndex] == ')')
                {
                    error(pos, (err == "") ? "Отсутствует ," : "Ожидалась \',\' , а встретилась " + err, (err == "") ? 0 : 1);
                    this.strAir += ",";
                    state7();
                    return;
                }
                if (str[nowIndex] == '*')
                {
                    error(pos, (err == "") ? "Отсутствует ," : "Ожидалась \',\' , а встретилась " + err, (err == "") ? 0 : 1);
                    this.strAir += ",";
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state9();
                    return;
                }
                if (IsDigit(str[nowIndex]))
                {
                    error(pos, (err == "") ? "Отсутствует ," : "Ожидалась \',\' , а встретилась " + err, (err == "") ? 0 : 1);
                    this.strAir += ",";
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state8();
                    return;
                }
                if (str[nowIndex] == ';')
                {
                    nowIndex = pos;
                    this.strAir += ",";
                    error(pos, "Отсутствует ," );
                    state7();
                    return;
                }
                error(pos, "Некорректные значения:"+ err,1);

                if (str[nowIndex] == ',')
                {
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state7();
                    return;
                }
            }
        }
        private void state7()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует 2 значение");
                this.strAir += "*";
                state9();
                return;
            }
            if (str[nowIndex] == '*')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state9();
                return;
            }

            if (IsDigit(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state8();
                return;
            }

            string err = "";
            int pos = this.nowIndex;
            while (str[nowIndex] != ')' && str[nowIndex] != '*' && !IsDigit(str[nowIndex]) && str[nowIndex] != ';')
            {
                err += str[nowIndex];
                nowIndex++;
                if (nowIndex >= str.Length)
                {
                    nowIndex = pos;
                    error(pos, "Отсутствует 2 значение");
                    state9();
                    return;
                }
            }

            if (str[nowIndex] == ')')
            {
                error(pos, (err=="")? "Отсутствует 2 значение": "Некорректные значения:" + err, (err == "") ? 0 : 1);
                this.strAir += "*";
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }

            if (str[nowIndex] == ';')
            {
                nowIndex = pos;
                error(pos, "Отсутствует 2 значение");
                this.strAir += "*";
                state9();
                return;
            }

            error(pos, "Некорректные значения: " + err,1);

            if (str[nowIndex] == '*')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state9();
                return;
            }

            if (IsDigit(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state8();
                return;
            }

        }
        private void state8()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует символ « ) »");
                this.strAir += ")";
                state10();
                return;
            }

            if (str[nowIndex] == ')')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }

            if (IsDigit(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state8();
                return;
            }

            string err = "";
            int pos = this.nowIndex;
            while (str[nowIndex] != ')' && !IsDigit(str[nowIndex])  && str[nowIndex] != ';')
            {
                err += str[nowIndex];
                nowIndex++;
                if (nowIndex >= str.Length)
                {
                    nowIndex = pos;
                    error(pos, "Отсутствует символ « ) »");
                    this.strAir += ")";
                    state10();
                    return;
                }
            }

            if (str[nowIndex] == ';')
            {
                nowIndex = pos;
                error(pos, "Отсутствует символ « ) »");
                this.strAir += ")";
                state10();
                return;
            }
            error(pos, "Некорректные значения:" + err,1);
            if (str[nowIndex] == ')')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }
            if (IsDigit(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state8();
                return;
            }
        }
        private void state9()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует символ « ) »");
                this.strAir += ")";
                state10();
                return;
            }

            if (str[nowIndex] == ')')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }
            else
            {
                string err = "";
                int pos = this.nowIndex;
                while (str[nowIndex] != ')' && str[nowIndex] != ';')
                {
                    err += str[nowIndex];
                    nowIndex++;
                    if (nowIndex >= str.Length)
                    {
                        nowIndex = pos;
                        error(pos, "Отсутствует символ « ) »");
                        this.strAir += ")";
                        state10();
                        return;
                    }
                }
                if (str[nowIndex] == ';')
                {
                    nowIndex = pos;
                    error(pos, "Отсутствует символ « ) »");
                    this.strAir += ")";
                    state10();
                    return;
                }
                error(pos, "Некорректные значения:" + err,1);
                
                if (str[nowIndex] == ')')
                {
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state10();
                    return;
                }
            }
        }
        private void state10()
        {
            if (nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует идентификатор переменной");
                this.strAir += "i";
                error(nowIndex, "Отсутствует ;");
                this.strAir += ";";
                return;
            }

            if (isChar(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state11();
                return;
            }
            else
            {
                string err = "";
                int pos = this.nowIndex;
                while (!isChar(str[nowIndex]) && str[nowIndex] != ',' && str[nowIndex] != ';')
                {
                    err += str[nowIndex];
                    nowIndex++;
                    if (nowIndex >= str.Length)
                    {
                        error(pos, (err == "") ? "Отсутствует идентификатор переменной" : "Некорректные значения:" + err, (err == "") ? 0 : 1);
                        this.strAir += "i";
                        error(nowIndex, "Отсутствует ;");
                        this.strAir += ";";
                        return;
                    }
                }
                if (str[nowIndex] == ';')
                {
                    error(pos, (err == "") ? "Отсутствует идентификатор переменной" : "Некорректные значения:" + err, (err == "") ? 0 : 1);
                    this.strAir += "i";
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state13();
                    return;
                }
                if (str[nowIndex] == ',')
                {
                    error(pos, (err == "") ? "Отсутствует идентификатор переменной" : "Некорректные значения: " + err, (err == "") ? 0 : 1);
                    this.strAir += "i";
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state10();
                    return;
                }
                error(pos, "Некорректные значения:" + err,1);

                if (isChar(str[nowIndex]))
                {
                    this.strAir += str[nowIndex];
                    nowIndex++;
                    state11();
                    return;
                }
            }
        }
        private void state11()
        {
            if(nowIndex >= str.Length)
            {
                error(nowIndex, "Отсутствует ;");
                this.strAir += ";";
                return;
            }

            if (isChar(str[nowIndex]) || IsDigit(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state11();
                return;
            }
            if (str[nowIndex] == ',')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }
            if (str[nowIndex] == ';')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state13();
                return;
            }

            string err = "";
            int pos = this.nowIndex;
            while (str[nowIndex] != ',' && !(IsDigit(str[nowIndex]) || isChar(str[nowIndex])) && str[nowIndex] != ';')
            {
                err += str[nowIndex];
                nowIndex++;
                if (nowIndex >= str.Length)
                {
                    error(pos, "Некорректные значения:" + err);
                    error(nowIndex, "Отсутствует ;");
                    this.strAir += ";";
                    return;
                }
            }

            error(pos, "Некорректные значения:" + err,1);

            if (str[nowIndex] == ';')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state13();
                return;
            }
            if (str[nowIndex] == ',')
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state10();
                return;
            }
            if (IsDigit(str[nowIndex]) || isChar(str[nowIndex]))
            {
                this.strAir += str[nowIndex];
                nowIndex++;
                state11();
                return;
            }
        }
        
        private void state13()
        {
            return;
        }
        private bool isChar(char ch)
        {
            return char.IsLetter(ch);
        }
        private bool IsDigit(char ch)
        {
            return char.IsDigit(ch);
        }
       
        public void isRelate()
        {
            this.log = "";
            this.read = "";
            this.strAir = "";
            state4();
        }
    }
}
